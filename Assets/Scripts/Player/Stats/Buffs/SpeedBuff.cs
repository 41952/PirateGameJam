using UnityEngine;
namespace Game.Buffs
{
    public class SpeedBuff : StatModifier
    {
        public SpeedBuff(float duration)
            : base(StatType.Speed, duration, StackingPolicy.StackBoth)
        {
        }

        public override float GetModifierValue() => 2f;
    }
}

