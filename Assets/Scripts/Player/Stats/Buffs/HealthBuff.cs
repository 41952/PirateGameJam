using UnityEngine;


public class HealthBuff : StatModifier
{
    private readonly float _multiplier;
    private bool _applied = false;

    public HealthBuff(float multiplier, float duration)
        : base(StatType.Health, duration, StackingPolicy.StackBoth)
    {
        _multiplier = multiplier;
    }

    public override float GetModifierValue() => _multiplier;

    // Вызывается, когда модификатор добавлен (один раз)
    public void OnApplied(GameObject target)
    {
        if (_applied) return;
        _applied = true;

        if (target.TryGetComponent(out PlayerHealthSystem health))
        {
            // Устанавливаем текущее ХП = новому максимуму
            health.SetCurrentHealthToMax();
        }
    }

    protected override void CombineEffect(StatModifier other)
    {
        if (other is HealthBuff hb)
        {
            // Можем модифицировать эффект, если нужно — пока оставим как есть
        }
    }
}
