using UnityEngine;

public class ChargePrepareState : State
{
    private ChargingEnemyAI chargingOwner;
    private float prepareStartTime;
    private bool signaled;

    public ChargePrepareState(ChargingEnemyAI o, StateMachine s) : base(o, s)
    {
        chargingOwner = o;
    }

    public override void Enter()
    {
        chargingOwner.agent.isStopped = true;
        prepareStartTime = Time.time;
        signaled = false;
    }

    public override void Tick()
    {
        // Медленный поворот к игроку
        Vector3 dir = chargingOwner.player.position - chargingOwner.transform.position;
        chargingOwner.SlowRotate(dir);

        float angle = Vector3.Angle(chargingOwner.transform.forward, new Vector3(dir.x, 0, dir.z));
        if (angle <= 10f && !signaled)
        {
            // Вызов сигнала интерфейса
        //GameEvents.OnPlayerWarned?.Invoke(chargingOwner.transform.position);
            signaled = true;
        }

        if (Time.time >= prepareStartTime + chargingOwner.chargeDelay)
        {
            stateMachine.ChangeState(new ChargeState(chargingOwner, stateMachine, chargingOwner.wallMask));
        }
    }
}
