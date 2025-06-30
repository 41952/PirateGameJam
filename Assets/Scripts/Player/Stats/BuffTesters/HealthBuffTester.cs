using UnityEngine;


[RequireComponent(typeof(StatsContainer))]
public class HealthBuffTester : MonoBehaviour
{
    private StatsContainer _stats;
    private const float TestMultiplier = 4f;
    private const float TestDuration = 5f;

    private void Awake()
    {
        _stats = GetComponent<StatsContainer>();
    }

    [ContextMenu("Apply Health Buff")]        
    private void ApplyBuff()
    {
        var buff = new HealthBuff(TestMultiplier, TestDuration);
        _stats.ApplyModifier(buff);
        Debug.Log($"Applied HealthBuff x{TestMultiplier} for {TestDuration}s");
    }
}
