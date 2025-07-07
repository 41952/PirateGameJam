using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class WaveManager : MonoBehaviour
{
    [Header("Конфигурация волн")]
    public List<WaveData> waves;
    [Header("Спавнеры врагов на арене")] private List<EnemySpawner> spawners;
    [Header("Компонент, который активирует изменения уровня")] public LevelChangeActivator levelChangeActivator;
    [Header("Задержка между спавнами врагов")] public float enemySpawnDelay = 0.2f;

    private int currentWave = 0;

    private void Start()
    {
        MusicManager.Instance.PlayBattle();
        StartCoroutine(RunWaves());
    }

    private void UpdateSpawners()
    {
        spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
    }

    private IEnumerator RunWaves()
    {
        while (currentWave < waves.Count)
        {
            var data = waves[currentWave];
            GameEvents.OnWaveStarted?.Invoke(currentWave + 1);

            levelChangeActivator.ApplyChanges(data.levelChangePrefabsToSpawn);
            UpdateSpawners();

            int totalCount = 0;
            foreach (var info in data.spawnList) totalCount += info.count;
            float spawnInterval = data.waveDuration / totalCount;
            // Запуск корутины спавна
            StartCoroutine(SpawnRoutine(data, spawnInterval));

            yield return new WaitForSeconds(data.waveDuration);
            GameEvents.OnWaveEnded?.Invoke(currentWave + 1);

            MusicManager.Instance.BlendBattleIntermission(0.4f);
            yield return new WaitForSeconds(data.intermission);
            MusicManager.Instance.PlayBattle();

            currentWave++;
        }

        GameEvents.OnAllWavesCompleted?.Invoke();
    }

    private IEnumerator SpawnRoutine(WaveData data, float spawnInterval)
    {
        List<GameObject> flatList = new List<GameObject>();
        foreach (var info in data.spawnList)
            for (int i = 0; i < info.count; i++)
                flatList.Add(info.prefab);

        int lastSpawner = -1;
        for (int i = 0; i < flatList.Count; i++)
        {
            int idx;
            do { idx = Random.Range(0, spawners.Count); }
            while (idx == lastSpawner && spawners.Count > 1);
            lastSpawner = idx;

            var prefab = flatList[i];
            var go = PoolManager.Instance.GetFromPool(prefab);
            var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.enabled = false;
            Vector3 pos = spawners[idx].GetSpawnPosition();
            go.transform.position = pos;
            go.transform.rotation = Quaternion.identity;
            agent.Warp(pos);
            agent.enabled = true;

            GameEvents.OnEnemySpawned?.Invoke(go);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}