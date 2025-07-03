
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
    public static InputAction GetAction(InputActionType type)
    {
        return playerInput.actions[type.ToString()];
    }
    public static PlayerControls GetControls() 
    {
        return playerControls; 
    }
    public static void ChangeControlsAction( string action,string path)
    {
        //playerControls = newControls;
        playerControls.asset.FindActionMap("MainActionMap").FindAction(action).ChangeBinding(0).WithPath(path);
        //playerControls.MainActionMap.Movement.ChangeBinding(1).WithPath(path);
        Debug.Log("changed controls");
        
    }
    public static void ChangeControlsComposite( string action,string binding,string path)
    {
        playerControls.asset.FindActionMap("MainActionMap").FindAction(action).ChangeBinding(binding).WithPath(path);
        Debug.Log("changed controls");
    }
}
public enum InputActionType
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