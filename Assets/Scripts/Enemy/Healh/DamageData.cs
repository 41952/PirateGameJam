using UnityEngine;
public struct DamageData
{
    public float baseDamage;
    public float armorPenetration; // Множитель по броне
    public DamageType type; // пуля, взрыв и т.д.
    public HitZone hitZone; // голова, тело, спина и т.д.

    public DamageData(float baseDamage, float armorPenetration, DamageType type, HitZone hitZone)
    {
        this.baseDamage = baseDamage;
        this.armorPenetration = armorPenetration;
        this.type = type;
        this.hitZone = hitZone;
    }
}

public enum DamageType { Bullet, Explosion, Melee }
public enum HitZone { Head, Body, Back, Armor }
