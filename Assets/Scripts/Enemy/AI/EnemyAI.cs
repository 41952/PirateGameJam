using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

[RequireComponent(typeof(Collider), typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float sightRadius = 10f;               // Cone detection distance
    [Range(0, 360)] public float sightAngle = 120f; // Cone detection angle
    public float detectionSphereRadius = 3f;      // Close-proximity sphere detection

    [Header("Attack Settings")]
    public float attackRadius = 2f;               // Distance to trigger melee attack
    public float attackCooldown = 2f;

    [Header("Memory")]
    public float memoryDuration = 2f;             // How long to remember player

    [Header("Navigation")]
    public NavMeshSurface navSurface;             // Assign for dynamic NavMesh rebuild

    [Header("Gizmos")]
    public bool drawGizmos = true;

    public LayerMask obstacleMask;

    [HideInInspector] public Transform player;
    private float lastDetectedTime = Mathf.NegativeInfinity;

    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    private StateMachine stateMachine;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;              // Manual rotation for instant turn
        agent.stoppingDistance = attackRadius;
        stateMachine = new StateMachine();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (navSurface != null) navSurface.BuildNavMesh();
        stateMachine.Initialize(new IdleState(this, stateMachine));
        agent.autoTraverseOffMeshLink = false;
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

    public bool PlayerInMemory => Time.time - lastDetectedTime <= memoryDuration;

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Vector3 fwd = transform.forward * sightRadius;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, sightAngle/2,0) * fwd);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -sightAngle/2,0) * fwd);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}