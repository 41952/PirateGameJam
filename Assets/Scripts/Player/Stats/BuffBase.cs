using UnityEngine;

    public abstract class StatModifier
    {
        public StatType TargetStat { get; }
        public float Duration { get; private set; }
        public StackingPolicy Policy { get; }

        protected StatModifier(StatType targetStat, float duration, StackingPolicy policy)
        {
            TargetStat = targetStat;
            Duration = duration;
            Policy = policy;
        }

        // Возвращает множитель или бонус к базовому значению
        public abstract float GetModifierValue();

        // Уменьшает оставшееся время эффекта
        public bool Tick(float deltaTime)
        {
            Duration -= deltaTime;
            return Duration <= 0f;
        }

        // Обновляет этот модификатор при повторном наложении
        public void Merge(StatModifier incoming)
        {
            switch (Policy)
            {
                case StackingPolicy.Override:
                    Duration = incoming.Duration;
                    break;
                case StackingPolicy.StackTime:
                    Duration += incoming.Duration;
                    break;
                case StackingPolicy.StackEffect:
                    CombineEffect(incoming);
                    break;
                case StackingPolicy.StackBoth:
                    Duration += incoming.Duration;
                    CombineEffect(incoming);
                    break;
            }
        }

        // Для полей, изменяющих эффект (например, множитель)
        protected virtual void CombineEffect(StatModifier other) { }
    }

