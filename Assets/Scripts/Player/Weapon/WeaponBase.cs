using UnityEngine;
using System.Collections.Generic;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Shoot Settings")]
    public string weaponName;
    public float baseDamage;
    public float baseDamageMultiplier;
    public float fireRate;
    public int magazineSize;
    public int currentAmmo;
    public float reloadTime;
    public int level = 1;

    public int maxLevel = 6;

    [Header("Shake Settings")]
    public float recoilDelay = 0.08f;
    public float recoilAmout = 7f;
    public float shakeDelay = 0.1f;
    public float shakeAmout = 0.15f;

    [HideInInspector] public SimpleFpsCamera cameraScript;

    [HideInInspector] public float lastFireTime;
    [HideInInspector] public float reloadTimer;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public Transform firePoint; // точка, откуда идёт выстрел
    [HideInInspector] public Camera mainCamera;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Melee")]
    public float meleeCooldown = 1.0f;
    public float meleeDamage = 25f;
    protected float lastMeleeTime;

    [HideInInspector] public List<SynergyBase> synergies = new();
    private HashSet<string> synergyPairs = new(); // weaponName+equipmentName
   
    private bool isAttacking = false;
    

 
    private void Start()
    {
        //биндим выстрелы на нажатие и удержание клавиш стрельбы и альт стрельбы
        InputHolder.GetAction(TypeInputAction.Shoot).started += context => isAttacking = true;
        InputHolder.GetAction(TypeInputAction.Shoot).performed += context => isAttacking = true;
        InputHolder.GetAction(TypeInputAction.Shoot).canceled += context => isAttacking = false;

        InputHolder.GetAction(TypeInputAction.AltShoot).started += context => Aim();
        InputHolder.GetAction(TypeInputAction.Ultimate).started += context => AltFire();
        InputHolder.GetAction(TypeInputAction.CloseCombat).started += context => TryMelee();

        if (Camera.main != null)
        {
            mainCamera = Camera.main;
            firePoint = mainCamera.transform;
            cameraScript = Camera.main.GetComponent<SimpleFpsCamera>();
        }
    }
  
    public virtual void Update()
    {
        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                isReloading = false;
                currentAmmo = magazineSize;
                Debug.Log($"{weaponName} reload complete.");
            }
            return;
        }
        if(isAttacking)
        {
            Fire();
        }

        //if (Input.GetMouseButtonDown(1))
        //    AltFire();

        //if (Input.GetMouseButton(0))
        //    Fire();

        //if (Input.GetKeyDown(KeyCode.Q))
        //    AltFire();

        //if (Input.GetKeyDown(KeyCode.F))
        //    TryMelee();

        //if (Input.GetMouseButtonDown(1))
        //    Aim();
    }

    public virtual void Fire()
    {
        if (Time.time < lastFireTime + 1f / fireRate || isReloading || currentAmmo <= 0)
            return;

        currentAmmo--;
        lastFireTime = Time.time;

        Debug.Log($"{weaponName} shoot!~ Current Ammo: {currentAmmo}");

        foreach (var s in synergies)
            s.OnFire();

        if (currentAmmo <= 0)
            Reload();
    }

    public virtual void AltFire()
    {
        Debug.Log($"{weaponName} altFire!~");
        foreach (var s in synergies)
            s.OnAltFire();
    }

    public virtual void Aim()
    {
        Debug.Log($"{weaponName} aiming (placeholder)");
    }

    public virtual void TryMelee()
    {
        if (Time.time < lastMeleeTime + meleeCooldown)
            return;

        lastMeleeTime = Time.time;
        MeleeAttack(); // Вызывается в дочернем классе
    }

    public virtual void MeleeAttack()
    {
        Debug.Log($"{weaponName} melee hit (abstract, override in child)");
    }

    public virtual void Reload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
        Debug.Log($"{weaponName} reload...");
    }

    public virtual void AddLevel()
    {
        level++; // <- пусть здесь будет
        InventoryManager.Instance.CheckSynergies();
        Debug.Log($"{weaponName} level up to {level}");

        GetComponent<WeaponLevelApplier>()?.ApplyLevel();

        foreach (var s in synergies)
            s.OnLevelUp();
    }

    public void AddSynergy(SynergyBase synergy, string equipmentName)
    {
        string pairKey = weaponName + "+" + equipmentName;
        if (synergyPairs.Contains(pairKey))
            return;

        synergyPairs.Add(pairKey);
        synergies.Add(synergy);
        synergy.Init(this);
        AddLevel();
    }


    public int GetLevel() => level;

    
}