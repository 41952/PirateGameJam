using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    private float speed;
    private float damage;
    private float lifetime = 5f;
    private DamageType damageType;
    private HitZone hitZone;

    private Vector3 direction;

    public void Initialize(float speed, float damage, Vector3 direction, DamageType type = DamageType.Bullet)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction.normalized;
        this.damageType = type;
        this.hitZone = HitZone.Body;

        Destroy(gameObject, lifetime); // самоуничтожение
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageReceiver receiver))
        {
            Debug.Log("Receiver found, applying damage!");

            DamageData data = new DamageData(damage, 1f, damageType, hitZone);
            receiver.TakeDamage(data);
        }

        Destroy(gameObject);
    }
}
