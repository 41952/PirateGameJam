using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class WaveManager : MonoBehaviour
{
    [Header("Конфигурация волн")]
    public List<WaveData> waves;
    [Header("Спавнеры врагов на арене")] public List<EnemySpawner> spawners;

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (currentWave < waves.Count)
        {
            var data = waves[currentWave];
            GameEvents.OnWaveStarted?.Invoke(currentWave + 1);

            // Подготовка: считаем общее число
            int totalCount = 0;
            foreach (var info in data.spawnList) totalCount += info.count;
            float spawnInterval = data.waveDuration / totalCount;

            // Запуск корутины спавна
            StartCoroutine(SpawnRoutine(data, spawnInterval));

            // Ждём окончания волны по времени
            yield return new WaitForSeconds(data.waveDuration);
            GameEvents.OnWaveEnded?.Invoke(currentWave + 1);

            // Интермиссия
            yield return new WaitForSeconds(data.intermission);
            currentWave++;
        }
        GameEvents.OnAllWavesCompleted?.Invoke();
    }

    private IEnumerator SpawnRoutine(WaveData data, float spawnInterval)
    {
        // Флэт-список референсов врагов для равномерного спавна
        List<GameObject> flatList = new List<GameObject>();
        foreach (var info in data.spawnList)
            for (int i = 0; i < info.count; i++)
                flatList.Add(info.prefab);

        int lastSpawner = -1;
        for (int i = 0; i < flatList.Count; i++)
        {
            // Выбираем спавнер, не равный предыдущему
            int idx;
            do { idx = UnityEngine.Random.Range(0, spawners.Count); }
            while (idx == lastSpawner && spawners.Count > 1);
            lastSpawner = idx;

            // Спавним
            var prefab = flatList[i];
            var go = PoolManager.Instance.GetFromPool(prefab);
            var pooled = go.GetComponent<PooledObject>();
            if (pooled == null) pooled = go.AddComponent<PooledObject>();
            pooled.PrefabReference = prefab;

            Vector3 pos = spawners[idx].GetSpawnPosition();
            go.transform.position = pos;

            GameEvents.OnEnemySpawned?.Invoke(go);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}