using UnityEngine;
using UnityEngine.AI;

public class PortalTraverseState : State
{
    public PortalTraverseState(EnemyAI owner, StateMachine sm) : base(owner, sm) { }

    public override void Enter()
    {
        // Получаем данные OffMeshLink
        var data = owner.agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos;

        // Отключаем NavMeshAgent на время телепорта
        owner.agent.updatePosition = false;

        // Ставим позицию врага в точку выхода
        owner.transform.position = endPos;

        // Завершаем переход
        owner.agent.CompleteOffMeshLink();
        owner.agent.updatePosition = true;

        // Возвращаемся в погоню
        stateMachine.ChangeState(new ChaseState(owner, stateMachine));
    }

    public override void Tick() { }
    public override void Exit() { }
}
