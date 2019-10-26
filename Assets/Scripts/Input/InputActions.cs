// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Project.Input
{
    public class GameInput : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;
        public GameInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""98e2c2ec-dfda-41b8-80e9-5c5f0880ccd6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""493a706e-337b-4e67-b751-f3112a7b74da"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""93a3a4b4-bfba-421d-90cd-8aaab028abbf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""ab9ec2f4-78be-40dd-8219-aa57a3f4da12"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""229241d8-60ff-457c-bacb-c3cb08f91ce4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Button"",
                    ""id"": ""10606575-a314-4f87-8f5c-0284bc7dc359"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24d20567-daf9-44d9-ac43-09c7df269d45"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""022b3b36-c4ad-40fb-983d-1d67402b233e"",
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
                    ""id"": ""4a99f3c6-56c5-4eae-9f7a-961b94c82c3c"",
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
                    ""id"": ""c62781cd-0f13-41bb-ad9d-e01796e450b6"",
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
                    ""id"": ""0d436993-00fc-4da4-a56f-2c6e8d5efff9"",
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
                    ""id"": ""503f11ac-c202-4a98-8141-aa727ed7639b"",
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
                    ""id"": ""dfe2f69b-21b5-43e0-b62d-6f4ce5402c80"",
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
                    ""id"": ""ac26cfdc-12d6-42f1-8522-fdfa227caacb"",
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
                    ""id"": ""70e8fc35-bf9e-4c73-a743-4ea532b6b1db"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27d7b866-e846-46eb-b47a-ae50fd39200a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20603d0d-5e73-433e-9a30-e2eb9710285e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26a890d9-1fc3-4134-925f-885182a1d97a"",
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
                    ""id"": ""14e75629-6ab8-4be6-8758-a0cf4ad246d9"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""8fb4dcc4-e720-4be9-b074-3a444a63d427"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d68e4b84-c31b-45f3-802a-d3d52f7c1316"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad949618-0534-46bb-8cd8-b858dbbca9e5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // GamePlay
            m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
            m_GamePlay_Movement = m_GamePlay.FindAction("Movement", throwIfNotFound: true);
            m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
            m_GamePlay_Crouch = m_GamePlay.FindAction("Crouch", throwIfNotFound: true);
            m_GamePlay_Interact = m_GamePlay.FindAction("Interact", throwIfNotFound: true);
            m_GamePlay_CameraZoom = m_GamePlay.FindAction("CameraZoom", throwIfNotFound: true);
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

        // GamePlay
        private readonly InputActionMap m_GamePlay;
        private IGamePlayActions m_GamePlayActionsCallbackInterface;
        private readonly InputAction m_GamePlay_Movement;
        private readonly InputAction m_GamePlay_Jump;
        private readonly InputAction m_GamePlay_Crouch;
        private readonly InputAction m_GamePlay_Interact;
        private readonly InputAction m_GamePlay_CameraZoom;
        public struct GamePlayActions
        {
            private GameInput m_Wrapper;
            public GamePlayActions(GameInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_GamePlay_Movement;
            public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
            public InputAction @Crouch => m_Wrapper.m_GamePlay_Crouch;
            public InputAction @Interact => m_Wrapper.m_GamePlay_Interact;
            public InputAction @CameraZoom => m_Wrapper.m_GamePlay_CameraZoom;
            public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
            public void SetCallbacks(IGamePlayActions instance)
            {
                if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
                {
                    Movement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                    Movement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                    Movement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMovement;
                    Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Crouch.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCrouch;
                    Crouch.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCrouch;
                    Crouch.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCrouch;
                    Interact.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                    Interact.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                    Interact.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteract;
                    CameraZoom.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraZoom;
                    CameraZoom.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraZoom;
                    CameraZoom.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraZoom;
                }
                m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Movement.started += instance.OnMovement;
                    Movement.performed += instance.OnMovement;
                    Movement.canceled += instance.OnMovement;
                    Jump.started += instance.OnJump;
                    Jump.performed += instance.OnJump;
                    Jump.canceled += instance.OnJump;
                    Crouch.started += instance.OnCrouch;
                    Crouch.performed += instance.OnCrouch;
                    Crouch.canceled += instance.OnCrouch;
                    Interact.started += instance.OnInteract;
                    Interact.performed += instance.OnInteract;
                    Interact.canceled += instance.OnInteract;
                    CameraZoom.started += instance.OnCameraZoom;
                    CameraZoom.performed += instance.OnCameraZoom;
                    CameraZoom.canceled += instance.OnCameraZoom;
                }
            }
        }
        public GamePlayActions @GamePlay => new GamePlayActions(this);
        public interface IGamePlayActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnCameraZoom(InputAction.CallbackContext context);
        }
    }
}
