using System;
using UnityEngine;
public static class GameEvents
{
    // реализация статик эвентов по просьбе AshCrow

    // дамаг
    public static event Action<EnemyHealth, float, DamageData> OnEnemyDamaged;
    public static event Action<EnemyHealth> OnEnemyDeath;

    public static void RaiseEnemyDamaged(EnemyHealth target, float amount, DamageData data)
    {
        OnEnemyDamaged?.Invoke(target, amount, data);
    }

    public static void RaiseEnemyDeath(EnemyHealth target)
    {
        OnEnemyDeath?.Invoke(target);
    }
    // статы
    public static event Action<StatsContainer, StatType, float> OnStatChanged;

    public static void RaiseStatChanged(StatsContainer owner, StatType type, float value)
    {
        OnStatChanged?.Invoke(owner, type, value);
    }
    // опыт
    public static event Action<PlayerExperience, int> OnPlayerXPGained;

    public static event Action<PlayerExperience, int> OnPlayerLevelUp;

    public static void RaisePlayerXPGained(PlayerExperience playerXP, int amount)
    {
        OnPlayerXPGained?.Invoke(playerXP, amount);
    }

    public static void RaisePlayerLevelUp(PlayerExperience playerXP, int newLevel)
    {
        OnPlayerLevelUp?.Invoke(playerXP, newLevel);
    }

    // апргейт менюшки
    public static event Action OnUpgradeMenuClosed;

    public static void RaiseUpgradeMenuClosed()
    {
        OnUpgradeMenuClosed?.Invoke();
    }

    // смена оружия
    public static event Action<int, WeaponBase> OnWeaponSwitched;

    public static void RaiseWeaponSwitched(int newIndex, WeaponBase weapon)
    {
        OnWeaponSwitched?.Invoke(newIndex, weapon);
    }
    public static Action<bool> OnUltimateStateChanged;

    public static void RaiseUltimateStateChanged(bool active)
    {
        OnUltimateStateChanged?.Invoke(active);
    }

    // спавнер волн
    public static Action<int> OnWaveStarted;
    public static Action<int> OnWaveEnded;
    public static Action<GameObject> OnEnemySpawned;
    public static Action OnAllWavesCompleted;

}
