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
    [HideInInspector] public Transform player;

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
    [HideInInspector] public NavMeshAgent agent;
    public LayerMask obstacleMask;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Sounds")]

    private float lastDetectedTime = Mathf.NegativeInfinity;

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
        health.ResetHealth();

        lastDetectedTime = Mathf.NegativeInfinity;

        // if (!agent.enabled) agent.enabled = true;
        // agent.ResetPath();

        stateMachine.Initialize(new IdleState(this, stateMachine));

        // if (navSurface == null)
        //     navSurface = FindObjectOfType<NavMeshSurface>();

        // Например, сброс анимации смерти:
        // animator.Play("Idle");
    }

    public void PlayAnimation(string animationName)
    {
        animator?.Play(animationName);
    }

    public void SetTrigger(string triggerName)
    {
        animator?.SetTrigger(triggerName);
    }

    public void SetBool(string param, bool value)
    {
        animator?.SetBool(param, value);
    }

    public void SetFloat(string param, float value)
    {
        animator?.SetFloat(param, value);
    }

    public void ResetTrigger(string triggerName)
    {
        animator?.ResetTrigger(triggerName);
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