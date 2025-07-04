using UnityEngine;
public class ArmorHitPart : HitPartBase
{
    [SerializeField] private float armorHP = 50f;
    [SerializeField] private float damageThroughPercent = 0.05f;

    public override void TakeDamage(DamageData data)
    {
        float damageToArmor = data.baseDamage * data.armorPenetration;

        if (armorHP > 0)
        {
            armorHP -= damageToArmor;
            float passThrough = data.baseDamage * damageThroughPercent;
            if(data.armorPenetration == -1f)
                enemyHealth.ReceiveDamage(data.baseDamage, data);
            else
                enemyHealth.ReceiveDamage(passThrough, data);
        }
        else
        {
            enemyHealth.ReceiveDamage(data.baseDamage, data);
        }
    }
}
