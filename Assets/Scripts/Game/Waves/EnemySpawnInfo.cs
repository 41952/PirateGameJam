using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData")]
public class WaveData : ScriptableObject
{
    [Tooltip("Примерная длительность волны в секундах")] public float waveDuration = 30f;
    [Tooltip("Количество секунд между волнами")] public float intermission = 15f;
    [Tooltip("Список врагов и их общее число для спавна в этой волне")] public List<EnemySpawnInfo> spawnList;
}
