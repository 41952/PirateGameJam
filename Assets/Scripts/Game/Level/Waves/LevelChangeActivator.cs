using UnityEngine;
using System.Collections.Generic;

public class LevelSceneChanges : MonoBehaviour
{
    [System.Serializable]
    public class WaveLevelObjects
    {
        public string waveName;
        public List<GameObject> objectsToEnable;
        public List<GameObject> objectsToDisable;
    }

    public List<WaveLevelObjects> waveChanges;

    public void ApplyChangesForWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= waveChanges.Count) return;

        var change = waveChanges[waveIndex];

        foreach (var go in change.objectsToEnable)
            if (go != null) go.SetActive(true);

        foreach (var go in change.objectsToDisable)
            if (go != null) go.SetActive(false);
    }
}
