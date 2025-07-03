using UnityEngine;
using DG.Tweening;

public class SniperRifle : WeaponBase
{
    [Header("HitScan Settings")]
    private Transform firePoint;
    public float fireRange = 100f;

    [Header("Sniper Settings")]
    public float armorPenetrationMultiplier = 1f;
    private Canvas sniperScopeCanvas;
    public float aimFOV = 30f;
    public float aimDuration = 0.25f;

    [Header("Melee Settings")]
    public float meleeRange = 3f;
    public float meleeAngle = 60f;

    private float originalFOV;
    private bool isAiming = false;
    private Camera mainCamera;

    private void Awake()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
            firePoint = mainCamera.transform;
            originalFOV = mainCamera.fieldOfView;
        }

        GameObject canvasGO = GameObject.FindGameObjectWithTag("Aim");
        if (canvasGO != null)
        {
            sniperScopeCanvas = canvasGO.GetComponent<Canvas>();
            sniperScopeCanvas.enabled = false;
        }
    }

    private void OnDisable()
    {
        ExitAim();
    }

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

    public override void Aim()
    {
        if (mainCamera == null) return;

        if (!isAiming)
        {
            isAiming = true;
            originalFOV = mainCamera.fieldOfView;
            mainCamera.DOFieldOfView(aimFOV, aimDuration);
            if (sniperScopeCanvas != null)
                sniperScopeCanvas.enabled = true;
        }
        else
        {
            ExitAim();
        }
    }

    private void ExitAim()
    {
        if (!isAiming || mainCamera == null) return;

        isAiming = false;
        mainCamera.DOFieldOfView(originalFOV, aimDuration);
        if (sniperScopeCanvas != null)
            sniperScopeCanvas.enabled = false;
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
                var damage = baseDamage * baseDamageMultiplier;
                var data = new DamageData(damage, armorPenetrationMultiplier, DamageType.Bullet, zone);
                receiver.TakeDamage(data);
                Debug.Log($"Sniper hit {hit.collider.name} for {damage} (x{armorPenetrationMultiplier})");
            }
        }

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
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
