using UnityEngine;
using UnityEngine.Events;
using System;
public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    public event Action<float, DamageData> OnDamageTaken;
    public event Action OnDeath;

    private void Awake() => currentHP = maxHP;

    public void ReceiveDamage(float damage, DamageData data)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        OnDamageTaken?.Invoke(damage, data);

        if (currentHP <= 0)
            OnDeath?.Invoke();
    }

    public float GetHealthPercent() => currentHP / maxHP;
    public float GetCurrentHealth() => currentHP;
}

