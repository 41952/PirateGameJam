using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class SynergyBase : MonoBehaviour
{
    protected WeaponBase weapon;

    [Header("Ultimate Settings")]
    public float ultimateChargeTime = 5f; // сколько нужно секунд до полной зарядки
    public float ultimateDuration = 3f;   // сколько длится ульта

    private float chargeTimer = 0f;
    private bool ultimateReady = false;
    private bool ultimateActive = false;

    private float ultimateTimer = 0f;

    public virtual void Init(WeaponBase weapon)
    {
        this.weapon = weapon;
    }

    public virtual void Update()
    {
        // Заряжаем ульту только если не активна и не готова
        if (!ultimateActive && !ultimateReady)
        {
            chargeTimer += Time.deltaTime;
            GameEvents.RaiseUltimateCooldown(chargeTimer, ultimateChargeTime);

            if (chargeTimer >= ultimateChargeTime)
            {
                
                chargeTimer = ultimateChargeTime;
                ultimateReady = true;
                Debug.Log("Ultimate Ready!");
            }
        }

        // Работа таймера ульты
        if (ultimateActive)
        {
            ultimateTimer -= Time.deltaTime;
            if (ultimateTimer <= 0f)
            {
                EndUltimate();
            }
        }
    }


    public virtual void OnFire() {}
    public virtual void OnLevelUp() {}

    public virtual void OnAltFire()
    {
        if (!ultimateReady || ultimateActive) return;

        ActivateUltimate();
    }

    private void ActivateUltimate()
    {
        ultimateReady = false;
        ultimateActive = true;
        ultimateTimer = ultimateDuration;
        chargeTimer = 0f;

        GameEvents.RaiseUltimateStateChanged(true);
        Debug.Log("Ultimate Activated!");

        // Здесь — конкретная логика эффекта ульты (в дочерних классах)
    }

    private void EndUltimate()
    {
        ultimateActive = false;

        GameEvents.RaiseUltimateStateChanged(false);
        Debug.Log("Ultimate Ended");

        // Здесь — откат эффекта
    }

    public bool IsUltimateActive() => ultimateActive;
    public bool IsUltimateReady() => ultimateReady;
}
