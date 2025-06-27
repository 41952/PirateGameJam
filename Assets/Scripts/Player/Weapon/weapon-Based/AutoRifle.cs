using UnityEngine;

public class AutoRifle : WeaponBase
{
    [ContextMenu("LevelUP!~")]
    public override void AddLevel()
    {
        level++;
        InventoryManager.Instance.CheckSynergies();
        Debug.Log($"{weaponName} level up to {level}");
        foreach (var s in synergies)
            s.OnLevelUp();
    }
}
