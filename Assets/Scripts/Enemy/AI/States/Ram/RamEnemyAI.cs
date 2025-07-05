using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public class ChargingEnemyAI : EnemyAI
{
    [Header("Charge Settings")]
    public float chargePrepareRadius = 6f;
    public float chargeDelay = 2f;
    public float chargeSpeed = 18f;
    public float chargeDuration = 1.5f;
    public float rotationSpeed = 1f;

    [HideInInspector] public bool isCharging;
    [HideInInspector] public Vector3 chargeTarget;

    public LayerMask wallMask;

    public ChargingDamage chargingTrigger;

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(new IdleState(this, stateMachine));
    }

    public void SlowRotate(Vector3 targetDir)
    {
        var flatDir = new Vector3(targetDir.x, 0, targetDir.z).normalized;
        var targetRot = Quaternion.LookRotation(flatDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }

    public bool InChargeRange()
    {
        return Vector3.Distance(player.position, transform.position) <= chargePrepareRadius && CanDetectPlayer();
    }
    public void InterruptCharge()
    {
        if (!isCharging) return;

        isCharging = false;
        agent.enabled = true;
        stateMachine.ChangeState(new StunnedState(this, stateMachine));
    }

}


   