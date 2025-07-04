
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : EnemyAI
{
    [Header("Ranged Attack Settings")]
    public float rangedAttackRadius = 8f;
    public float rangedAttackCooldown = 1.5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 12f;
    public float projectileDamage = 8f;
    public Transform firePoint;

    [HideInInspector]public float lastRangedAttackTime;

    protected override void Start()
    {
        base.Start();
        lastRangedAttackTime = Time.time - rangedAttackCooldown;
        // заменяем стартовое состояние на наш собственный StateMachine
        stateMachine.Initialize(new IdleState(this, stateMachine));
    }

    // Вызывается из ChaseState: если дистанция попала в rangedAttackRadius
    public bool InRangedRange()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= rangedAttackRadius && CanDetectPlayer();
    }

    // Метод для выполнения выстрела

public  void FireProjectile()
    {
        if (Time.time - lastRangedAttackTime < rangedAttackCooldown)
            return;

        // World-space vector to player
        Vector3 toTarget = (player.position + Vector3.up * 0.2f - firePoint.position);
        // Extract vertical component (pitch)
        float vertical = toTarget.y;
        // Horizontal direction locked to current forward of firePoint
        Vector3 flatForward = new Vector3(firePoint.forward.x, 0, firePoint.forward.z).normalized;
        // Combine pitch with locked yaw
        Vector3 aimDir = (flatForward * toTarget.magnitude + Vector3.up * vertical).normalized;

        // Instantiate projectile
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(aimDir));
        var ps = proj.GetComponent<EnemyProjectile>();
        ps.Initialize(projectileSpeed, projectileDamage, ownerMask: LayerMask.GetMask("Body"));

        lastRangedAttackTime = Time.time;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRadius);
    }


}
