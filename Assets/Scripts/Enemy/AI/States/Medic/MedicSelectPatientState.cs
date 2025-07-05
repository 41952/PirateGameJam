using UnityEngine;
using System.Linq;
public class MedicSelectPatientState : MedicState
{
    public MedicSelectPatientState(MedicAI owner, StateMachine sm) : base(owner, sm) { }

    public override void Enter()
    {
        var candidates = Physics.OverlapSphere(medic.transform.position, medic.seekRadius)
            .Select(c => c.GetComponent<EnemyHealth>())
            .Where(h => h != null && h.GetCurrentHealth() < h.maxHP && !MedicTargetRegistry.IsAssigned(h))
            .ToList();

        EnemyHealth chosen = candidates
            .OrderBy(h => medic.priorityOrder.IndexOf(h.GetComponent<EnemyAI>().enemyType))
            .FirstOrDefault();

        if (chosen != null && MedicTargetRegistry.TryAssign(chosen, medic))
        {
            medic.currentPatient = chosen;
            // find repair point on patient
            healPoint = chosen.GetComponentInChildren<RepairPoint>()?.transform;
            stateMachine.ChangeState(new MedicChaseState(medic, stateMachine, healPoint));
        }
        else
        {
            stateMachine.ChangeState(new MedicIdleState(medic, stateMachine));
        }
    }

    public override void Exit() { }
    public override void Tick() { }
}
