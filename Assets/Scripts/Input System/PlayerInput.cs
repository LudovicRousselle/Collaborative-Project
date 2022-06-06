// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input System/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""f539ae95-a410-4e25-b37d-88a6922f69a7"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d9fefd82-5388-4c5e-8fbd-3c41c5b93642"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""847bb536-6978-444a-a408-c2fd37505400"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rewind"",
                    ""type"": ""Button"",
                    ""id"": ""8556b5cd-25ba-4a1c-9b94-9380a73424cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MarkObject"",
                    ""type"": ""Button"",
                    ""id"": ""5f825524-cbca-4cb4-ac21-96223c9ca35b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7c7dd59f-a332-454a-807f-74a4ce32f0b6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""b2d5fc5b-5f3f-430a-a1b3-1ed644cf3970"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""4b4b9004-ec7c-4ae9-a51b-c9c65b876230"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""BackMenu"",
                    ""type"": ""Button"",
                    ""id"": ""39dc335c-4953-45bb-9a93-277fb1a035ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1c434dc1-48b4-4831-9ddb-5944d4df00fa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""098c2d79-9254-434e-bc93-01bb8ee791be"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64d6aeb1-d3c8-46bc-acfe-f222278e22d7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b59dcbeb-95c0-4e84-9609-46484ffb4842"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4e50db6-6125-4114-83eb-081b2514b480"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a93e9ab-6288-4601-8921-812eb3aa1a35"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e58d059a-f8ca-4963-84ba-39d1c98254eb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rewind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a54259c-62b5-4fcd-a9cc-05c91c5005cf"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MarkObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c324f307-bc92-4d6f-b961-9d0afe76c72e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MarkObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8a85976-11b9-432f-b713-10c244353748"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MarkObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d39cec1-86ef-4eb6-a1fc-7b2ecbe259d8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyboardAxis"",
                    ""id"": ""a4e68cbd-6984-4f12-a30d-b7d0302e8228"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""21dda5d4-61bb-4d0b-94d1-1d329503c3b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c2a5010-9c44-4b32-8e8c-4b496ec6e91f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""868f93b1-2a48-490d-92c9-38f95ce88fbb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""28f5beca-f7e1-4bb8-94f4-8fe57a2fd92d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""14a818b4-3b8f-4bd4-80fc-3bee8a24b629"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44535219-f890-49f1-8986-27b7a9e8a0c1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e85f2ea-3967-48d7-866f-2faf522c3311"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37c0a0fb-7f00-45db-b223-5796b5192da9"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba8ae17a-d8bb-480f-9b30-957993419972"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Jump = m_Default.FindAction("Jump", throwIfNotFound: true);
        m_Default_Interact = m_Default.FindAction("Interact", throwIfNotFound: true);
        m_Default_Rewind = m_Default.FindAction("Rewind", throwIfNotFound: true);
        m_Default_MarkObject = m_Default.FindAction("MarkObject", throwIfNotFound: true);
        m_Default_Move = m_Default.FindAction("Move", throwIfNotFound: true);
        m_Default_PauseGame = m_Default.FindAction("PauseGame", throwIfNotFound: true);
        m_Default_Run = m_Default.FindAction("Run", throwIfNotFound: true);
        m_Default_BackMenu = m_Default.FindAction("BackMenu", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Jump;
    private readonly InputAction m_Default_Interact;
    private readonly InputAction m_Default_Rewind;
    private readonly InputAction m_Default_MarkObject;
    private readonly InputAction m_Default_Move;
    private readonly InputAction m_Default_PauseGame;
    private readonly InputAction m_Default_Run;
    private readonly InputAction m_Default_BackMenu;
    public struct DefaultActions
    {
        private @PlayerInput m_Wrapper;
        public DefaultActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Default_Jump;
        public InputAction @Interact => m_Wrapper.m_Default_Interact;
        public InputAction @Rewind => m_Wrapper.m_Default_Rewind;
        public InputAction @MarkObject => m_Wrapper.m_Default_MarkObject;
        public InputAction @Move => m_Wrapper.m_Default_Move;
        public InputAction @PauseGame => m_Wrapper.m_Default_PauseGame;
        public InputAction @Run => m_Wrapper.m_Default_Run;
        public InputAction @BackMenu => m_Wrapper.m_Default_BackMenu;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnInteract;
                @Rewind.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRewind;
                @Rewind.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRewind;
                @Rewind.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRewind;
                @MarkObject.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMarkObject;
                @MarkObject.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMarkObject;
                @MarkObject.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMarkObject;
                @Move.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @PauseGame.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPauseGame;
                @Run.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRun;
                @BackMenu.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnBackMenu;
                @BackMenu.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnBackMenu;
                @BackMenu.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnBackMenu;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Rewind.started += instance.OnRewind;
                @Rewind.performed += instance.OnRewind;
                @Rewind.canceled += instance.OnRewind;
                @MarkObject.started += instance.OnMarkObject;
                @MarkObject.performed += instance.OnMarkObject;
                @MarkObject.canceled += instance.OnMarkObject;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @BackMenu.started += instance.OnBackMenu;
                @BackMenu.performed += instance.OnBackMenu;
                @BackMenu.canceled += instance.OnBackMenu;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnRewind(InputAction.CallbackContext context);
        void OnMarkObject(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnBackMenu(InputAction.CallbackContext context);
    }
}
