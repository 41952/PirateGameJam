using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SynergyDashSlash : SynergyBase
{
    [Header("Dash Settings")]
    public float dashDistance = 10f;            // общее расстояние дэша
    public float dashSpeed = 20f;               // скорость движения (м/с)
    public float damage = 50f;                  // урон за попадание
    public float damageRadius = 2f;             // радиус зоны урона вокруг игрока

    private bool isDashing = false;
    private HashSet<Collider> hitEnemies = new HashSet<Collider>();

    private Rigidbody playerRb;

    public override void Init(WeaponBase weapon)
    {
        base.Init(weapon);

        // Найти игрока по тэгу и получить Rigidbody
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("SynergyDashSlash: не найден объект Player!");
            return;
        }
        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            Debug.LogError("SynergyDashSlash: у Player нет Rigidbody!");
        }
    }

    public override void OnAltFire()
    {
        base.OnAltFire();

        if (IsUltimateActive() && !isDashing && playerRb != null)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        hitEnemies.Clear();

        float remaining = dashDistance;
        // Дэш по горизонтальной проекции направления камеры
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;
        dir.Normalize();

        // Запомним исходную вертикальную скорость
        float originalYVel = playerRb.linearVelocity.y;

        while (remaining > 0f && IsUltimateActive())
        {
            // Расстояние за этот кадр
            float step = dashSpeed * Time.deltaTime;
            if (step > remaining) step = remaining;

            // Задаём новую скорость: вектор дэша + гравитация/вертикальная составляющая
            Vector3 newVel = dir * dashSpeed;
            newVel.y = originalYVel; 
            playerRb.linearVelocity = newVel;

            remaining -= step;

            // Зона урона
            Collider[] hits = Physics.OverlapSphere(playerRb.position, damageRadius);
            foreach (var col in hits)
            {
                if (hitEnemies.Contains(col)) continue;
                if (col.TryGetComponent<IDamageReceiver>(out var receiver))
                {
                    DamageData data = new DamageData(damage, 1f, DamageType.Melee, HitZone.Body);
                    receiver.TakeDamage(data);
                    hitEnemies.Add(col);
                }
            }

            yield return null;
        }

        // Остановим движение дэша (сохраним гравитацию)
        var stopVel = playerRb.linearVelocity;
        stopVel.x = 0;
        stopVel.z = 0;
        playerRb.linearVelocity = new Vector3(0, stopVel.y, 0);

        isDashing = false;
    }

    private new void Update()
    {
        base.Update();

        // Если ульта закончилась во время дэша — прерываем корутину
        if (!IsUltimateActive() && isDashing)
        {
            StopAllCoroutines();
            isDashing = false;
        }
    }
}
