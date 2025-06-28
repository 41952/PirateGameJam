using UnityEngine;
public class HeadHitPart : HitPartBase
{
    private void Reset() => damageMultiplier = 2f;

    public override void TakeDamage(DamageData data)
    {
        float finalDamage = data.baseDamage * damageMultiplier;
        data.hitZone = HitZone.Head;
        enemyHealth.ReceiveDamage(finalDamage, data);
    }
}
