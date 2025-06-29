using UnityEngine;

    public enum StatType
    {
        Health,
        HealthRegen,
        Speed,
        Damage
    }

    public enum StackingPolicy
    {
        Override,   // сбрасывает время
        StackTime,  // добавляет время
        StackEffect,// увеличивает эффект
        StackBoth   // и эффект, и время
    }


