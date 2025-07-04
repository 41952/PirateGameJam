using UnityEngine;

public class HealthBuffEffect : IEffect {
    private float multiplier;
    private float addedMaxHp;

    public HealthBuffEffect(EnemyAI target, float duration, float multiplier) : base(target, duration) {
        this.multiplier = multiplier;
    }

    protected override void Apply() {
        var health = target.GetComponent<EnemyHealth>();
        float oldMax = health.maxHP;
        float newMax = oldMax * multiplier;
        addedMaxHp = newMax - oldMax;
        health.maxHP = newMax;
        DamageData data = new DamageData(-newMax * 0.5f, 1f, DamageType.Explosion, HitZone.Body);
        // Heal for half of new max
        health.ReceiveDamage(-newMax * 0.5f, data);
    }

    protected override void End() {
        var health = target.GetComponent<EnemyHealth>();
        health.maxHP -= addedMaxHp;
        // ensure currentHP not above max
        if (health.GetCurrentHealth() > health.maxHP) {
            DamageData data = new DamageData(health.maxHP - health.GetCurrentHealth(), 1f, DamageType.Explosion, HitZone.Body);
            health.ReceiveDamage(health.maxHP - health.GetCurrentHealth(), data);
        }
    }
}
