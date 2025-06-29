using UnityEngine;

public class DamageStatListener : StatListener
{
    protected override StatType GetStatType() => StatType.Damage;

    protected override void OnStatChanged(StatType type, float newDamage)
    {
        var weapon = GetComponent<WeaponBase>();
        if (weapon != null)
        {
            weapon.baseDamageMultiplier = newDamage;
        }
        else
        {
            Debug.LogWarning("WeaponBase не найден на объекте");
        }
    }
}
