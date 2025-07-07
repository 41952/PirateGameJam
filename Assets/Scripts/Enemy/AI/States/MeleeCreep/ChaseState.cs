using UnityEngine;
using UnityEngine.AI;
public class ChaseState : State
{
    public ChaseState(EnemyAI o, StateMachine s): base(o, s) { }
    public override void Enter() => owner.agent.isStopped = false;
    public override void Tick()
    {
        owner.UpdateDetection();
        // Instant Y-only rotation
        Vector3 rawDir = owner.player.position - owner.transform.position;
        Vector3 dir = new Vector3(rawDir.x, 0, rawDir.z).normalized;
        owner.transform.rotation = Quaternion.LookRotation(dir);

        // If currently on OffMeshLink, jump
        if(owner.agent.isOnOffMeshLink)
        {
            stateMachine.ChangeState(new JumpState(owner, stateMachine));
            return;
        }

        owner.agent.SetDestination(owner.player.position);

        // В Tick() заменяем ветку 
        float dist = Vector3.Distance(owner.player.position, owner.transform.position);
        if (owner is RangedEnemyAI ranged && ranged.InRangedRange())
        {
            if (owner is HeavyRangedEnemyAI heavy && heavy.InRangedRange())
            {
                owner.agent.isStopped = true;
                heavy.StartBurstAttack();
            }
            else
            {
                owner.agent.isStopped = true;
                stateMachine.ChangeState(new RangedAttackState(ranged, stateMachine));
            }
        }
        if (owner is ChargingEnemyAI charger && charger.InChargeRange())
        {

            stateMachine.ChangeState(new ChargePrepareState(charger, stateMachine));
            return;
        }

        else if (dist <= owner.agent.stoppingDistance && owner.CanDetectPlayer())
        {
            // обычная ближняя атака для тех, кто не наследует RangedEnemyAI
            owner.agent.isStopped = true;
            stateMachine.ChangeState(new AttackState(owner, stateMachine));
        }



        else if (!owner.CanDetectPlayer() && !owner.PlayerInMemory)
        {
            owner.agent.isStopped = true;
            stateMachine.ChangeState(new IdleState(owner, stateMachine));
        }
    }
    public override void Exit() => owner.agent.isStopped = true;
}