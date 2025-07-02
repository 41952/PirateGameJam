using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    [Header("Link to data")]
    [SerializeField] private EquipmentData data;

    [SerializeField] private StatsContainer _statsContainer;
    public string equipmentName;
    public int level = 1;
    public int maxLevel = 6;

    private void Awake()
    {
        if (data == null)
        {
            Debug.LogError($"[{nameof(EquipmentItem)}] No EquipmentData assigned on {gameObject.name}!");
            enabled = false;
            return;
        }

    }

    private void Start() => AddLevel();

    /// <summary>
    /// Вызывается извне, когда предмет должен прокачаться на +1 уровень.
    /// </summary>
    [ContextMenu("LevelUP~")]
    public void AddLevel()
    {
        if (level >= data.percentPerLevel.Count)
        {
            Debug.LogWarning($"{data.equipmentName} уже на максимальном уровне ({level}).");
            return;
        }

        float percent = data.percentPerLevel[level]; // e.g. 0.2 для +20%
        var stat = _statsContainer.GetStat(data.targetStat);

        // берём текущее итоговое значение стата и прибавляем к нему нужный процент
        float bonus = stat.FinalValue * percent;
        stat.AddUpgrade(bonus);

        level++;
        Debug.Log($"[{data.equipmentName}] Level {level} — +{percent * 100f}% ({bonus:F1}) к {data.targetStat}");
    }

    /// <summary>
    /// Если нужно, можно получить текущий уровень предмета.
    /// </summary>
    public int GetLevel() => level;
}

