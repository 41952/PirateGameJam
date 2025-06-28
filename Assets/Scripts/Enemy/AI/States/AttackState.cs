using UnityEngine;

using System;
public class AttackState : State
{
    private float lastAttackTime;
    public AttackState(EnemyAI owner, StateMachine sm) : base(owner, sm) { }

    public override void Enter() => lastAttackTime = Time.time - owner.attackCooldown;

    public override void Tick()
    {
        // Y-axis only rotation
        Vector3 rawDir = owner.player.position - owner.transform.position;
        Vector3 dir = new Vector3(rawDir.x, 0, rawDir.z).normalized;
        owner.transform.rotation = Quaternion.LookRotation(dir);

        float dist = Vector3.Distance(owner.player.position, owner.transform.position);
        if (dist > owner.agent.stoppingDistance || !owner.CanDetectPlayer())
        {
            stateMachine.ChangeState(new ChaseState(owner, stateMachine));
            return;
        }

        // Check both global and local cooldowns
        if (Time.time - lastAttackTime >= owner.attackCooldown && AttackCooldownManager.CanGlobalAttack())
        {
            // TODO: implement attack logic/animation here
            Debug.Log("ATTACK!~");
            lastAttackTime = Time.time;
            AttackCooldownManager.NotifyGlobalAttack();
        }
    }

    public override void Exit() { }
}