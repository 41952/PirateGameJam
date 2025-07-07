using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections.Generic;
public class LevelChangeActivator : MonoBehaviour
{
    public Transform levelRoot; // куда спавнить
    public NavMeshSurface surfaceToRebake;

    public void ApplyChanges(List<GameObject> prefabsToSpawn)
    {
        foreach (var prefab in prefabsToSpawn)
        {
            if (prefab != null)
                Instantiate(prefab, levelRoot);
        }

        if (surfaceToRebake != null)
            surfaceToRebake.BuildNavMesh();
    }

}