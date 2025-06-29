using UnityEngine;
using System.Collections.Generic;
using System;


    public class Stat
    {
        public StatType Type { get; }
        public float BaseValue { get; private set; }
        public float UpgradedValue { get; private set; }

        private readonly List<StatModifier> _modifiers = new List<StatModifier>();

        public float FinalValue { get; private set; }

        public event Action<StatType, float> OnStatChanged;

        public Stat(StatType type, float baseValue)
        {
            Type = type;
            BaseValue = baseValue;
            UpgradedValue = 0f;
            Recalculate();
        }

        // Прокачка стата навсегда
        public void AddUpgrade(float amount)
        {
            UpgradedValue += amount;
            Recalculate();
        }

        // Добавить временный модификатор
        public void AddModifier(StatModifier mod)
        {
            // Проверяем на существующие однотипные модификаторы
            var existing = _modifiers.Find(m => m.GetType() == mod.GetType());
            if (existing != null)
                existing.Merge(mod);
            else
                _modifiers.Add(mod);

            Recalculate();
        }

        // Убрать модификатор
        public void RemoveModifier(StatModifier mod)
        {
            _modifiers.Remove(mod);
            Recalculate();
        }

        // Вычисляет итоговое значение
        public void Recalculate()
        {
            float basePlus = BaseValue + UpgradedValue;
            float multiplier = 1f;

            foreach (var mod in _modifiers)
            {
                multiplier *= mod.GetModifierValue();
            }

            FinalValue = basePlus * multiplier;
            OnStatChanged?.Invoke(Type, FinalValue);
        }

        // Обновление модификаторов: возвращает список истекших
        public List<StatModifier> TickModifiers(float deltaTime)
        {
            var expired = new List<StatModifier>();
            foreach (var mod in _modifiers)
            {
                if (mod.Tick(deltaTime))
                    expired.Add(mod);
            }
            return expired;
        }
    }

