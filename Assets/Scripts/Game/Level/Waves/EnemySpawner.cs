using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Радиус вокруг спавнера для рандомной точки")] public float spawnRadius = 2f;

    public Vector3 GetSpawnPosition()
    {
        Vector2 circle = UnityEngine.Random.insideUnitCircle * spawnRadius;
        Vector3 pos = transform.position + new Vector3(circle.x, 0, circle.y);
        return pos;
    }
}
