using UnityEngine;
// Move towards the patient
public class MedicChaseState : MedicState
{
    private Rigidbody rb;
    private Transform targetPoint;

    public MedicChaseState(MedicAI owner, StateMachine sm, Transform point) : base(owner, sm)
    {
        rb = owner.GetComponent<Rigidbody>();
        targetPoint = point;
    }

    public override void Enter()
    {
        medic.agent.enabled = false;
    }

    public override void Tick()
    {
        // If no patient or patient is dead, clean up and return to idle
        if (medic.currentPatient == null || medic.currentPatient.GetCurrentHealth() <= 0f)
        {
            if (medic.currentPatient != null)
                MedicTargetRegistry.Unassign(medic.currentPatient);
            medic.currentPatient = null;
            stateMachine.ChangeState(new MedicIdleState(medic, stateMachine));
            return;
        }

        Vector3 dest = targetPoint != null ? targetPoint.position : medic.currentPatient.transform.position;
        Vector3 dir = (dest - medic.transform.position).normalized;
        Vector3 desiredVel = dir * medic.flySpeed;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, desiredVel, Time.deltaTime * medic.acceleration);

        // Smooth rotation
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            medic.transform.rotation = Quaternion.RotateTowards(medic.transform.rotation, targetRot, medic.rotationSpeed * Time.deltaTime);
        }

        float dist = Vector3.Distance(medic.transform.position, dest);
        if (dist < 0.1f)
        {
            stateMachine.ChangeState(new MedicHealState(medic, stateMachine, targetPoint));
        }
    }

    public override void Exit()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
