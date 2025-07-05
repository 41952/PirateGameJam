using UnityEngine;

public abstract class MedicState : State
{
    protected MedicAI medic;
    protected Transform healPoint;

    public MedicState(MedicAI owner, StateMachine sm) : base(owner, sm)
    {
        medic = owner;
    }
}