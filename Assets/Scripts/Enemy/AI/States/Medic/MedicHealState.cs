using UnityEngine;

public class MedicHealState : MedicState
{
    private EnemyAI patientAI;
    private Transform point;

    public MedicHealState(MedicAI owner, StateMachine sm, Transform healPt) : base(owner, sm)
    {
        point = healPt;
    }

    public override void Enter()
    {
        medic.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        patientAI = medic.currentPatient.GetComponent<EnemyAI>();
    if (patientAI != null)
    {
        patientAI.agent.isStopped = true;
        patientAI.agent.ResetPath();
    }

        medic.SetBool("isHeal", true);
    }

    public override void Tick()
    {
        // If no patient or patient is dead, clean up and return to idle
        if (medic.currentPatient == null || medic.currentPatient.GetCurrentHealth() <= 0f)
        {
            if (patientAI != null)
            {
                patientAI.agent.isStopped = true;
            }
            if (medic.currentPatient != null)
                MedicTargetRegistry.Unassign(medic.currentPatient);
            medic.currentPatient = null;
            stateMachine.ChangeState(new MedicIdleState(medic, stateMachine));
            return;
        }

        // Face the repair point
        Vector3 dir = (point != null ? point.position : medic.currentPatient.transform.position) - medic.transform.position;
        dir.Normalize();
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            medic.transform.rotation = Quaternion.RotateTowards(medic.transform.rotation, targetRot, medic.rotationSpeed * Time.deltaTime);
        }

        // Heal over time
        float healAmount = 10f * Time.deltaTime;
        float missingHP = medic.currentPatient.maxHP - medic.currentPatient.GetCurrentHealth();
        if (missingHP > 0f)
        {
            healAmount = Mathf.Min(healAmount, missingHP); // не больше, чем нужно
            DamageData data = new DamageData(-healAmount, 1f, DamageType.Explosion, HitZone.Body);
            medic.currentPatient.ReceiveDamage(-healAmount, data);
        }

        if (medic.currentPatient.GetCurrentHealth() >= medic.currentPatient.maxHP)
        {
            if (patientAI != null)
                patientAI.agent.isStopped = false;

            MedicTargetRegistry.Unassign(medic.currentPatient);
            medic.currentPatient = null;

            stateMachine.ChangeState(new MedicIdleState(medic, stateMachine));
        }
        else if (patientAI != null)
        {
            patientAI.agent.isStopped = true;
        }
    }

    public override void Exit() { medic.SetBool("isHeal", false); }
}