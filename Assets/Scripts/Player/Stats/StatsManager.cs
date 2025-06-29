using System.Collections.Generic;
using UnityEngine;

    public class StatsContainer : MonoBehaviour
    {
        private readonly Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();

        private void Awake()
        {
            // Инициализация базовых статов (примеры значений)
            _stats[StatType.Health] = new Stat(StatType.Health, 100f);
            _stats[StatType.HealthRegen] = new Stat(StatType.HealthRegen, 1f);
            _stats[StatType.Speed] = new Stat(StatType.Speed, 1f);
            _stats[StatType.Damage] = new Stat(StatType.Damage, 1f);
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            // Тикаем модификаторы и удаляем истекшие
            foreach (var stat in _stats.Values)
            {
                var expired = stat.TickModifiers(dt);
                foreach (var mod in expired)
                    stat.RemoveModifier(mod);
            }
        }

        // Получить конкретный стат
        public Stat GetStat(StatType type)
        {
            return _stats[type];
        }

        // Применить бафф к стату
        public void ApplyModifier(StatModifier mod)
        {
            if (_stats.TryGetValue(mod.TargetStat, out var stat))
            {
                stat.AddModifier(mod);

                // Специальный хук для баффов, которым нужно знать об объекте
                if (mod is HealthBuff hb)
                {
                    hb.OnApplied(gameObject);
                }
            }
            else
            {
                Debug.LogWarning($"Stat {mod.TargetStat} not found on {gameObject.name}");
            }
        }

    }

