using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [Header("XP Configuration")]
    [SerializeField] private XPTableSO xpTable;

    private int currentLevel;
    private int currentXP;

    private void Awake()
    {
        currentLevel = 0;
        currentXP = 0;
    }

    public void AddExperience(int xpAmount)
    {
        if (xpAmount <= 0) return;

        currentXP += xpAmount;
        GameEvents.RaisePlayerXPGained(this, xpAmount);

        while (currentLevel < xpTable.MaxLevel && currentXP >= xpTable.GetXPForLevel(currentLevel + 1))
        {
            currentLevel++;
            GameEvents.RaisePlayerLevelUp(this, currentLevel);
        }
    }

    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPForNextLevel => xpTable.GetXPForLevel(currentLevel + 1) - currentXP;
}
