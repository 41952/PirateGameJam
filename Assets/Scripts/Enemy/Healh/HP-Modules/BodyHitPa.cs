using UnityEngine;

public class BodyHitPart : HitPartBase
{
    private void Reset() => damageMultiplier = 1f;

    public override void TakeDamage(DamageData data)
    {
        float finalDamage = data.baseDamage * damageMultiplier;
        data.hitZone = HitZone.Body;
        enemyHealth.ReceiveDamage(finalDamage, data);
    }
}
