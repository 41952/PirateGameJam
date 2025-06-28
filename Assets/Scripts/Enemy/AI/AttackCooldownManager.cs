using UnityEngine;
using System;
/// <summary>
/// Manages a shared cooldown between all melee attacks.
/// </summary>
public static class AttackCooldownManager
{
    public static float SharedCooldown = 4f;      // Time others wait after any attack
    private static float lastGlobalAttackTime = Mathf.NegativeInfinity;

    /// <summary>Can any enemy perform a new attack?</summary>
    public static bool CanGlobalAttack() => Time.time - lastGlobalAttackTime >= SharedCooldown;

    /// <summary>Notify manager that a melee attack happened now.</summary>
    public static void NotifyGlobalAttack()
    {
        lastGlobalAttackTime = Time.time;
    }
}