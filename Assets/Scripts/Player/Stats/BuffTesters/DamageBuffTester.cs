using UnityEngine;


[RequireComponent(typeof(StatsContainer))]
public class DamageBuffTester : MonoBehaviour
{
    private StatsContainer _stats;
    private const float Duration = 5f;

    private void Awake() => _stats = GetComponent<StatsContainer>();

    [ContextMenu("Apply Damage Buff")]
    private void ApplyBuff()
    {
        var buff = new Game.Buffs.DamageBuff(Duration);
        _stats.ApplyModifier(buff);
        Debug.Log($"Applied DamageBuff x2 for {Duration}s");
    }
}
