using UnityEngine;

public class ReloadBuffEffect : IEffect {
    private float multiplier;
    private float originalCooldown;

    public ReloadBuffEffect(EnemyAI target, float duration, float multiplier) : base(target, duration) {
        this.multiplier = multiplier;
    }

    protected override void Apply() {
        originalCooldown = target.attackCooldown;
        target.attackCooldown /= multiplier; // faster reload => lower cooldown
    }

    protected override void End() {
        if (target != null) {
            target.attackCooldown = originalCooldown;
        }
    }
}