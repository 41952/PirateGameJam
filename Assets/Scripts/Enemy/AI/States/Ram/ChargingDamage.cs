using UnityEngine;

public class ChargingDamage : MonoBehaviour
{
    private ChargingEnemyAI owner;

    [SerializeField] private float damage = 15f;
    [SerializeField] private LayerMask targetMask; // например, Body

    public void Init(ChargingEnemyAI o) => owner = o;

    private void OnTriggerEnter(Collider other)
    {
        if (!owner.isCharging) return;

        // Проверяем слой
        if ((targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            owner.InterruptCharge();
            var ph = other.GetComponentInParent<PlayerHealthSystem>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

           
        }
    }
}
