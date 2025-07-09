using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum RewardType
{
    Weapon,
    Equipment,
    WeaponUpgrade,
    EquipmentUpgrade,
    StatBonus
}

[Serializable]
public class RewardOptionData
{
    public RewardType rewardType;

    public WeaponBase weaponPrefab;
    public EquipmentItem equipmentPrefab;

    public int upgradeSlotIndex; // индекс оружия/снаряги, если это апгрейд
    public string displayName;
    public string description;
    public Sprite icon;
}

public class UIRewardOptionBlock : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button chooseButton;

    private RewardOptionData currentData;
    private Action onChosenCallback;

    public void Initialize(RewardOptionData data, Action onChosen)
    {
        currentData = data;
        onChosenCallback = onChosen;

        // Заполнение UI
        iconImage.sprite = data.icon;
        nameText.text = data.displayName;
        descriptionText.text = data.description;

        chooseButton.onClick.RemoveAllListeners();
        chooseButton.onClick.AddListener(OnChoosePressed);
    }

    private void OnChoosePressed()
    {
        ApplyReward(currentData);
        onChosenCallback?.Invoke();
    }

    private void ApplyReward(RewardOptionData data)
    {
        switch (data.rewardType)
        {
            case RewardType.Weapon:
                InventoryManager.Instance.ReplaceWeapon(data.upgradeSlotIndex, data.weaponPrefab);
                break;

            case RewardType.Equipment:
                InventoryManager.Instance.ReplaceEquipment(data.upgradeSlotIndex, data.equipmentPrefab);
                break;

            case RewardType.WeaponUpgrade:
                var weapon = InventoryManager.Instance.weapons[data.upgradeSlotIndex];
                if (weapon != null) weapon.AddLevel();
                break;

            case RewardType.EquipmentUpgrade:
                var equip = InventoryManager.Instance.equipment[data.upgradeSlotIndex];
                if (equip != null) equip.AddLevel();
                break;

            case RewardType.StatBonus:
            {
                // Получаем все доступные типы статов
                var statTypes = System.Enum.GetValues(typeof(StatType)) as StatType[];
                if (statTypes == null || statTypes.Length == 0)
                {
                    Debug.LogWarning("Нет доступных статов для баффа.");
                    break;
                }

                // Случайный выбор стата
                StatType randomStat = statTypes[UnityEngine.Random.Range(0, statTypes.Length)];

                // Получаем сам стат
                var stat = StatsContainer.Instance.GetStat(randomStat);

                // Вычисляем бонус как 10% от текущего финального значения
                float bonus = stat.FinalValue * 0.1f;

                // Применяем как постоянный апгрейд
                stat.AddUpgrade(bonus);

                Debug.Log($"[STAT BONUS] +10% к {randomStat}: +{bonus:F1} (теперь {stat.FinalValue:F1})");
                break;
            }

        }
    }
}
