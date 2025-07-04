using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(StatsContainer))]
public class PlayerHealthSystem : MonoBehaviour
{
    private StatsContainer _stats;
    private Stat _healthStat;
    private Stat _regenStat;

    private float _currentHealth;

    private void Start()
    {
        _stats = GetComponent<StatsContainer>();
        _healthStat = _stats.GetStat(StatType.Health);
        _regenStat = _stats.GetStat(StatType.HealthRegen);

        _currentHealth = _healthStat.FinalValue;
    }

    private void OnEnable()
    {
        _stats = GetComponent<StatsContainer>();
        GameEvents.OnStatChanged += OnStatChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnStatChanged -= OnStatChanged;
    }

    private void OnStatChanged(StatsContainer source, StatType type, float newValue)
    {
        if (source != _stats || type != StatType.Health) return;

        _currentHealth = newValue;
    }

    public void SetCurrentHealthToMax()
    {
        _currentHealth = _healthStat.FinalValue;
    }

    private void Update()
    {
        // реген
        if (_currentHealth < _healthStat.FinalValue)
        {
            _currentHealth += _regenStat.FinalValue * Time.deltaTime;
            _currentHealth = Mathf.Min(_currentHealth, _healthStat.FinalValue);
            GameEvents.RaisePlayerHealthChanged(_currentHealth, _healthStat.FinalValue);
        }
        if (_currentHealth <= 1f)
            Die();
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0f);
    }

    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxHealth() => _healthStat.FinalValue;

    private void Die()
    {
        // рестарт уровня
        SceneManager.LoadScene(0);
    }
}
