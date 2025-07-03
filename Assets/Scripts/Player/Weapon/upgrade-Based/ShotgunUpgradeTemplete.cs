using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShotgunUpgrade", menuName = "Weapons/ShotgunUpgrade")]
public class ShotgunUpgradeTemplate : WeaponUpgradeTemplate
{
    [SerializeField]
    private List<ShotgunLevelData> weaponLevels;

    public override IWeaponUpgradeData GetDataForLevel(int level)
    {
        if (level - 1 >= 0 && level - 1 < weaponLevels.Count)
            return weaponLevels[level - 1];
        return null;
    }

    [System.Serializable]
    public class ShotgunLevelData : UpgradeLevelData
    {
        public int pelletsPerShot = 12;
        public float spreadAngle = 5f;

        public override void ApplyTo(WeaponBase weapon)
        {
            base.ApplyTo(weapon);
            if (weapon is ShotGun f)
            {
                f.pelletsPerShot = pelletsPerShot;
                f.spreadAngle = spreadAngle;
            }
        }
        public override string GetUpgradeDescription()
        {
            return base.GetUpgradeDescription() + $", \n Кол-во дробинок: {pelletsPerShot}, \n Разброс: {spreadAngle}";
        }
    }

}
