using UnityEngine;

public class HeavyBurstAttackState : State
{
    private HeavyRangedEnemyAI heavy;
    private int shotsFired;
    private float nextShotTime;
    private float burstEndTime;

    public HeavyBurstAttackState(HeavyRangedEnemyAI owner, StateMachine sm) : base(owner, sm)
    {
        heavy = owner;
    }

    public override void Enter()
    {
        shotsFired = 0;
        nextShotTime = Time.time;
        burstEndTime = Time.time + (heavy.burstSize - 1) * heavy.timeBetweenShots + heavy.burstCooldown;
        heavy.agent.isStopped = true;
    }

    public override void Tick()
    {
        // Update detection in case player moves away
        heavy.UpdateDetection();

        // Rotate slowly toward player
        Vector3 dir = (heavy.player.position + Vector3.up * 0.2f - heavy.transform.position).normalized;
        heavy.SlowRotate(dir);

        if (!heavy.InRangedRange())
        {
            stateMachine.ChangeState(new ChaseState(heavy, stateMachine));
            return;
        }

        // Fire burst
        if (shotsFired < heavy.burstSize && Time.time >= nextShotTime)
        {
            heavy.FireProjectile();
            shotsFired++;
            nextShotTime = Time.time + heavy.timeBetweenShots;
        }
        // After finishing burst and cooldown, go back to chase
        else if (shotsFired >= heavy.burstSize && Time.time >= burstEndTime)
        {
            stateMachine.ChangeState(new ChaseState(heavy, stateMachine));
        }
    }

    public override void Exit()
    {
        heavy.agent.isStopped = false;
    }
}
