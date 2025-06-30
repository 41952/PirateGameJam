using UnityEngine;
using Game.Buffs;

[RequireComponent(typeof(StatsContainer))]
public class SpeedBuffTester : MonoBehaviour
{
    private StatsContainer _stats;
    private const float Duration = 5f;

    private void Start() => _stats = GetComponent<StatsContainer>();

    [ContextMenu("Apply Speed Buff")]
    private void ApplyBuff()
    {
        var buff = new Game.Buffs.SpeedBuff(Duration);
        _stats.ApplyModifier(buff);
        Debug.Log($"Applied SpeedBuff x2 for {Duration}s");
    }
}
