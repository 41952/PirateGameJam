using UnityEngine;

public class SynergyTest : SynergyBase
{
    public override void OnAltFire()
    {
        base.OnAltFire();

        if (IsUltimateActive())
        {
            // Пример действия ульты
            Debug.Log("SynergyTest: ульта активна! Урон увеличен!");
        }
    }
}
