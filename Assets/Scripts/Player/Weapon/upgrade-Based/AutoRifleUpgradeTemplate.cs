using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AutoRifleUpgrade", menuName = "Weapons/AutoRifleUpgrade")]
public class AutoRifleUpgradeTemplate : WeaponUpgradeTemplate
{
    [SerializeField]
    private List<AutoRifleLevelData> weaponLevels;

    public override IWeaponUpgradeData GetDataForLevel(int level)
    {
        if (level - 1 >= 0 && level - 1 < weaponLevels.Count)
            return weaponLevels[level - 1];
        return null;
    }

    [System.Serializable]
    public class AutoRifleLevelData : UpgradeLevelData
    {
        public float projectileSpeed;

        public override void ApplyTo(WeaponBase weapon)
        {
            base.ApplyTo(weapon);
            if (weapon is AutoRifle f)
            {

                f.projectileSpeed = projectileSpeed;
            }
        }
    }
}
