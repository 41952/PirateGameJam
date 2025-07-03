using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SniperRifleUpgrade", menuName = "Weapons/SniperRifleUpgrade")]
public class SniperRifleUpgradeTemplate : WeaponUpgradeTemplate
{
    [SerializeField]
    private List<SniperRifleLevelData> weaponLevels;

    public override IWeaponUpgradeData GetDataForLevel(int level)
    {
        if (level - 1 >= 0 && level - 1 < weaponLevels.Count)
            return weaponLevels[level - 1];
        return null;
    }

    [System.Serializable]
    public class SniperRifleLevelData : UpgradeLevelData
    {
        public float armorPenetrationMultiplier;

        public override void ApplyTo(WeaponBase weapon)
        {
            base.ApplyTo(weapon);
            if (weapon is SniperRifle f)
            {
                f.armorPenetrationMultiplier = armorPenetrationMultiplier;
            }
        }
        public override string GetUpgradeDescription()
        {
            return base.GetUpgradeDescription() + $", \n Множитель урона по броне: {armorPenetrationMultiplier}";
        }
    }

}
