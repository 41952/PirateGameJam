using UnityEngine;
namespace Game.Buffs
{
    // Бафф на регенерацию здоровья: x4 на 5 сек
    public class RegenBuff : StatModifier
    {
        public RegenBuff(float duration)
            : base(StatType.HealthRegen, duration, StackingPolicy.StackBoth)
        {
        }

        public override float GetModifierValue() => 4f;
    }
}
