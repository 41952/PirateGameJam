using UnityEngine;

public abstract class StatListener : MonoBehaviour
    {
        protected Stat BoundStat;

        private void OnEnable()
        {
            StartCoroutine(WaitAndSubscribe());
        }

        private System.Collections.IEnumerator WaitAndSubscribe()
        {
            yield return null; // подождать 1 кадр — Awake() точно вызовется
            if (BoundStat == null)
                BoundStat = StatsContainer.Instance.GetStat(GetStatType());

            GameEvents.OnStatChanged += HandleStatChanged;
            OnStatChanged(GetStatType(), BoundStat.FinalValue);
        }


        protected virtual void OnDisable()
        {
            if (BoundStat != null)
                GameEvents.OnStatChanged -= HandleStatChanged;
        }
        private void HandleStatChanged(StatsContainer source, StatType type, float newValue)
        {
            if (source != StatsContainer.Instance) return;
            if (type != GetStatType()) return;

            OnStatChanged(type, newValue);
        }

        // Должен возвращать тип стата для подписки
    protected abstract StatType GetStatType();
        // Вызывается при изменении стата
        protected abstract void OnStatChanged(StatType type, float newValue);
    }

