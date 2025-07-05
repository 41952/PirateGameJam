using UnityEngine;
using System;

[Serializable]
public class EnemySpawnInfo
{
    public GameObject prefab;
    [Min(1)] public int count = 1;
}