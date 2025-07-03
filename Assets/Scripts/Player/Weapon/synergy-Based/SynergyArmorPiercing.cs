using UnityEngine;

public class SynergyArmorPiercing : SynergyBase
{
    private float originalArmorPenetration;

    private SniperRifle sniperRifle;

    public override void Init(WeaponBase weapon)
    {
        base.Init(weapon);
        sniperRifle = weapon as SniperRifle;

        if (sniperRifle == null)
        {
            Debug.LogError("SynergyArmorPiercing: Привязано не к снайперке!");
        }
    }

    private void Start()
    {
        originalArmorPenetration = sniperRifle.armorPenetrationMultiplier;
    }

    public override void OnAltFire()
    {
        base.OnAltFire();

        if (IsUltimateActive() && sniperRifle != null)
        {
            // Сохраняем исходное значение и активируем бронепробитие
            originalArmorPenetration = sniperRifle.armorPenetrationMultiplier;
            sniperRifle.armorPenetrationMultiplier = -1f;

        }
    }

    private new void Update()
    {
        base.Update();

        // После завершения ульты — вернуть значение обратно
        if (!IsUltimateActive() && sniperRifle != null && sniperRifle.armorPenetrationMultiplier == -1f)
        {
            sniperRifle.armorPenetrationMultiplier = originalArmorPenetration;
        }
    }
}
