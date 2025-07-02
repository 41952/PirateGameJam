using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardGenerator : MonoBehaviour
{
    [Serializable]
    public class WeaponEntry
    {
        public string weaponName;
        public WeaponBase prefab;

        [TextArea] public string baseDescription;
    }

    [Serializable]
    public class EquipmentEntry
    {
        public string equipmentName;
        public EquipmentItem prefab;
        [TextArea] public string baseDescription;
    }

    [Header("Available Rewards")]
    public List<WeaponEntry> allWeapons = new();
    public List<EquipmentEntry> allEquipment = new();

    [Header("Icons & Defaults")]
    public Sprite defaultWeaponIcon;
    public Sprite defaultEquipmentIcon;
    public Sprite statBonusIcon;

    [Header("Reward Settings")]
    public int maxLevelPerSlot = 6;

    [ContextMenu("Test Generate Rewards")]
    public void TestGenerateRewards()
    {
        var rewards = GenerateRewardOptions();

        Debug.Log("Generated Rewards:");
        foreach (var r in rewards)
        {
            Debug.Log($"• {r.rewardType} | {r.displayName} | {r.description}");
        }
    }

    public List<RewardOptionData> GenerateRewardOptions()
    {
        List<RewardOptionData> result = new();

        var inventory = InventoryManager.Instance;
        var weapons = inventory.weapons;
        var equipment = inventory.equipment;

        bool allSlotsFilled = weapons.All(w => w != null) && equipment.All(e => e != null);
        int maxedEquipCount = equipment.Count(e => e != null && e.GetLevel() >= maxLevelPerSlot);

        // Если прокачано всё (2 оружия + 2 экипа до макс)
        bool allMaxed = weapons.All(w => w != null && w.GetLevel() >= maxLevelPerSlot) &&
                        equipment.All(e => e != null && e.GetLevel() >= maxLevelPerSlot);

        if (allMaxed)
        {
            // Предложить 3 бонуса к статам
            for (int i = 0; i < 3; i++)
            {
                result.Add(new RewardOptionData
                {
                    rewardType = RewardType.StatBonus,
                    displayName = "Усиление характеристики",
                    description = "+10% к случайному стату (заглушка)",
                    icon = statBonusIcon
                });
            }

            return result;
        }

        // Варианты для добавления нового оружия
        if (weapons.Any(w => w == null))
        {
            var newWeapon = GetRandomUnusedWeapon(weapons);
            if (newWeapon != null)
            {
                var weaponEntry = allWeapons.FirstOrDefault(w => w.prefab == newWeapon);
                string description = weaponEntry != null ? weaponEntry.baseDescription : "Описание недоступно";
                result.Add(new RewardOptionData
                {
                    rewardType = RewardType.Weapon,
                    displayName = $"Новое оружие: {newWeapon.weaponName}",
                    description = description,
                    icon = defaultWeaponIcon,
                    weaponPrefab = newWeapon,
                    upgradeSlotIndex = Array.IndexOf(weapons, null)
                });
            }
        }

        // Варианты для добавления экипировки
        if (equipment.Any(e => e == null))
        {
            var newEquip = GetRandomUnusedEquipment(equipment);
            if (newEquip != null)
            {
                var equipmentEntry = allEquipment.FirstOrDefault(w => w.prefab == newEquip);
                string description = equipmentEntry != null ? equipmentEntry.baseDescription : "Описание недоступно";
                result.Add(new RewardOptionData
                {
                    rewardType = RewardType.Equipment,
                    displayName = $"Новое снаряжение: {newEquip.equipmentName}",
                    description = description,
                    icon = defaultEquipmentIcon,
                    equipmentPrefab = newEquip,
                    upgradeSlotIndex = Array.IndexOf(equipment, null)
                });
            }
        }

        // Добавить апгрейды существующего оружия/снаряги
        foreach (var (weapon, i) in weapons.Select((w, i) => (w, i)))
        {
            if (weapon != null && weapon.GetLevel() < maxLevelPerSlot)
            {
                var applier = weapon.GetComponent<WeaponLevelApplier>();
                result.Add(new RewardOptionData
                {
                    rewardType = RewardType.WeaponUpgrade,
                    displayName = $"{weapon.weaponName} → уровень {weapon.GetLevel() + 1}",
                    description = applier ? applier.GetNextLevelDescription() : "Нет данных об улучшении",
                    icon = defaultWeaponIcon,
                    upgradeSlotIndex = i
                });
            }
        }

        foreach (var (equip, i) in equipment.Select((e, i) => (e, i)))
        {
            if (equip != null && equip.GetLevel() < maxLevelPerSlot)
            {
                // Находим соответствующий EquipmentEntry
                var equipmentEntry = allEquipment.FirstOrDefault(e => e.prefab.GetType() == equip.GetType());
                string description = equipmentEntry != null ? equipmentEntry.baseDescription : "Описание недоступно";

                result.Add(new RewardOptionData
                {
                    rewardType = RewardType.EquipmentUpgrade,
                    displayName = $"Улучшение: {equip.equipmentName} → уровень {equip.GetLevel() + 1}",
                    description = description,
                    icon = defaultEquipmentIcon,
                    equipmentPrefab = equip, // Важно! Нужно указать prefab, иначе в апгрейде не поймёшь, кого качать
                    upgradeSlotIndex = i
                });
            }
        }


        // Ограничения по числу наград
        if (result.Count > 3)
        {
            // Гарантировать как минимум 1 оружие и 1 экип, если есть
            var guaranteed = new List<RewardOptionData>();

            var weaponReward = result.FirstOrDefault(r => r.rewardType == RewardType.Weapon);
            var equipReward = result.FirstOrDefault(r => r.rewardType == RewardType.Equipment);

            if (weaponReward != null) guaranteed.Add(weaponReward);
            if (equipReward != null) guaranteed.Add(equipReward);

            // Остальные случайные, пока не будет 3
            var pool = result.Except(guaranteed).OrderBy(x => UnityEngine.Random.value).ToList();
            while (guaranteed.Count < 3 && pool.Any())
            { 
                var last = pool[pool.Count - 1];
                pool.RemoveAt(pool.Count - 1);
                guaranteed.Add(last);
            }

            result = guaranteed;
        }

        return result;
    }

    private WeaponBase GetRandomUnusedWeapon(WeaponBase[] current)
    {
        var currentNames = current.Where(w => w != null).Select(w => w.weaponName).ToHashSet();
        var candidates = allWeapons.Where(w => !currentNames.Contains(w.weaponName)).ToList();

        if (candidates.Count == 0) return null;
        return candidates[UnityEngine.Random.Range(0, candidates.Count)].prefab;
    }

    private EquipmentItem GetRandomUnusedEquipment(EquipmentItem[] current)
    {
        var currentNames = current.Where(e => e != null).Select(e => e.equipmentName).ToHashSet();
        var candidates = allEquipment.Where(e => !currentNames.Contains(e.equipmentName)).ToList();

        if (candidates.Count == 0) return null;
        return candidates[UnityEngine.Random.Range(0, candidates.Count)].prefab;
    }
}

