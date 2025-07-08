using UnityEngine;

public class ShotGun : WeaponBase
{
    [Header("HitScan Settings")]
    public float fireRange = 100f;

    [Header("Shotgun Settings")]
    public int pelletsPerShot = 12;
    public float spreadAngle = 5f;

    [Header("Melee Settings")]
    public float meleeRange = 3f;
    public float meleeAngle = 60f;

    public override void Fire()
    {
        if (Time.time < lastFireTime + 1f / fireRate || isReloading || currentAmmo <= 0)
            return;

        currentAmmo--;
        lastFireTime = Time.time;

        Debug.Log($"{weaponName} shoot!~ Current Ammo: {currentAmmo}");

        for (int i = 0; i < pelletsPerShot; i++)
        {
            Vector3 spreadDir = GetSpreadDirection(firePoint.forward, spreadAngle);
            if (Physics.Raycast(firePoint.position, spreadDir, out RaycastHit hit, fireRange))
            {
                Debug.DrawLine(firePoint.position, hit.point, Color.yellow, 1f);

                if (hit.collider.TryGetComponent(out IDamageReceiver receiver))
                {
                    DamageData data = new DamageData(baseDamage * baseDamageMultiplier, 1f, DamageType.Bullet, HitZone.Body);
                    receiver.TakeDamage(data);
                    Debug.Log($"Pellet hit {hit.collider.name} for {data.baseDamage}");
                }
            }
        }

        cameraScript.PlayRecoil(recoilAmout, recoilDelay);
        cameraScript.PlayFireShake(shakeAmout, shakeDelay);

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
    }


    private Vector3 GetSpreadDirection(Vector3 forward, float angle)
    {
        // Случайное отклонение в пределах заданного угла
        float yaw = Random.Range(-angle, angle);
        float pitch = Random.Range(-angle, angle);
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        return rot * forward;
    }

    public override void MeleeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(firePoint.position, meleeRange);
        int targetsHit = 0;

        foreach (var hit in hits)
        {
            Vector3 dirToTarget = (hit.transform.position - firePoint.position).normalized;
            float angle = Vector3.Angle(firePoint.forward, dirToTarget);

            if (angle <= meleeAngle / 2)
            {
                if (hit.TryGetComponent(out IDamageReceiver receiver))
                {
                    DamageData data = new DamageData(meleeDamage, 1f, DamageType.Melee, HitZone.Body);
                    receiver.TakeDamage(data);
                    Debug.Log($"Melee hit {hit.name} for {meleeDamage}");
                    targetsHit++;
                }
            }
        }
    }

    [ContextMenu("LevelUP!~")]
    public override void AddLevel()
    {
        base.AddLevel();
    }
}
