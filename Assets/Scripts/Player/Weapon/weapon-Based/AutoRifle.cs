using UnityEngine;


public class AutoRifle : WeaponBase
{
    [Header("HitScan Settings")]
    [SerializeField] private GameObject projectilePrefab;
    public float projectileSpeed = 30f;
    public float projectileSpread = 1.2f; // множитель разброса (по оси X и Y)


    [Header("Melee Settings")]
    public float meleeRange = 3f;
    public float meleeAngle = 60f; // угол конуса

    [ContextMenu("LevelUP!~")]
    public override void AddLevel()
    {
        base.AddLevel();
    }

    public override void Fire()
    {

        if (Time.time < lastFireTime + 1f / fireRate || isReloading || currentAmmo <= 0)
        {

            return;
        }
            

        currentAmmo--;
        GameEvents.RaiseAmmoChanged(currentAmmo, magazineSize);
        lastFireTime = Time.time;

        Vector3 direction = GetSpreadDirection(firePoint.forward, projectileSpread);

        GameObject bulletGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        if (bulletGO.TryGetComponent(out ProjectileBullet bullet))
        {
            bullet.Initialize(projectileSpeed, baseDamage * baseDamageMultiplier, direction);
        }
        cameraScript.PlayRecoil(recoilAmout, recoilDelay);
        cameraScript.PlayFireShake(shakeAmout, shakeDelay);

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
    }

    private Vector3 GetSpreadDirection(Vector3 forward, float spread)
    {
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 spreadDir = Quaternion.Euler(y, x, 0) * forward;
        return spreadDir.normalized;
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
}
