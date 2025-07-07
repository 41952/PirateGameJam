using UnityEngine;
using System;
using System.Collections.Generic;

public static class MedicTargetRegistry
{
    // Maps target health component to assigned medic
    private static Dictionary<EnemyHealth, MedicAI> assignments = new Dictionary<EnemyHealth, MedicAI>();

    public static bool TryAssign(EnemyHealth target, MedicAI medic)
    {
        if (assignments.ContainsKey(target)) return false;
        assignments[target] = medic;
        return true;
    }

    public static void Unassign(EnemyHealth target)
    {
        if (assignments.ContainsKey(target))
            assignments.Remove(target);
    }

    public static bool IsAssigned(EnemyHealth target)
        => assignments.ContainsKey(target);
}