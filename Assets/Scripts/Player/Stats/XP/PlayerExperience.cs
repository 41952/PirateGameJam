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
        GameEvents.RaisePlayerXPGained(this, currentXP);
    }

    public void AddExperience(int xpAmount)
    {
        if (xpAmount <= 0) return;

        currentXP += xpAmount;
        

        while (currentLevel < xpTable.MaxLevel && currentXP >= xpTable.GetXPForLevel(currentLevel + 1))
        {
            currentLevel++;
            GameEvents.RaisePlayerLevelUp(this, currentLevel);
        }
        GameEvents.RaisePlayerXPGained(this, xpAmount);
    }

    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPForNextLevel => xpTable.GetXPForLevel(currentLevel + 1) - currentXP;
    public int MaxPXForNextLevel => xpTable.GetXPForLevel(currentLevel + 1);
    public int XPForCurrentLevel => xpTable.GetXPForLevel(currentLevel);
}
