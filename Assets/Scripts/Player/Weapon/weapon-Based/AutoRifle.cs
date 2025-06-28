using UnityEngine;


public class AutoRifle : WeaponBase
{
    [Header("HitScan Settings")]
    public Transform firePoint; // точка, откуда идёт выстрел
    public float fireRange = 100f;

    [ContextMenu("LevelUP!~")]
    public override void AddLevel()
    {
        level++;
        InventoryManager.Instance.CheckSynergies();
        Debug.Log($"{weaponName} level up to {level}");
        foreach (var s in synergies)
            s.OnLevelUp();
    }

    public override void Fire()
    {
        if (Time.time < lastFireTime + 1f / fireRate || isReloading || currentAmmo <= 0)
            return;

        currentAmmo--;
        lastFireTime = Time.time;

        Debug.Log($"{weaponName} shoot!~ Current Ammo: {currentAmmo}");

        // 🔫 HitScan логика
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, fireRange))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 1f);

            HitZone zone = HitZone.Body;


            if (hit.collider.TryGetComponent(out IDamageReceiver receiver))
            {
                DamageData data = new DamageData(baseDamage, 1f, DamageType.Bullet, zone);
                receiver.TakeDamage(data);
                Debug.Log($"Hit {hit.collider.name} for {baseDamage} ({zone})");
            }
        }

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
    }
}
