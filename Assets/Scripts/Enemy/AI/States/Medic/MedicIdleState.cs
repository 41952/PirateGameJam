using UnityEngine;
using System.Linq;

public class MedicIdleState : MedicState
{
    public MedicIdleState(MedicAI owner, StateMachine sm) : base(owner, sm) { }
    public override void Enter() { }
    public override void Tick()
    {
        stateMachine.ChangeState(new MedicSelectPatientState(medic, stateMachine));
    }
}