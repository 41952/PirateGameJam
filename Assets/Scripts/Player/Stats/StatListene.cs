using UnityEngine;

public abstract class StatListener : MonoBehaviour
    {
        [SerializeField] private StatsContainer StatsContainer;
        protected Stat BoundStat;

        private void OnEnable()
        {
            StartCoroutine(WaitAndSubscribe());
        }

        private System.Collections.IEnumerator WaitAndSubscribe()
        {
            yield return null; // подождать 1 кадр — Awake() точно вызовется
            if (BoundStat == null)
                BoundStat = StatsContainer.GetStat(GetStatType());

            BoundStat.OnStatChanged += OnStatChanged;
            OnStatChanged(GetStatType(), BoundStat.FinalValue);
}


        protected virtual void OnDisable()
        {
            if (BoundStat != null)
                BoundStat.OnStatChanged -= OnStatChanged;
        }

        // Должен возвращать тип стата для подписки
        protected abstract StatType GetStatType();
        // Вызывается при изменении стата
        protected abstract void OnStatChanged(StatType type, float newValue);
    }

