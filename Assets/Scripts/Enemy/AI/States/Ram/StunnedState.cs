using UnityEngine;

public class StunnedState : State
{
    private ChargingEnemyAI chargingOwner;
    private float stunStartTime;
    private float stunDuration = 2.5f;

    public StunnedState(ChargingEnemyAI o, StateMachine s) : base(o, s)
    {
        chargingOwner = o;
    }

    public override void Enter()
    {
        chargingOwner.agent.isStopped = true;
        stunStartTime = Time.time;
    }

    public override void Tick()
    {
        if (Time.time >= stunStartTime + stunDuration)
        {
            stateMachine.ChangeState(new ChaseState(chargingOwner, stateMachine));
        }
    }
}
