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
        var pooled = obj.GetComponent<PooledObject>();
        var prefab = pooled?.PrefabReference;
        if (prefab == null)
        {
            Destroy(obj);
            return;
        }

        // Если по этому prefab ещё нет очереди — создаём
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new Queue<GameObject>();

        obj.SetActive(false);
        pool[prefab].Enqueue(obj);
    }

}
