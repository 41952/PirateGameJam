//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.1
//     from Assets/Scripts/Game/Input/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MainActionMap"",
            ""id"": ""101f97d4-b426-4a0b-b20f-91a1a48cec6b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""4003ba0f-6e8e-4200-bf85-e59ad92c2ade"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""c9ab0e30-f145-44a8-b33e-764d8833f29a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7e7fa903-d104-423c-a7de-b5d9f5da4ed4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""529cf4fd-8019-4c71-b52e-b0ba5be5640f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AltShoot"",
                    ""type"": ""Button"",
                    ""id"": ""d3837786-1811-46a7-88dd-d3c981e2b042"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""0d9cdd2e-ea21-4a45-8371-8e70946cd6f9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Button"",
                    ""id"": ""f836d292-4c96-4ea4-8b14-2c2a9020ae60"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""4edea006-42e7-435b-bbfb-9c270455e78f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""f492bff0-34cf-40d8-8474-e92fb1426db0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CloseCombat"",
                    ""type"": ""Button"",
                    ""id"": ""35eeb051-89c7-45cc-846a-5c78f386ac17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hook"",
                    ""type"": ""Button"",
                    ""id"": ""f977a10b-0037-4c0b-ad07-599a15acf90f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Upgrades"",
                    ""type"": ""Button"",
                    ""id"": ""1481a635-123a-4f32-81ba-6c0a9bc7952d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Direction"",
                    ""id"": ""58850d24-e75b-41c2-883d-d5837c223c57"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""53f69e84-5108-40c0-99c6-27da6f5cc6fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fd9570b9-84e5-4841-a0c9-40b9d42bea95"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bca5d76a-65ed-4037-8436-42adcfc41488"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b8c12f1f-2daf-4db0-b267-080adf0776d4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8147b0b2-5e1e-44da-9cce-ed4dd8eda318"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c342ff89-3d9b-4027-a4bd-bc00192304e4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""480289e7-6ff2-4351-a662-e3e8c66d8e91"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e66abe46-3ecf-411b-ac03-9ba9cfd97e4b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AltShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1e9c354-bfb6-4cb7-baff-4fa8627c0035"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2b26789-aa39-4c2a-911b-f08d66535ae8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00532f59-f83b-4a29-998a-39d5ffd05f4d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e521259-c0a9-4fc3-aa96-e835a0e1cb53"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d015fbc-ca46-4c2b-817c-d83f280f69e1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseCombat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b524866b-9846-45df-be3f-7421f598cd4f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa071231-7b6f-4699-b688-73d7583d4a3c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Upgrades"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""DefaultControlScheme"",
            ""bindingGroup"": ""DefaultControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MainActionMap
        m_MainActionMap = asset.FindActionMap("MainActionMap", throwIfNotFound: true);
        m_MainActionMap_Movement = m_MainActionMap.FindAction("Movement", throwIfNotFound: true);
        m_MainActionMap_Sprint = m_MainActionMap.FindAction("Sprint", throwIfNotFound: true);
        m_MainActionMap_Jump = m_MainActionMap.FindAction("Jump", throwIfNotFound: true);
        m_MainActionMap_Shoot = m_MainActionMap.FindAction("Shoot", throwIfNotFound: true);
        m_MainActionMap_AltShoot = m_MainActionMap.FindAction("AltShoot", throwIfNotFound: true);
        m_MainActionMap_Weapon1 = m_MainActionMap.FindAction("Weapon1", throwIfNotFound: true);
        m_MainActionMap_Weapon2 = m_MainActionMap.FindAction("Weapon2", throwIfNotFound: true);
        m_MainActionMap_WeaponSwitch = m_MainActionMap.FindAction("WeaponSwitch", throwIfNotFound: true);
        m_MainActionMap_Ultimate = m_MainActionMap.FindAction("Ultimate", throwIfNotFound: true);
        m_MainActionMap_CloseCombat = m_MainActionMap.FindAction("CloseCombat", throwIfNotFound: true);
        m_MainActionMap_Hook = m_MainActionMap.FindAction("Hook", throwIfNotFound: true);
        m_MainActionMap_Upgrades = m_MainActionMap.FindAction("Upgrades", throwIfNotFound: true);
    }

    ~@PlayerControls()
    {
        UnityEngine.Debug.Assert(!m_MainActionMap.enabled, "This will cause a leak and performance issues, PlayerControls.MainActionMap.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MainActionMap
    private readonly InputActionMap m_MainActionMap;
    private List<IMainActionMapActions> m_MainActionMapActionsCallbackInterfaces = new List<IMainActionMapActions>();
    private readonly InputAction m_MainActionMap_Movement;
    private readonly InputAction m_MainActionMap_Sprint;
    private readonly InputAction m_MainActionMap_Jump;
    private readonly InputAction m_MainActionMap_Shoot;
    private readonly InputAction m_MainActionMap_AltShoot;
    private readonly InputAction m_MainActionMap_Weapon1;
    private readonly InputAction m_MainActionMap_Weapon2;
    private readonly InputAction m_MainActionMap_WeaponSwitch;
    private readonly InputAction m_MainActionMap_Ultimate;
    private readonly InputAction m_MainActionMap_CloseCombat;
    private readonly InputAction m_MainActionMap_Hook;
    private readonly InputAction m_MainActionMap_Upgrades;
    public struct MainActionMapActions
    {
        private @PlayerControls m_Wrapper;
        public MainActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MainActionMap_Movement;
        public InputAction @Sprint => m_Wrapper.m_MainActionMap_Sprint;
        public InputAction @Jump => m_Wrapper.m_MainActionMap_Jump;
        public InputAction @Shoot => m_Wrapper.m_MainActionMap_Shoot;
        public InputAction @AltShoot => m_Wrapper.m_MainActionMap_AltShoot;
        public InputAction @Weapon1 => m_Wrapper.m_MainActionMap_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_MainActionMap_Weapon2;
        public InputAction @WeaponSwitch => m_Wrapper.m_MainActionMap_WeaponSwitch;
        public InputAction @Ultimate => m_Wrapper.m_MainActionMap_Ultimate;
        public InputAction @CloseCombat => m_Wrapper.m_MainActionMap_CloseCombat;
        public InputAction @Hook => m_Wrapper.m_MainActionMap_Hook;
        public InputAction @Upgrades => m_Wrapper.m_MainActionMap_Upgrades;
        public InputActionMap Get() { return m_Wrapper.m_MainActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IMainActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionMapActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @AltShoot.started += instance.OnAltShoot;
            @AltShoot.performed += instance.OnAltShoot;
            @AltShoot.canceled += instance.OnAltShoot;
            @Weapon1.started += instance.OnWeapon1;
            @Weapon1.performed += instance.OnWeapon1;
            @Weapon1.canceled += instance.OnWeapon1;
            @Weapon2.started += instance.OnWeapon2;
            @Weapon2.performed += instance.OnWeapon2;
            @Weapon2.canceled += instance.OnWeapon2;
            @WeaponSwitch.started += instance.OnWeaponSwitch;
            @WeaponSwitch.performed += instance.OnWeaponSwitch;
            @WeaponSwitch.canceled += instance.OnWeaponSwitch;
            @Ultimate.started += instance.OnUltimate;
            @Ultimate.performed += instance.OnUltimate;
            @Ultimate.canceled += instance.OnUltimate;
            @CloseCombat.started += instance.OnCloseCombat;
            @CloseCombat.performed += instance.OnCloseCombat;
            @CloseCombat.canceled += instance.OnCloseCombat;
            @Hook.started += instance.OnHook;
            @Hook.performed += instance.OnHook;
            @Hook.canceled += instance.OnHook;
            @Upgrades.started += instance.OnUpgrades;
            @Upgrades.performed += instance.OnUpgrades;
            @Upgrades.canceled += instance.OnUpgrades;
        }

        private void UnregisterCallbacks(IMainActionMapActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @AltShoot.started -= instance.OnAltShoot;
            @AltShoot.performed -= instance.OnAltShoot;
            @AltShoot.canceled -= instance.OnAltShoot;
            @Weapon1.started -= instance.OnWeapon1;
            @Weapon1.performed -= instance.OnWeapon1;
            @Weapon1.canceled -= instance.OnWeapon1;
            @Weapon2.started -= instance.OnWeapon2;
            @Weapon2.performed -= instance.OnWeapon2;
            @Weapon2.canceled -= instance.OnWeapon2;
            @WeaponSwitch.started -= instance.OnWeaponSwitch;
            @WeaponSwitch.performed -= instance.OnWeaponSwitch;
            @WeaponSwitch.canceled -= instance.OnWeaponSwitch;
            @Ultimate.started -= instance.OnUltimate;
            @Ultimate.performed -= instance.OnUltimate;
            @Ultimate.canceled -= instance.OnUltimate;
            @CloseCombat.started -= instance.OnCloseCombat;
            @CloseCombat.performed -= instance.OnCloseCombat;
            @CloseCombat.canceled -= instance.OnCloseCombat;
            @Hook.started -= instance.OnHook;
            @Hook.performed -= instance.OnHook;
            @Hook.canceled -= instance.OnHook;
            @Upgrades.started -= instance.OnUpgrades;
            @Upgrades.performed -= instance.OnUpgrades;
            @Upgrades.canceled -= instance.OnUpgrades;
        }

        public void RemoveCallbacks(IMainActionMapActions instance)
        {
            if (m_Wrapper.m_MainActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_MainActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainActionMapActions @MainActionMap => new MainActionMapActions(this);
    private int m_DefaultControlSchemeSchemeIndex = -1;
    public InputControlScheme DefaultControlSchemeScheme
    {
        get
        {
            if (m_DefaultControlSchemeSchemeIndex == -1) m_DefaultControlSchemeSchemeIndex = asset.FindControlSchemeIndex("DefaultControlScheme");
            return asset.controlSchemes[m_DefaultControlSchemeSchemeIndex];
        }
    }
    public interface IMainActionMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAltShoot(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
        void OnWeaponSwitch(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
        void OnCloseCombat(InputAction.CallbackContext context);
        void OnHook(InputAction.CallbackContext context);
        void OnUpgrades(InputAction.CallbackContext context);
    }
}
