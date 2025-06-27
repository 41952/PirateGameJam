using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Статы
    private float health = 100f;
    private float healthRegen = 1f;
    private float speed = 5f;
    private float damage = 10f;

    public event Action OnStatsChanged;
    public float Health
    {
        get => health;
        private set
        {
            if (health != value)
            {
                health = value;
                NotifyStatsChanged();
            }
        }
    }

    public float HealthRegen
    {
        get => healthRegen;
        private set
        {
            if (healthRegen != value)
            {
                healthRegen = value;
                NotifyStatsChanged();
            }
        }
    }

    public float Speed
    {
        get => speed;
        private set
        {
            if (speed != value)
            {
                speed = value;
                NotifyStatsChanged();
            }
        }
    }

    public float Damage
    {
        get => damage;
        private set
        {
            if (damage != value)
            {
                damage = value;
                NotifyStatsChanged();
            }
        }
    }

    private void NotifyStatsChanged()
    {
        OnStatsChanged?.Invoke();
    }
    public void UpgradeHealth(float amount)
    {
        Health += amount;
    }

    public void UpgradeHealthRegen(float amount)
    {
        HealthRegen += amount;
    }

    public void UpgradeSpeed(float amount)
    {
        Speed += amount;
    }

    public void UpgradeDamage(float amount)
    {
        Damage += amount;
    }

    public (float health, float regen, float speed, float damage) GetAllStats()
    {
        return (health, healthRegen, speed, damage);
    }
}
