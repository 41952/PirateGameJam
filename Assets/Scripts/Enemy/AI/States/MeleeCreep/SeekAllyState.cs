using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class SeekAllyState : State
{
    private EnemyAI targetAlly;
    public SeekAllyState(EnemyAI o, StateMachine s): base(o,s){}
    public override void Enter()
    {
        Collider[] hits = Physics.OverlapSphere(owner.transform.position, owner.seekAllyRadius);
        float best = float.MaxValue;
        foreach(var c in hits)
        {
            EnemyAI ai = c.GetComponent<EnemyAI>();
            if(ai != null && ai != owner)
            {
                float d = Vector3.Distance(owner.transform.position, ai.transform.position);
                if(d < best) { best = d; targetAlly = ai; }
            }
        }
        owner.agent.isStopped = false;
    }
    public override void Tick()
    {
        if (targetAlly == null)
        {
            stateMachine.ChangeState(new ChaseState(owner, stateMachine));
            return;
        }
        RotateY();
        if (owner.agent.isOnOffMeshLink)
        {
            stateMachine.ChangeState(new JumpState(owner, stateMachine));
            return;
        }
        owner.agent.SetDestination(targetAlly.transform.position);
        float dist = Vector3.Distance(owner.transform.position, targetAlly.transform.position);
        if (dist <= owner.seekAllyRadius * 0.5f)
        {
            owner.agent.isStopped = true;
        }
        else
            owner.agent.isStopped = false;
    }
    private void RotateY()
    {
        Vector3 raw = targetAlly.transform.position - owner.transform.position;
        owner.transform.rotation = Quaternion.LookRotation(new Vector3(raw.x,0,raw.z).normalized);
    }
    public override void Exit() => owner.agent.isStopped = true;
}
