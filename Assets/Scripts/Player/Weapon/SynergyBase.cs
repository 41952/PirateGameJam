using UnityEngine;

public abstract class SynergyBase : MonoBehaviour
{
    protected WeaponBase weapon;

    public void Init(WeaponBase weapon)
    {
        this.weapon = weapon;
    }

    public virtual void OnFire() {}
    public virtual void OnAltFire() {}
    public virtual void OnLevelUp() {}
}
