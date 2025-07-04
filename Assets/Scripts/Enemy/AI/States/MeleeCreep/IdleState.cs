using UnityEngine;
public class IdleState : State
{
    public IdleState(EnemyAI owner, StateMachine sm) : base(owner, sm) { }
    public override void Enter() { }
    public override void Tick()
    {
        owner.UpdateDetection();
        if (owner.CanDetectPlayer())
            stateMachine.ChangeState(new ChaseState(owner, stateMachine));
    }
    public override void Exit() { }
}
