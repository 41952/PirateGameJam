using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Equipment Data", fileName = "New Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [Header("General")]
    public string equipmentName;
    public StatType targetStat;

    [Header("Level modifiers")]
    [Tooltip("Нулевой элемент = уровень 1 (+X%), первый = уровень 2 и т.д.")]
    [Range(0f, 5f)]
    public List<float> percentPerLevel = new List<float> { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };
}
