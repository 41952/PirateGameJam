using UnityEngine;

public class DamageBuffEffect : IEffect {
    private float multiplier;
    private float originalDamage;

    public DamageBuffEffect(EnemyAI target, float duration, float multiplier) : base(target, duration) {
        this.multiplier = multiplier;
    }

    protected override void Apply() {
        originalDamage = target.attackDamage;
        target.attackDamage *= multiplier;
    }

    protected override void End() {
        if (target != null) target.attackDamage = originalDamage;
    }
}
