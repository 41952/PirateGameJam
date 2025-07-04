using UnityEngine;
using System;
public class AttackState : State
{
    private float lastAttack;
    public AttackState(EnemyAI o, StateMachine s): base(o,s){}
    public override void Enter() { lastAttack = Time.time - owner.attackCooldown; }
    public override void Tick()
    {
        RotateY();
        float dist = Vector3.Distance(owner.player.position, owner.transform.position);
        if(dist > owner.agent.stoppingDistance || !owner.CanDetectPlayer())
            stateMachine.ChangeState(new ChaseState(owner, stateMachine));
        else if(Time.time - lastAttack >= owner.attackCooldown && AttackCooldownManager.CanGlobalAttack())
        {
            // perform attack
            var ph = owner.player.GetComponent<PlayerHealthSystem>();
            if(ph!=null) ph.TakeDamage(owner.attackDamage);
            lastAttack = Time.time;
            AttackCooldownManager.NotifyGlobalAttack();
        }
    }
    private void RotateY()
    {
        Vector3 raw=owner.player.position - owner.transform.position;
        owner.transform.rotation = Quaternion.LookRotation(new Vector3(raw.x,0,raw.z).normalized);
    }
}
