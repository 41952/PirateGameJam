using UnityEngine;
using System.Collections.Generic;
public class DeadState : State
{
    public DeadState(EnemyAI o, StateMachine s): base(o,s){}
    private List<EnemyAI> stoppedAllies = new List<EnemyAI>();
    public override void Enter()
    {
        owner.agent.isStopped = true;

        // stop all nearby AI and record
        Collider[] hits = Physics.OverlapSphere(owner.transform.position, owner.seekAllyRadius);
        foreach (var c in hits)
        {
            EnemyAI ai = c.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.agent.isStopped = false;
                stoppedAllies.Add(ai);
            }
        }

        owner.gameObject.SetActive(false);
    }
    public override void Tick() { }
}
