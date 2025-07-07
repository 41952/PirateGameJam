using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public GameObject PrefabReference;
    private void OnDisable()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}