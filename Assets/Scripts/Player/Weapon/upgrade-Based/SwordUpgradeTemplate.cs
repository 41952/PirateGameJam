using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SwordUpgrade", menuName = "Weapons/SwordUpgrade")]
public class SwordUpgradeTemplate : WeaponUpgradeTemplate
{
    [SerializeField]
    private List<SwordLevelData> weaponLevels;

    public override IWeaponUpgradeData GetDataForLevel(int level)
    {
        if (level - 1 >= 0 && level - 1 < weaponLevels.Count)
            return weaponLevels[level - 1];
        return null;
    }

    [System.Serializable]
    public class SwordLevelData : UpgradeLevelData
    {

        public override void ApplyTo(WeaponBase weapon)
        {
            base.ApplyTo(weapon);
        }
        public override string GetUpgradeDescription()
        {
            return base.GetUpgradeDescription() + $"";
        }
    }
    

}
