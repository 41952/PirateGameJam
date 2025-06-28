using UnityEngine;
public abstract class HitPartBase : MonoBehaviour, IDamageReceiver
{
    [SerializeField] protected EnemyHealth enemyHealth;
    [SerializeField] protected float damageMultiplier = 1f;
    public abstract void TakeDamage(DamageData data);
}
