
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputHolder
{
    private static PlayerControls playerControls = new PlayerControls();
    private static PlayerInput playerInput;

    public static PlayerInput GetInput()
    {
        return playerInput;
    }
    public static void SetupInput(PlayerInput input)
    {
        playerInput = input;
    }
    public static InputAction GetAction(TypeInputAction type)
    {
        return playerInput.actions[type.ToString()];
    }
    public static PlayerControls GetControls()
    {
        return playerControls;
    }
}
public enum TypeInputAction
{
    Movement,
    Sprint,
    Jump,
    Shoot,
    AltShoot,
    Weapon1,
    Weapon2,
    WeaponSwitch,
    Ultimate,
    CloseCombat,
    Hook,
    Upgrades
}