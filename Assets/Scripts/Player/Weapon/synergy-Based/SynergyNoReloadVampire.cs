using UnityEngine;

public class SynergyNoReloadVampire : SynergyBase
{
    private float originalReloadTime;
    public float vampirismMultiplier = 0.3f; // 30% от урона идёт в лечение
    public float maxHealPerSecond = 20f; // лимит исцеления в секунду (опционально)

    private float healAccumulator = 0f;

    private void OnEnable()
    {
        GameEvents.OnEnemyDamaged += HandleEnemyDamaged;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDamaged -= HandleEnemyDamaged;
    }

    private void HandleEnemyDamaged(EnemyHealth target, float amount, DamageData data)
    {
        if (!IsUltimateActive()) return;

        float healAmount = amount * vampirismMultiplier;

        if (maxHealPerSecond > 0)
        {
            healAccumulator += healAmount;
            healAmount = Mathf.Min(healAccumulator, maxHealPerSecond * Time.deltaTime);
        }

        // Найдём компонент игрока и вылечим его (или передадим через GameManager, если требуется)
        var player = GameObject.FindWithTag("Player");
        if (player.TryGetComponent(out PlayerHealthSystem playerHealth))
        {
            playerHealth.Heal(healAmount);
        }
    }

    public override void OnAltFire()
    {
        base.OnAltFire();

        if (IsUltimateActive())
        {
            // Сохраняем и обнуляем время перезарядки
            originalReloadTime = weapon.reloadTime;
            weapon.reloadTime = 0f;
            weapon.isReloading = false; // вдруг в процессе
            Debug.Log("SynergyNoReloadVampire: Ульта активна - перезарядка отключена!");
        }
    }

    private new void Update()
    {
        base.Update();

        // Отключение ульты — возвращаем параметры
        if (!IsUltimateActive() && weapon.reloadTime == 0f)
        {
            weapon.reloadTime = originalReloadTime;
            Debug.Log("SynergyNoReloadVampire: Ульта закончилась - перезарядка восстановлена.");
        }

        // Обновляем лимит на исцеление
        healAccumulator = Mathf.Max(0, healAccumulator - maxHealPerSecond * Time.deltaTime);
    }
}
