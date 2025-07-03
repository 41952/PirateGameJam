using UnityEngine;

public class Sword : WeaponBase
{
    [Header("Melee Settings")]
    public Transform firePoint;
    public float attackRange = 3f;
    public float attackAngle = 60f;

    private void Awake()
    {
        if (firePoint == null && Camera.main != null)
            firePoint = Camera.main.transform;
    }

    public override void Fire()
    {
        if (Time.time < lastFireTime + 1f / fireRate || isReloading)
            return;

        lastFireTime = Time.time;

        Debug.Log($"{weaponName} sword swing!");

        Collider[] hits = Physics.OverlapSphere(firePoint.position, attackRange);
        int targetsHit = 0;

        foreach (var hit in hits)
        {
            Vector3 dirToTarget = (hit.transform.position - firePoint.position).normalized;
            float angle = Vector3.Angle(firePoint.forward, dirToTarget);

            if (angle <= attackAngle / 2)
            {
                if (hit.TryGetComponent(out IDamageReceiver receiver))
                {
                    DamageData data = new DamageData(baseDamage * baseDamageMultiplier, 1f, DamageType.Melee, HitZone.Body);
                    receiver.TakeDamage(data);
                    Debug.Log($"Sword hit {hit.name} for {data.baseDamage}");
                    targetsHit++;
                }
            }
        }

        foreach (var s in synergies)
            s.OnFire();
    }

    [ContextMenu("LevelUP!~")]
    public override void AddLevel()
    {
        base.AddLevel();
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.red;
        Vector3 origin = firePoint.position;
        Vector3 forward = firePoint.forward;

        // Нарисуем сектор (визуализация конуса)
        int steps = 20;
        float angleStep = attackAngle / steps;

        for (int i = 0; i <= steps; i++)
        {
            float angle = -attackAngle / 2 + angleStep * i;
            Quaternion rot = Quaternion.AngleAxis(angle, firePoint.up);
            Vector3 dir = rot * forward;
            Gizmos.DrawRay(origin, dir * attackRange);
        }

        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawWireSphere(origin, attackRange);
    }
}
