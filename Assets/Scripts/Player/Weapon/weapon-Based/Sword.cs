using UnityEngine;


public class Sword : WeaponBase
{
    [Header("HitScan Settings")]
    public Transform firePoint; // точка, откуда идёт выстрел
    public float fireRange = 100f;
    public float projectileSpeed;

    [ContextMenu("LevelUP!~")]
    public virtual void AddLevel()
    {
        level++; // <- пусть здесь будет
        InventoryManager.Instance.CheckSynergies();
        Debug.Log($"{weaponName} level up to {level}");

        GetComponent<WeaponLevelApplier>()?.ApplyLevel();

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

        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, fireRange))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 1f);

            HitZone zone = HitZone.Body;


            if (hit.collider.TryGetComponent(out IDamageReceiver receiver))
            {
                
                DamageData data = new DamageData(baseDamage * baseDamageMultiplier, 1f, DamageType.Bullet, zone);
                receiver.TakeDamage(data);
                Debug.Log($"Hit {hit.collider.name} for {baseDamage * baseDamageMultiplier} ({zone})");
            }
        }

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
    }
}
