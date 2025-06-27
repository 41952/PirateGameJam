using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    public string equipmentName;
    public int level = 1;

    public void AddLevel()
    {
        level++;
        Debug.Log($"{equipmentName} повысил уровень до {level}");
    }
}
