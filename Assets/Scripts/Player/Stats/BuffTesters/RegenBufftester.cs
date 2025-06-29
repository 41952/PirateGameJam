using UnityEngine;


[RequireComponent(typeof(StatsContainer))]
public class RegenBuffTester : MonoBehaviour
{
    private StatsContainer _stats;
    private const float Duration = 5f;

    private void Awake() => _stats = GetComponent<StatsContainer>();

    [ContextMenu("Apply Regen Buff")]
    private void ApplyBuff()
    {
        var buff = new Game.Buffs.RegenBuff(Duration);
        _stats.ApplyModifier(buff);
        Debug.Log($"Applied RegenBuff x4 for {Duration}s");
    }
}
