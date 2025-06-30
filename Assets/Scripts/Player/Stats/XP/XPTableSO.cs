using UnityEngine;

[CreateAssetMenu(fileName = "XPTable", menuName = "Experience/XPTable", order = 0)]
public class XPTableSO : ScriptableObject
{
    [Tooltip("XP required for each level. Index = level, value = cumulative XP required to reach that level.")]
    public int[] xpThresholds;

    /// <summary>
    /// Get XP required for next level. If at max, returns int.MaxValue.
    /// </summary>
    public int GetXPForLevel(int level)
    {
        if (level < 0 || level >= xpThresholds.Length) return int.MaxValue;
        return xpThresholds[level];
    }

    /// <summary>
    /// Get maximum defined level.
    /// </summary>
    public int MaxLevel => xpThresholds.Length - 1;
}
