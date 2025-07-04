using UnityEngine;

public class RangedAttackState : State
{
    private RangedEnemyAI rangedOwner;
    private float aimDuration = 0.6f; // Задержка перед выстрелом
    private float aimStartTime;
    private bool hasFired;

    public RangedAttackState(RangedEnemyAI o, StateMachine s) : base(o, s)
    {
        rangedOwner = o;
    }

    public override void Enter()
    {
        aimStartTime = Time.time;
        hasFired = false;
    }

    public override void Tick()
    {
        // Плавный поворот к игроку
        Vector3 dir = (rangedOwner.player.position + Vector3.up * 0.2f - rangedOwner.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        rangedOwner.transform.rotation = Quaternion.Slerp(rangedOwner.transform.rotation, targetRotation, Time.deltaTime * 5f);

        if (!rangedOwner.InRangedRange())
        {
            stateMachine.ChangeState(new ChaseState(rangedOwner, stateMachine));
            return;
        }

        // После небольшой задержки — огонь
        if (!hasFired && Time.time >= aimStartTime + aimDuration)
        {
            rangedOwner.FireProjectile();
            hasFired = true;
        }

        // После выстрела и cooldown — возвращаемся к погоне
        if (hasFired && Time.time >= aimStartTime + aimDuration + 0.3f)
        {
            stateMachine.ChangeState(new ChaseState(rangedOwner, stateMachine));
        }
    }
}

