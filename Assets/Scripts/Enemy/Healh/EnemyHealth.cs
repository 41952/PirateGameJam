using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    private void Awake() => currentHP = maxHP;

    public void ReceiveDamage(float damage, DamageData data)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        GameEvents.RaiseEnemyDamaged(this, damage, data);

        if (currentHP <= 0)
        {
            GameEvents.RaiseEnemyDeath(this);
        }
    }

    public void Die()
    {
        GameEvents.RaiseEnemyDeath(this);
    }

    public float GetHealthPercent() => currentHP / maxHP;
    public float GetCurrentHealth() => currentHP;
}
