using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        if (!pool.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            pool[prefab] = queue;
        }
        if (queue.Count > 0)
        {
            var go = queue.Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        var prefab = obj.GetComponent<PooledObject>()?.PrefabReference;
        if (prefab == null) Destroy(obj);
        else
        {
            pool[prefab].Enqueue(obj);
        }
    }
}
