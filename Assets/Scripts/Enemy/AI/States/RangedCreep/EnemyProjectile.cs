using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private LayerMask ownerMask;
    private Rigidbody rb;

    public void Initialize(float _speed, float _damage, LayerMask ownerMask)
    {
        speed = _speed;
        damage = _damage;
        this.ownerMask = ownerMask;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, 5f); // самоуничтожение, чтобы не висели бесконечно
    }

    void OnTriggerEnter(Collider other)
    {

        if ((ownerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            var ph = other.GetComponentInParent<PlayerHealthSystem>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Debug.Log("Damage applied!");
            }


        }
        Destroy(gameObject);
    }


    
}
