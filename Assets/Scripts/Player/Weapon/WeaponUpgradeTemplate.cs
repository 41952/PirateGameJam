using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponUpgradeTemplate", menuName = "Weapons/UpgradeTemplate")]

public abstract class WeaponUpgradeTemplate : ScriptableObject
{
    [SerializeField]
    private List<UpgradeLevelData> levels;

    public virtual IWeaponUpgradeData GetDataForLevel(int level)
    {
        if (level - 1 >= 0 && level - 1 < levels.Count)
            return levels[level - 1];
        return null;
    }

    [System.Serializable]
    public class UpgradeLevelData : IWeaponUpgradeData
    {
        public float damage;
        public float fireRate;
        public int magazineSize;
        public float reloadTime;

        public virtual void ApplyTo(WeaponBase weapon)
        {
            weapon.baseDamage = damage;
            weapon.fireRate = fireRate;
            weapon.magazineSize = magazineSize;
            weapon.reloadTime = reloadTime;
            weapon.currentAmmo = magazineSize;
        }
        public virtual string GetUpgradeDescription()
        {
            return
                $"Урон: {damage}\n" +
                $"Скорострельность: {fireRate}/c\n" +
                $"Магазин: {magazineSize}\n" +
                $"Перезарядка: {reloadTime}с";
        }
    }
    
}

