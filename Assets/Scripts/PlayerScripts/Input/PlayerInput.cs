using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Ingame"",
            ""id"": ""e1541cf7-cc81-4316-b0b5-80e53d035d8e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f050d53e-c3a6-47d6-8a7d-4db4792fd499"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""55cad34b-de12-4f25-a230-18a25599a859"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CastSpell"",
                    ""type"": ""Button"",
                    ""id"": ""682bcb91-b3c7-4843-87e7-cc4f6e466e39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Teleport"",
                    ""type"": ""Button"",
                    ""id"": ""40b6db62-1b7a-494a-bf8b-d8a291152cb6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""271ae65d-1ae5-4318-a038-a64f27bfe81a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""27faf7ce-803f-4e73-9020-5d28b78b7bb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActiveItem"",
                    ""type"": ""Button"",
                    ""id"": ""066cdce5-ee9d-41b0-9ec7-7f01a01cd97e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""dd721e09-01b5-4a9a-8ec3-67d11f14cdd6"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1301af86-15d0-4cf4-a5dd-316de601700d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""39fb0134-02c9-4762-a27c-696edc5bc7b4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""caa7a7de-5624-4e51-a1b8-d104fe44261d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c8016844-3d35-4b8a-8484-f6a8d91ce61f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""26c5cdf6-046a-4d6f-9c3f-9b82da79632c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df542b7c-b075-4890-b1aa-9324bea65539"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d077f87-1eb8-42f6-9e14-b2df5d37fc5a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdf63bf0-ccb8-4d30-88d8-9fdb5ab22d86"",
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
                    ""id"": ""dffc3758-f87d-449d-9d11-e4dd56912be9"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActiveItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1824b247-0e32-4990-ad7a-9e8b55bb151b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""5e5eef34-9992-46ac-a29d-22677b7a2bbd"",
            ""actions"": [
                {
                    ""name"": ""PauseIngame"",
                    ""type"": ""Button"",
                    ""id"": ""26b4864e-93f4-43d3-85cd-6f96d8d7633b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fac7b3ca-dacc-481e-9163-dd74b9ed43ef"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseIngame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
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
        // Ingame
        m_Ingame = asset.FindActionMap("Ingame", throwIfNotFound: true);
        m_Ingame_Movement = m_Ingame.FindAction("Movement", throwIfNotFound: true);
        m_Ingame_Melee = m_Ingame.FindAction("Melee", throwIfNotFound: true);
        m_Ingame_CastSpell = m_Ingame.FindAction("CastSpell", throwIfNotFound: true);
        m_Ingame_Teleport = m_Ingame.FindAction("Teleport", throwIfNotFound: true);
        m_Ingame_MousePosition = m_Ingame.FindAction("MousePosition", throwIfNotFound: true);
        m_Ingame_Interact = m_Ingame.FindAction("Interact", throwIfNotFound: true);
        m_Ingame_ActiveItem = m_Ingame.FindAction("ActiveItem", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_PauseIngame = m_UI.FindAction("PauseIngame", throwIfNotFound: true);
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

    // Ingame
    private readonly InputActionMap m_Ingame;
    private IIngameActions m_IngameActionsCallbackInterface;
    private readonly InputAction m_Ingame_Movement;
    private readonly InputAction m_Ingame_Melee;
    private readonly InputAction m_Ingame_CastSpell;
    private readonly InputAction m_Ingame_Teleport;
    private readonly InputAction m_Ingame_MousePosition;
    private readonly InputAction m_Ingame_Interact;
    private readonly InputAction m_Ingame_ActiveItem;
    public struct IngameActions
    {
        private @PlayerInput m_Wrapper;
        public IngameActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Ingame_Movement;
        public InputAction @Melee => m_Wrapper.m_Ingame_Melee;
        public InputAction @CastSpell => m_Wrapper.m_Ingame_CastSpell;
        public InputAction @Teleport => m_Wrapper.m_Ingame_Teleport;
        public InputAction @MousePosition => m_Wrapper.m_Ingame_MousePosition;
        public InputAction @Interact => m_Wrapper.m_Ingame_Interact;
        public InputAction @ActiveItem => m_Wrapper.m_Ingame_ActiveItem;
        public InputActionMap Get() { return m_Wrapper.m_Ingame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IngameActions set) { return set.Get(); }
        public void SetCallbacks(IIngameActions instance)
        {
            if (m_Wrapper.m_IngameActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnMovement;
                @Melee.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnMelee;
                @CastSpell.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnCastSpell;
                @CastSpell.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnCastSpell;
                @CastSpell.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnCastSpell;
                @Teleport.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnTeleport;
                @Teleport.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnTeleport;
                @Teleport.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnTeleport;
                @MousePosition.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnMousePosition;
                @Interact.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnInteract;
                @ActiveItem.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnActiveItem;
                @ActiveItem.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnActiveItem;
                @ActiveItem.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnActiveItem;
            }
            m_Wrapper.m_IngameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @CastSpell.started += instance.OnCastSpell;
                @CastSpell.performed += instance.OnCastSpell;
                @CastSpell.canceled += instance.OnCastSpell;
                @Teleport.started += instance.OnTeleport;
                @Teleport.performed += instance.OnTeleport;
                @Teleport.canceled += instance.OnTeleport;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ActiveItem.started += instance.OnActiveItem;
                @ActiveItem.performed += instance.OnActiveItem;
                @ActiveItem.canceled += instance.OnActiveItem;
            }
        }
    }
    public IngameActions @Ingame => new IngameActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_PauseIngame;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseIngame => m_Wrapper.m_UI_PauseIngame;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @PauseIngame.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseIngame;
                @PauseIngame.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseIngame;
                @PauseIngame.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseIngame;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseIngame.started += instance.OnPauseIngame;
                @PauseIngame.performed += instance.OnPauseIngame;
                @PauseIngame.canceled += instance.OnPauseIngame;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IIngameActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnCastSpell(InputAction.CallbackContext context);
        void OnTeleport(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnActiveItem(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnPauseIngame(InputAction.CallbackContext context);
    }
}
