using UnityEngine;

public class SpeedBuffEffect : IEffect {
    private float multiplier;
    private float originalSpeed;

    public SpeedBuffEffect(EnemyAI target, float duration, float multiplier) : base(target, duration) {
        this.multiplier = multiplier;
    }

    protected override void Apply() {
        var agent = target.agent;
        originalSpeed = agent.speed;
        agent.speed *= multiplier;
    }

    protected override void End() {
        if (target != null) {
            target.agent.speed = originalSpeed;
        }
    }
}

