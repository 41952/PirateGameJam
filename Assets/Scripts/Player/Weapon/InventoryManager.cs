using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public WeaponBase[] weapons = new WeaponBase[2];
    public EquipmentItem[] equipment = new EquipmentItem[2];

    [Header("Available Weapons")]
    public List<WeaponBase> availableWeapons; // Задаётся в инспекторе

    [Header("Holders")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform equipmentHolder;

    [System.Serializable]
    public class SynergyEntry
    {
        public string weaponName;
        public string equipmentName;
        public SynergyBase synergyPrefab;
    }

    public List<SynergyEntry> synergyTable;

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        AssignRandomWeaponToSlot(1); // Например, в слот 1
    }

    public void AssignRandomWeaponToSlot(int index)
    {
        if (availableWeapons == null || availableWeapons.Count == 0)
        {
            Debug.LogWarning("Нет доступных оружий для выбора!");
            return;
        }

        var randomWeapon = availableWeapons[Random.Range(0, availableWeapons.Count)];
        ReplaceWeapon(index, randomWeapon);
    }

    public void ReplaceWeapon(int index, WeaponBase newWeapon)
    {
        if (weapons[index]) Destroy(weapons[index].gameObject);
        weapons[index] = Instantiate(newWeapon, weaponHolder);
        weapons[index].gameObject.SetActive(false); 
        CheckSynergies();
    }

    public void ReplaceEquipment(int index, EquipmentItem newEquipment)
    {
        if (equipment[index]) Destroy(equipment[index].gameObject);
        equipment[index] = Instantiate(newEquipment, equipmentHolder);
        CheckSynergies();
    }

    [ContextMenu("SynergyCheck")]
    public void CheckSynergies()
    {
        foreach (var weapon in weapons)
        {
            if (weapon == null || weapon.level < 6) continue;

            foreach (var eq in equipment)
            {
                if (eq == null || eq.level < 6) continue;

                var match = synergyTable.FirstOrDefault(s =>
                    s.weaponName == weapon.weaponName && s.equipmentName == eq.equipmentName);
                    
                if (match != null)
                {
                    string pairKey = weapon.weaponName + "+" + eq.equipmentName;

                    // Проверка по ключу
                    if (!weapon.synergies.Any(s => s.GetType() == match.synergyPrefab.GetType()))
                    {
                        var synergy = Instantiate(match.synergyPrefab, weapon.transform);
                        weapon.AddSynergy(synergy, eq.equipmentName);
                        Debug.Log($"added synergy {match.synergyPrefab.name} for {weapon.weaponName} + {eq.equipmentName}");
                    }
                }

            }
        }
    }
}

