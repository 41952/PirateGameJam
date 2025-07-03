using UnityEngine;

public class SynergyRageMode : SynergyBase
{
    public float damageMultiplier = 1.5f;
    public float fireRateMultiplier = 1.5f;

    private float originalDamageMultiplier;
    private float originalFireRate;

    private PlayerHealthSystem playerHealth;

    private void Start()
    {
        originalDamageMultiplier = weapon.baseDamage;
        originalFireRate = weapon.fireRate;   
    }
    public override void OnAltFire()
    {
        base.OnAltFire();

        if (IsUltimateActive())
        {
            // Найдём игрока
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealthSystem>();
                if (playerHealth != null)
                    playerHealth.SetInvulnerable(true);
            }

            // Сохраняем и увеличиваем характеристики оружия
            originalDamageMultiplier = weapon.baseDamage;
            originalFireRate = weapon.fireRate;

            weapon.baseDamage = originalDamageMultiplier * damageMultiplier;
            weapon.fireRate = originalFireRate * fireRateMultiplier;

            Debug.Log("Rage Mode Activated: damage & fire rate increased, player invulnerable.");
        }
    }

    private new void Update()
    {
        base.Update();

        if (!IsUltimateActive() && weapon.baseDamageMultiplier != originalDamageMultiplier)
        {
            // Возвращаем статы
            weapon.baseDamage = originalDamageMultiplier;
            weapon.fireRate = originalFireRate;

            if (playerHealth != null)
                playerHealth.SetInvulnerable(false);

            Debug.Log("Rage Mode Ended: stats reset, player vulnerable again.");
        }
    }
}
