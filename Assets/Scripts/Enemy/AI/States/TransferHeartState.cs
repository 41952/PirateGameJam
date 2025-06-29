using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TransferHeartState : State
{
    private List<EnemyAI> stoppedAllies = new List<EnemyAI>();
    private EnemyGraphics enemyHeart;
    public TransferHeartState(EnemyAI o, StateMachine s) : base(o, s) { }
    public override void Enter()
    {
        stoppedAllies.Clear();
        Collider[] hits = Physics.OverlapSphere(owner.transform.position, owner.seekAllyRadius);
        foreach (var c in hits)
        {
            EnemyAI ai = c.GetComponent<EnemyAI>();
            if (ai != null && ai != owner) // Игнорируем самого себя
            {
                ai.agent.isStopped = true;
                stoppedAllies.Add(ai);
            }
        }

        // Если никого не нашли — не передаём сердце
        if (stoppedAllies.Count == 0)
        {
            stateMachine.ChangeState(new DeadState(owner, stateMachine)); // или другое состояние
            return;
        }

        owner.agent.isStopped = true;

        if (enemyHeart == null)
            enemyHeart = owner.GetComponent<EnemyGraphics>();
        enemyHeart?.ActivateHeart();

        owner.StartCoroutine(TransferRoutine());
    }

    public override void Tick() { }
    private IEnumerator TransferRoutine()
    {
        float timer = 0f;
        while(timer < owner.transferTimeout)
        {
            timer += Time.deltaTime; yield return null;
        }
        // resume allies
        foreach(var ai in stoppedAllies)
        {
            if(ai != null) ai.agent.isStopped = false;
        }
        // self die
        owner.health.Die(); 
        stateMachine.ChangeState(new DeadState(owner, stateMachine));
    }
}