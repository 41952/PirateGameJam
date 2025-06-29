using UnityEngine;
namespace Game.Buffs
{
    public class DamageBuff : StatModifier
    {
        public DamageBuff(float duration)
            : base(StatType.Damage, duration, StackingPolicy.StackBoth)
        {
        }

        public override float GetModifierValue() => 2f;
    }
}