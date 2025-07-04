using UnityEngine;


public abstract class IEffect {
    public float Duration { get; protected set; }
    protected float elapsed;
    protected EnemyAI target;

    public IEffect(EnemyAI target, float duration) {
        this.target = target;
        this.Duration = duration;
        this.elapsed = 0f;
    }

    // Called once when effect starts
    public void Start() {
        Apply();
        // notify graphics
        var gfx = target.GetComponent<EnemyGraphics>();
        gfx?.ActivateBuffOutline();
    }

    // Called once when effect ends
    protected void Finish() {
        // notify graphics
        var gfx = target.GetComponent<EnemyGraphics>();
        gfx?.DeactivateBuffOutline();
        End();
    }

    // Called every frame
    public virtual void Tick(float delta) {
        elapsed += delta;
        if (elapsed >= Duration) {
            Finish();
        }
    }

    // Effect-specific apply
    protected abstract void Apply();
    // Effect-specific clean-up
    protected abstract void End();
}
