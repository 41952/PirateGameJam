using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public WeaponBase[] weapons = new WeaponBase[2];
    public EquipmentItem[] equipment = new EquipmentItem[2];

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

                if (match != null && !weapon.GetComponent(match.synergyPrefab.GetType()))
                {
                    var synergy = Instantiate(match.synergyPrefab, weapon.transform);
                    weapon.AddSynergy(synergy);
                    Debug.Log($"added synergy {match.synergyPrefab.name} for {weapon.weaponName} + {eq.equipmentName}");
                }
            }
        }
    }
}
