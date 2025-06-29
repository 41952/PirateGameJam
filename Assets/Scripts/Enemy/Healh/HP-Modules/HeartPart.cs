using UnityEngine;

public class HeartHitPart : HitPartBase
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionDamage = 250f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject explosionEffectPrefab;

    private StatType _chosenBuff;

    private void Awake()
    {
        _chosenBuff = GetRandomBuff();
    }

    private StatType GetRandomBuff()
    {
        var values = (StatType[])System.Enum.GetValues(typeof(StatType));
        return values[Random.Range(0, values.Length)];
    }

    public override void TakeDamage(DamageData data)
    {
        // Смертельный урон владельцу сердца
        enemyHealth.ReceiveDamage(999999f, data);

        // Взрыв по области
        Explode();

        // Выдача баффа игроку
        ApplyBuffToPlayer();
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.ReceiveDamage(explosionDamage, new DamageData { baseDamage = explosionDamage });
            }
        }

        if (explosionEffectPrefab)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void ApplyBuffToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.TryGetComponent(out StatsContainer stats))
        {
            const float Duration = 5f;
            switch (_chosenBuff)
            {
                case StatType.Damage:
                    stats.ApplyModifier(new Game.Buffs.DamageBuff(Duration));
                    break;
                case StatType.Speed:
                    stats.ApplyModifier(new Game.Buffs.SpeedBuff(Duration));
                    break;
                case StatType.HealthRegen:
                    stats.ApplyModifier(new Game.Buffs.RegenBuff(Duration));
                    break;
                case StatType.Health:
                    stats.ApplyModifier(new HealthBuff(1.5f, Duration));
                    break;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}
