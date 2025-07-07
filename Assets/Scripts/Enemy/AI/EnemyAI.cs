using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("Detection Settings")]
    public float sightRadius = 10f;               // Cone detection distance
    [Range(0, 360)] public float sightAngle = 120f; // Cone detection angle
    public float detectionSphereRadius = 3f;      // Close-proximity sphere detection

    [Header("Ally Transfer")]
    public float seekAllyRadius = 5f;
    public float transferTimeout = 2f;

    [Header("Attack Settings")]
    public float attackRadius = 2f;               // Distance to trigger melee attack
    public float attackCooldown = 2f;
    public float attackDamage = 10f;

    [Header("Memory")]
    public float memoryDuration = 2f;             // How long to remember player

    [Header("Navigation")]
    public NavMeshSurface navSurface;             // Assign for dynamic NavMesh rebuild

    [Header("Gizmos")]
    public bool drawGizmos = true;

    public LayerMask obstacleMask;

    [HideInInspector] public Transform player;
    private float lastDetectedTime = Mathf.NegativeInfinity;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public EnemyHealth health;
    private EnemyGraphics enemyHeart;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;              // Manual rotation for instant turn
        agent.stoppingDistance = attackRadius;
        stateMachine = new StateMachine();
        health = GetComponent<EnemyHealth>();
        if (enemyHeart == null)
            enemyHeart = GetComponent<EnemyGraphics>();
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDamaged += OnAnyEnemyDamaged;
        enemyHeart?.DeactivateBuffOutline();
        enemyHeart?.DeactivateHeartOutline();
        enemyHeart?.DeactivateHeart();
        ResetEnemy();
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDamaged -= OnAnyEnemyDamaged;
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (navSurface != null) navSurface.BuildNavMesh();
        stateMachine.Initialize(new IdleState(this, stateMachine));
        agent.autoTraverseOffMeshLink = false;
        if (navSurface == null)
        {
            navSurface = FindObjectOfType<NavMeshSurface>();
        }
    }

    void Update()
    {
        stateMachine.Update();
    }

    public bool CanDetectPlayer()
    {
        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;
        // Cone detection
        if (dist <= sightRadius && Vector3.Angle(transform.forward, dir.normalized) < sightAngle / 2)
        {
            if (!Physics.Raycast(transform.position, dir.normalized, dist, obstacleMask))
                return true;
        }
        // Sphere detection
        if (dist <= detectionSphereRadius)
        {
            if (!Physics.Raycast(transform.position, dir.normalized, dist, obstacleMask))
                return true;
        }
        return false;
    }

    public void UpdateDetection()
    {
        if (CanDetectPlayer()) lastDetectedTime = Time.time;
    }

    private void OnAnyEnemyDamaged(EnemyHealth target, float damage, DamageData data)
    {
        if (target == health)
        {
            float percent = health.GetHealthPercent();
            if (percent <= 0f)
                stateMachine.ChangeState(new DeadState(this, stateMachine));
            else if (percent <= 0.1f)
                stateMachine.ChangeState(new TransferHeartState(this, stateMachine));
            else if (percent <= 0.33f)
                stateMachine.ChangeState(new SeekAllyState(this, stateMachine));
        }
    }

    public bool PlayerInMemory => Time.time - lastDetectedTime <= memoryDuration;
    
    public void ResetEnemy()
    {
        // 1. Сброс здоровья
        health.ResetHealth();

        // 2. Сброс времени обнаружения
        lastDetectedTime = Mathf.NegativeInfinity;

        // 3. Сброс навигации
        if (!agent.enabled) agent.enabled = true;
        agent.ResetPath();

        // 4. Сброс стейт-машины
        stateMachine.Initialize(new IdleState(this, stateMachine));

        // 5. Перестроение навмеша, если надо
        if (navSurface == null)
            navSurface = FindObjectOfType<NavMeshSurface>();

        if (navSurface != null)
            navSurface.BuildNavMesh();

        // 6. Прочие визуальные/аудио эффекты, если есть
        // Например, сброс анимации смерти:
        // animator.Play("Idle");
    }


    protected virtual void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, sightRadius);
        Vector3 fwd = transform.forward * sightRadius;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, sightAngle / 2, 0) * fwd);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -sightAngle / 2, 0) * fwd);
        Gizmos.color = Color.cyan; Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.magenta; Gizmos.DrawWireSphere(transform.position, seekAllyRadius);
    }
}