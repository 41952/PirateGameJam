using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private InventoryManager inventory;
    private int currentWeaponIndex = 0;

    private bool switchBlocked = false;

    private void Start()
    {
        inventory = InventoryManager.Instance;

        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            if (inventory.weapons[i] != null)
                inventory.weapons[i].gameObject.SetActive(false);
        }

        ActivateWeapon(0);

        GameEvents.OnUltimateStateChanged += OnUltimateStateChanged;
    }

    private void OnDestroy()
    {
        GameEvents.OnUltimateStateChanged -= OnUltimateStateChanged;
    }

    private void Update()
    {
        if (switchBlocked) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) TrySwitchWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) TrySwitchWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToOppositeWeapon();
    }

    private void OnUltimateStateChanged(bool active)
    {
        switchBlocked = active;
    }

    private void TrySwitchWeapon(int index)
    {
        if (index < 0 || index >= inventory.weapons.Length) return;
        if (inventory.weapons[index] == null) return;

        if (index != currentWeaponIndex)
            ActivateWeapon(index);
    }

    private void SwitchToOppositeWeapon()
    {
        int otherIndex = currentWeaponIndex == 0 ? 1 : 0;

        if (inventory.weapons[otherIndex] != null)
            ActivateWeapon(otherIndex);
    }

    private void ActivateWeapon(int index)
    {
        if (inventory.weapons[currentWeaponIndex] != null)
            inventory.weapons[currentWeaponIndex].gameObject.SetActive(false);

        if (inventory.weapons[index] != null)
        {
            inventory.weapons[index].gameObject.SetActive(true);
            currentWeaponIndex = index;

            GameEvents.RaiseWeaponSwitched(index, inventory.weapons[index]);
        }
    }
}
