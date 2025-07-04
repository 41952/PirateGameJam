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
        enemyHeart?.ActivateHeartOutline();

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
        foreach (var ai in stoppedAllies) {
            if (ai == null) continue;
            var em = ai.GetComponent<EffectManager>();
            if (em == null) em = ai.gameObject.AddComponent<EffectManager>();

            float duration = 20f;
            float mult = 2f;
            int choice = Random.Range(0, 4);
            IEffect effect = null;

            switch (choice) {
                case 0:
                    effect = new SpeedBuffEffect(ai, duration, mult);
                    break;
                case 1:
                    effect = new DamageBuffEffect(ai, duration, mult);
                    break;
                case 2:
                    effect = new ReloadBuffEffect(ai, duration, mult);
                    break;
                case 3:
                    effect = new HealthBuffEffect(ai, duration, mult);
                    break;
            }

            if (effect != null)
                em.AddEffect(effect);
        }

        // self die
        owner.health.Die(); 
        stateMachine.ChangeState(new DeadState(owner, stateMachine));
    }
}