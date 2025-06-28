using UnityEngine;

public abstract class State 
{
    protected EnemyAI owner;
    protected StateMachine stateMachine;

    protected State(EnemyAI owner, StateMachine stateMachine)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    // Called once when entering the state
    public virtual void Enter() { }
    // Called every frame while in this state
    public abstract void Tick();
    // Called once when exiting the state
    public virtual void Exit() { }
}


