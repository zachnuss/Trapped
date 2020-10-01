// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""2bf8cb3a-acdc-4339-a93d-925b95787000"",
            ""actions"": [
                {
                    ""name"": ""Move1"",
                    ""type"": ""Value"",
                    ""id"": ""75917092-b8b8-48b4-bfbf-9a91ddc51f83"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b90fb597-d7d1-4a40-90c9-9199d6675e04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ebc5a850-33ac-4854-9186-a53168d15948"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""e038c58a-37ce-46fd-8335-74b3f020f307"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b7dc5e8c-75f2-43b4-a5f9-ffdea30e1378"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""407a1759-1930-42bc-8ed3-b49bc671ff37"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa935b3d-0d1b-42f4-b20c-e63db2120dbb"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fce1fc17-c349-4ecf-808a-6c57b2a490ab"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Move1 = m_PlayerControls.FindAction("Move1", throwIfNotFound: true);
        m_PlayerControls_Attack = m_PlayerControls.FindAction("Attack", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        m_PlayerControls_Return = m_PlayerControls.FindAction("Return", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move1;
    private readonly InputAction m_PlayerControls_Attack;
    private readonly InputAction m_PlayerControls_Pause;
    private readonly InputAction m_PlayerControls_Return;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move1 => m_Wrapper.m_PlayerControls_Move1;
        public InputAction @Attack => m_Wrapper.m_PlayerControls_Attack;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputAction @Return => m_Wrapper.m_PlayerControls_Return;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move1.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove1;
                @Move1.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove1;
                @Move1.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove1;
                @Attack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Return.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReturn;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move1.started += instance.OnMove1;
                @Move1.performed += instance.OnMove1;
                @Move1.canceled += instance.OnMove1;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMove1(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
    }
}
