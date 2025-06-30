using UnityEngine;

public class MovementStatListener : StatListener
{
    public MonoBehaviour movementScript;

    protected override StatType GetStatType() => StatType.Speed;

    protected override void OnStatChanged(StatType type, float newSpeed)
    {
        var prop = movementScript.GetType().GetField("speedMultiplier");
        if (prop != null)
            prop.SetValue(movementScript, newSpeed);
        else
            Debug.LogWarning("Movement script не содержит поле moveSpeed");
    }
}