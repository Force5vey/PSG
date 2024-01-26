//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/MainPlayerControls.inputactions
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

public partial class @MainPlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainPlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainPlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerControl"",
            ""id"": ""c7fba036-ef78-4643-96e3-d2c974db2db3"",
            ""actions"": [
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Value"",
                    ""id"": ""31ca0709-9618-4847-9288-cc73bdc68973"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Value"",
                    ""id"": ""e4710576-bf22-4585-aac4-70920752ed8a"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftTrigger"",
                    ""type"": ""Value"",
                    ""id"": ""7b1b3da6-9d2a-4329-98f2-b8a7739f2479"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightTrigger"",
                    ""type"": ""Value"",
                    ""id"": ""46f41487-9e87-4ecc-a4fd-4f430420aa7c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ButtonSouth"",
                    ""type"": ""Button"",
                    ""id"": ""92c87bd9-3006-4896-be03-bfb3b10bb43e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ButtonNorth"",
                    ""type"": ""Button"",
                    ""id"": ""e8689b59-1031-4aa8-bc54-7b00220b2acb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ButtonWest"",
                    ""type"": ""Button"",
                    ""id"": ""2600cf12-6637-483f-9663-af584f8750ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ButtonEast"",
                    ""type"": ""Button"",
                    ""id"": ""30bc61ab-9800-4b40-8b0a-a0255605723e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""aade8aed-05d6-473b-a1a9-f71a93e457bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""b821f2d5-035a-498b-9a8f-71718261a4fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b8c9e63d-fc41-4783-a2af-2cccc0360a51"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonSouth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26c29630-fc17-4c60-b9be-ebbb04f1fd1b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonNorth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d94d71c-92fe-4d10-9b00-3c2e0b91f1f0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonWest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c90ab411-2179-4348-a3a2-6cbb7b62c649"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonEast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c17be4a-731e-40eb-a94f-34e1562691b3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""89d2b097-20f2-482e-86dd-9c5d72cb77d3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7845e828-7f56-4583-b8aa-fdd152a37741"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99755b56-e49f-4657-b0b1-141501b5a2c2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""423a5b57-b659-4bfe-bdd1-c8a9b25faa95"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7bd845a6-fd59-44b6-aaa2-10f32e03a721"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""595f4874-3bf5-46f5-b13e-e0b0f5e1d66e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""67874cf2-ccca-49f6-aff9-5530d2dc4e3c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""510ebc99-916e-4859-8786-a9fc469cc6d6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19e98f21-35e1-40ad-aea8-ce9c3d08340d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""eb9b6db8-6202-4370-83b7-72e480d687a8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""64d9646d-8fa2-4111-adeb-a2e1490064e7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""833c7094-7c57-45e3-9791-01715bfe909b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90a9c152-bdc9-4d6b-ae14-f13a29f58534"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8acdfa8d-bb87-4e06-ab92-0fd5452e7141"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e4b8bcf-72f5-4a03-976b-79d265d1c1bb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GameControl"",
            ""id"": ""8a4f3eae-68c2-4077-82a9-a8452e3a34e1"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""170fee1b-d4a6-43f9-ab97-31d33e1156e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""6ce3e13a-7048-4b19-9b78-82162a939312"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""67908f81-cfe1-4a9f-8650-999042162762"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f8bb814-adb1-4840-9131-9048a724a120"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""f12970d8-f6e8-4225-84b0-8e29445bf479"",
            ""actions"": [
                {
                    ""name"": ""MousePointer"",
                    ""type"": ""Button"",
                    ""id"": ""fb4c91ca-da40-406c-b3d1-8b7effb391ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""7d170d7f-51a7-4188-b655-25acb5cd8e34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f38233d-eb06-4fa7-9f46-ace3f4a66ecb"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""967ebf4f-dd79-4010-9b15-b0638d5cd2fc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControl
        m_PlayerControl = asset.FindActionMap("PlayerControl", throwIfNotFound: true);
        m_PlayerControl_LeftStick = m_PlayerControl.FindAction("LeftStick", throwIfNotFound: true);
        m_PlayerControl_RightStick = m_PlayerControl.FindAction("RightStick", throwIfNotFound: true);
        m_PlayerControl_LeftTrigger = m_PlayerControl.FindAction("LeftTrigger", throwIfNotFound: true);
        m_PlayerControl_RightTrigger = m_PlayerControl.FindAction("RightTrigger", throwIfNotFound: true);
        m_PlayerControl_ButtonSouth = m_PlayerControl.FindAction("ButtonSouth", throwIfNotFound: true);
        m_PlayerControl_ButtonNorth = m_PlayerControl.FindAction("ButtonNorth", throwIfNotFound: true);
        m_PlayerControl_ButtonWest = m_PlayerControl.FindAction("ButtonWest", throwIfNotFound: true);
        m_PlayerControl_ButtonEast = m_PlayerControl.FindAction("ButtonEast", throwIfNotFound: true);
        m_PlayerControl_LeftShoulder = m_PlayerControl.FindAction("LeftShoulder", throwIfNotFound: true);
        m_PlayerControl_RightShoulder = m_PlayerControl.FindAction("RightShoulder", throwIfNotFound: true);
        // GameControl
        m_GameControl = asset.FindActionMap("GameControl", throwIfNotFound: true);
        m_GameControl_Pause = m_GameControl.FindAction("Pause", throwIfNotFound: true);
        m_GameControl_Menu = m_GameControl.FindAction("Menu", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_MousePointer = m_Mouse.FindAction("MousePointer", throwIfNotFound: true);
        m_Mouse_MouseClick = m_Mouse.FindAction("MouseClick", throwIfNotFound: true);
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

    // PlayerControl
    private readonly InputActionMap m_PlayerControl;
    private List<IPlayerControlActions> m_PlayerControlActionsCallbackInterfaces = new List<IPlayerControlActions>();
    private readonly InputAction m_PlayerControl_LeftStick;
    private readonly InputAction m_PlayerControl_RightStick;
    private readonly InputAction m_PlayerControl_LeftTrigger;
    private readonly InputAction m_PlayerControl_RightTrigger;
    private readonly InputAction m_PlayerControl_ButtonSouth;
    private readonly InputAction m_PlayerControl_ButtonNorth;
    private readonly InputAction m_PlayerControl_ButtonWest;
    private readonly InputAction m_PlayerControl_ButtonEast;
    private readonly InputAction m_PlayerControl_LeftShoulder;
    private readonly InputAction m_PlayerControl_RightShoulder;
    public struct PlayerControlActions
    {
        private @MainPlayerControls m_Wrapper;
        public PlayerControlActions(@MainPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftStick => m_Wrapper.m_PlayerControl_LeftStick;
        public InputAction @RightStick => m_Wrapper.m_PlayerControl_RightStick;
        public InputAction @LeftTrigger => m_Wrapper.m_PlayerControl_LeftTrigger;
        public InputAction @RightTrigger => m_Wrapper.m_PlayerControl_RightTrigger;
        public InputAction @ButtonSouth => m_Wrapper.m_PlayerControl_ButtonSouth;
        public InputAction @ButtonNorth => m_Wrapper.m_PlayerControl_ButtonNorth;
        public InputAction @ButtonWest => m_Wrapper.m_PlayerControl_ButtonWest;
        public InputAction @ButtonEast => m_Wrapper.m_PlayerControl_ButtonEast;
        public InputAction @LeftShoulder => m_Wrapper.m_PlayerControl_LeftShoulder;
        public InputAction @RightShoulder => m_Wrapper.m_PlayerControl_RightShoulder;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerControlActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerControlActionsCallbackInterfaces.Add(instance);
            @LeftStick.started += instance.OnLeftStick;
            @LeftStick.performed += instance.OnLeftStick;
            @LeftStick.canceled += instance.OnLeftStick;
            @RightStick.started += instance.OnRightStick;
            @RightStick.performed += instance.OnRightStick;
            @RightStick.canceled += instance.OnRightStick;
            @LeftTrigger.started += instance.OnLeftTrigger;
            @LeftTrigger.performed += instance.OnLeftTrigger;
            @LeftTrigger.canceled += instance.OnLeftTrigger;
            @RightTrigger.started += instance.OnRightTrigger;
            @RightTrigger.performed += instance.OnRightTrigger;
            @RightTrigger.canceled += instance.OnRightTrigger;
            @ButtonSouth.started += instance.OnButtonSouth;
            @ButtonSouth.performed += instance.OnButtonSouth;
            @ButtonSouth.canceled += instance.OnButtonSouth;
            @ButtonNorth.started += instance.OnButtonNorth;
            @ButtonNorth.performed += instance.OnButtonNorth;
            @ButtonNorth.canceled += instance.OnButtonNorth;
            @ButtonWest.started += instance.OnButtonWest;
            @ButtonWest.performed += instance.OnButtonWest;
            @ButtonWest.canceled += instance.OnButtonWest;
            @ButtonEast.started += instance.OnButtonEast;
            @ButtonEast.performed += instance.OnButtonEast;
            @ButtonEast.canceled += instance.OnButtonEast;
            @LeftShoulder.started += instance.OnLeftShoulder;
            @LeftShoulder.performed += instance.OnLeftShoulder;
            @LeftShoulder.canceled += instance.OnLeftShoulder;
            @RightShoulder.started += instance.OnRightShoulder;
            @RightShoulder.performed += instance.OnRightShoulder;
            @RightShoulder.canceled += instance.OnRightShoulder;
        }

        private void UnregisterCallbacks(IPlayerControlActions instance)
        {
            @LeftStick.started -= instance.OnLeftStick;
            @LeftStick.performed -= instance.OnLeftStick;
            @LeftStick.canceled -= instance.OnLeftStick;
            @RightStick.started -= instance.OnRightStick;
            @RightStick.performed -= instance.OnRightStick;
            @RightStick.canceled -= instance.OnRightStick;
            @LeftTrigger.started -= instance.OnLeftTrigger;
            @LeftTrigger.performed -= instance.OnLeftTrigger;
            @LeftTrigger.canceled -= instance.OnLeftTrigger;
            @RightTrigger.started -= instance.OnRightTrigger;
            @RightTrigger.performed -= instance.OnRightTrigger;
            @RightTrigger.canceled -= instance.OnRightTrigger;
            @ButtonSouth.started -= instance.OnButtonSouth;
            @ButtonSouth.performed -= instance.OnButtonSouth;
            @ButtonSouth.canceled -= instance.OnButtonSouth;
            @ButtonNorth.started -= instance.OnButtonNorth;
            @ButtonNorth.performed -= instance.OnButtonNorth;
            @ButtonNorth.canceled -= instance.OnButtonNorth;
            @ButtonWest.started -= instance.OnButtonWest;
            @ButtonWest.performed -= instance.OnButtonWest;
            @ButtonWest.canceled -= instance.OnButtonWest;
            @ButtonEast.started -= instance.OnButtonEast;
            @ButtonEast.performed -= instance.OnButtonEast;
            @ButtonEast.canceled -= instance.OnButtonEast;
            @LeftShoulder.started -= instance.OnLeftShoulder;
            @LeftShoulder.performed -= instance.OnLeftShoulder;
            @LeftShoulder.canceled -= instance.OnLeftShoulder;
            @RightShoulder.started -= instance.OnRightShoulder;
            @RightShoulder.performed -= instance.OnRightShoulder;
            @RightShoulder.canceled -= instance.OnRightShoulder;
        }

        public void RemoveCallbacks(IPlayerControlActions instance)
        {
            if (m_Wrapper.m_PlayerControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerControlActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerControlActions @PlayerControl => new PlayerControlActions(this);

    // GameControl
    private readonly InputActionMap m_GameControl;
    private List<IGameControlActions> m_GameControlActionsCallbackInterfaces = new List<IGameControlActions>();
    private readonly InputAction m_GameControl_Pause;
    private readonly InputAction m_GameControl_Menu;
    public struct GameControlActions
    {
        private @MainPlayerControls m_Wrapper;
        public GameControlActions(@MainPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_GameControl_Pause;
        public InputAction @Menu => m_Wrapper.m_GameControl_Menu;
        public InputActionMap Get() { return m_Wrapper.m_GameControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControlActions set) { return set.Get(); }
        public void AddCallbacks(IGameControlActions instance)
        {
            if (instance == null || m_Wrapper.m_GameControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameControlActionsCallbackInterfaces.Add(instance);
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @Menu.started += instance.OnMenu;
            @Menu.performed += instance.OnMenu;
            @Menu.canceled += instance.OnMenu;
        }

        private void UnregisterCallbacks(IGameControlActions instance)
        {
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @Menu.started -= instance.OnMenu;
            @Menu.performed -= instance.OnMenu;
            @Menu.canceled -= instance.OnMenu;
        }

        public void RemoveCallbacks(IGameControlActions instance)
        {
            if (m_Wrapper.m_GameControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameControlActions instance)
        {
            foreach (var item in m_Wrapper.m_GameControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameControlActions @GameControl => new GameControlActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private List<IMouseActions> m_MouseActionsCallbackInterfaces = new List<IMouseActions>();
    private readonly InputAction m_Mouse_MousePointer;
    private readonly InputAction m_Mouse_MouseClick;
    public struct MouseActions
    {
        private @MainPlayerControls m_Wrapper;
        public MouseActions(@MainPlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePointer => m_Wrapper.m_Mouse_MousePointer;
        public InputAction @MouseClick => m_Wrapper.m_Mouse_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void AddCallbacks(IMouseActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseActionsCallbackInterfaces.Add(instance);
            @MousePointer.started += instance.OnMousePointer;
            @MousePointer.performed += instance.OnMousePointer;
            @MousePointer.canceled += instance.OnMousePointer;
            @MouseClick.started += instance.OnMouseClick;
            @MouseClick.performed += instance.OnMouseClick;
            @MouseClick.canceled += instance.OnMouseClick;
        }

        private void UnregisterCallbacks(IMouseActions instance)
        {
            @MousePointer.started -= instance.OnMousePointer;
            @MousePointer.performed -= instance.OnMousePointer;
            @MousePointer.canceled -= instance.OnMousePointer;
            @MouseClick.started -= instance.OnMouseClick;
            @MouseClick.performed -= instance.OnMouseClick;
            @MouseClick.canceled -= instance.OnMouseClick;
        }

        public void RemoveCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IPlayerControlActions
    {
        void OnLeftStick(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnButtonSouth(InputAction.CallbackContext context);
        void OnButtonNorth(InputAction.CallbackContext context);
        void OnButtonWest(InputAction.CallbackContext context);
        void OnButtonEast(InputAction.CallbackContext context);
        void OnLeftShoulder(InputAction.CallbackContext context);
        void OnRightShoulder(InputAction.CallbackContext context);
    }
    public interface IGameControlActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnMousePointer(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
    }
}
