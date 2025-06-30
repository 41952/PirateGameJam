using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyExperienceDealer : MonoBehaviour
{
    [Header("XP Reward Settings")]
    [Tooltip("Base XP granted when this enemy is killed.")]
    public int baseXP = 50;

    [Tooltip("Multiplier applied for each hit zone.")]
    public HitZoneMultipliers zoneMultipliers;

    [Tooltip("Multiplier applied for each damage type.")]
    public DamageTypeMultipliers typeMultipliers;

    private EnemyHealth health;
    private DamageData lastHitData;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();

    }

    private void OnEnable()
    {
        Debug.Log("Subscribed to GameEvents");
        GameEvents.OnEnemyDamaged += HandleEnemyDamaged;
        GameEvents.OnEnemyDeath += HandleEnemyDeath;
    }


    private void OnDisable()
    {
        GameEvents.OnEnemyDamaged -= HandleEnemyDamaged;
        GameEvents.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDamaged(EnemyHealth target, float damage, DamageData data)
    {
        if (target == null || target.gameObject != gameObject)
            return;

        lastHitData = data; // сохраняем последнее попадание
    }

    private void HandleEnemyDeath(EnemyHealth target)
    {
        if (target == null || target.gameObject != gameObject)
            return;
        float totalMultiplier = 1f;

        if (zoneMultipliers.TryGetMultiplier(lastHitData.hitZone, out float zoneMult))
            totalMultiplier += zoneMult;

        if (typeMultipliers.TryGetMultiplier(lastHitData.type, out float typeMult))
            totalMultiplier += typeMult;

        int xpAward = Mathf.RoundToInt(baseXP * totalMultiplier);

        PlayerExperience playerXp = FindObjectOfType<PlayerExperience>();
        if (playerXp != null)
        {
            playerXp.AddExperience(xpAward);
        }
    }
}

[Serializable]
public struct HitZoneMultiplierEntry
{
    public HitZone zone;
    public float multiplier;
}

[Serializable]
public class HitZoneMultipliers
{
    public HitZoneMultiplierEntry[] entries;

    public bool TryGetMultiplier(HitZone zone, out float multiplier)
    {
        foreach (var entry in entries)
        {
            if (entry.zone == zone)
            {
                multiplier = entry.multiplier;
                return true;
            }
        }
        multiplier = 0f;
        return false;
    }
}

[Serializable]
public struct DamageTypeMultiplierEntry
{
    public DamageType type;
    public float multiplier;
}

[Serializable]
public class DamageTypeMultipliers
{
    public DamageTypeMultiplierEntry[] entries;

    public bool TryGetMultiplier(DamageType type, out float multiplier)
    {
        foreach (var entry in entries)
        {
            if (entry.type == type)
            {
                multiplier = entry.multiplier;
                return true;
            }
        }
        multiplier = 0f;
        return false;
    }
}
