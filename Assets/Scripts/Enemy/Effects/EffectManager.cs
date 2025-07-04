using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyAI))]
public class EffectManager : MonoBehaviour {
    private List<IEffect> activeEffects = new List<IEffect>();
    private EnemyAI ai;

    private void Awake() {
        ai = GetComponent<EnemyAI>();
    }

    private void Update() {
        float dt = Time.deltaTime;
        for (int i = activeEffects.Count - 1; i >= 0; i--) {
            activeEffects[i].Tick(dt);
        }
        activeEffects.RemoveAll(e => e is null);
    }

    public void AddEffect(IEffect effect) {
        activeEffects.Add(effect);
        effect.Start();
    }
}
