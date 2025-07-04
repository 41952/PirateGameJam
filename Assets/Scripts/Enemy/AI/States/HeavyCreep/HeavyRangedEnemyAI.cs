using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public class HeavyRangedEnemyAI : RangedEnemyAI
{
    [Header("Heavy Ranged Settings")]
    [Tooltip("Number of projectiles per burst")] public int burstSize = 3;
    [Tooltip("Time between individual shots in a burst")] public float timeBetweenShots = 0.2f;
    [Tooltip("Cooldown after finishing a burst before next burst can start")] public float burstCooldown = 2f;
    [Tooltip("Rotation speed multiplier (lower = slower turn)")] [Range(0.1f, 5f)] public float rotationSpeed = 1f;

    protected override void Start()
    {
        base.Start();
        // Replace attack state with heavy burst state
        stateMachine.Initialize(new IdleState(this, stateMachine));
    }

    /// <summary>
    /// Called by ChaseState when in range
    /// </summary>
    public void StartBurstAttack()
    {
        stateMachine.ChangeState(new HeavyBurstAttackState(this, stateMachine));
    }

    /// <summary>
    /// Custom rotation: slower Slerp
    /// </summary>
    /// <param name="targetDir"></param>
    public void SlowRotate(Vector3 targetDir)
    {
        var flatDir = new Vector3(targetDir.x, 0, targetDir.z).normalized;
        var targetRot = Quaternion.LookRotation(flatDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }
}