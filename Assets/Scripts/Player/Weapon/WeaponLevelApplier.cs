using UnityEngine;

[RequireComponent(typeof(WeaponBase))]
public class WeaponLevelApplier : MonoBehaviour
{
    public WeaponUpgradeTemplate upgradeData;
    private WeaponBase weapon;

    private void Awake()
    {
        weapon = GetComponent<WeaponBase>();
        ApplyLevel();
    }

    public void ApplyLevel()
    {
        var data = upgradeData?.GetDataForLevel(weapon.level);
        if (data != null)
            data.ApplyTo(weapon);
    }


    public void OnWeaponLevelUp()
    {
        ApplyLevel();
    }
}
