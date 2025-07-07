using UnityEngine;

public class ChargeState : State
{
    private ChargingEnemyAI chargingOwner;
    private float chargeStartTime;

    private LayerMask wallMask;  // маска слоя стен

    public ChargeState(ChargingEnemyAI o, StateMachine s, LayerMask walls) : base(o, s)
    {
        chargingOwner = o;
        wallMask = walls;
    }

    public override void Enter()
    {
        chargingOwner.agent.enabled = false;
        chargingOwner.isCharging = true;
        chargeStartTime = Time.time;

        // Сохраняем направление
        chargingOwner.chargeTarget = chargingOwner.transform.position + chargingOwner.transform.forward * 50f; // длинная прямая
        chargingOwner.chargingTrigger.gameObject.SetActive(true);
        chargingOwner.chargingTrigger.Init(chargingOwner);
    }

public override void Tick()
{
    float delta = Time.time - chargeStartTime;
    if (delta < chargingOwner.chargeDuration)
    {
        Vector3 dir = chargingOwner.transform.forward;
        float moveDist = chargingOwner.chargeSpeed * Time.deltaTime;
        
        // 1) Кастим луч чуть дальше, чем будем сдвигаться
        if (Physics.Raycast(chargingOwner.transform.position + Vector3.up * 0.1f, 
                            dir, 
                            out RaycastHit hit, 
                            moveDist + 4f, 
                            wallMask))
        {
            // Нашли стену – стоп
            StopCharge();
            return;
        }
        
        // 2) Если стены нет — двигаемся вручную (можно оставить transform, т.к. коллайдеры стен статические)
        chargingOwner.transform.position += dir * moveDist;
    }
    else
    {
        StopCharge();
    }
}
private void StopCharge()
{
    chargingOwner.isCharging = false;
    chargingOwner.agent.enabled = true;
    stateMachine.ChangeState(new StunnedState(chargingOwner, stateMachine));
}


    public override void Exit()
    {
        chargingOwner.isCharging = false;
    }
}
