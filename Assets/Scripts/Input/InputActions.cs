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
                    ""expectedControlType"": ""Button"",
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
                    ""processors"": ""StickDeadzone(min=0.5,max=1)"",
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
        },
        {
            ""name"": ""EditorMode"",
            ""id"": ""413078f5-52fd-423b-97fb-7b6f14948694"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Button"",
                    ""id"": ""31a228b4-8335-48d4-baca-a98284f7d9e2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""c76d216f-57e6-4675-891a-48acfd8334f1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""3dcdd30b-5686-4c7b-9d1f-0d6af33e6f5d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Place"",
                    ""type"": ""Button"",
                    ""id"": ""de9a209e-a58d-4c33-ad48-19dc09cf8ab0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Remove"",
                    ""type"": ""Button"",
                    ""id"": ""4ed12a8e-922c-4d11-8bf0-d142c10289a1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""77667840-6686-4f4e-996d-069a7a94765e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b2579fc-fa57-4e95-bf7b-7f3afe31e371"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""30632d5a-1978-4a12-996d-9885dc6bd967"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d9926b73-7f62-4e81-8e47-975d483f0c4d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ebc88868-3fac-44d2-bd2a-2cb77ba9028c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce01872f-1f20-4306-a845-df614b0ea48c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9bfb38a2-4279-4c28-a875-ba75f25f82ab"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Trigger"",
                    ""id"": ""eb379de0-93f2-477a-a6d5-3412fed6b3d9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9ff5852c-5382-434b-9e3f-fe293d0d4422"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""eee9e328-2cec-413d-b493-3ec70401f5fb"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""942e571b-0c89-4b36-aaea-e01f2348d4fd"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bd6f691-e5bf-46e2-8cac-c62488d1dbf5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0271df8a-b12c-49f9-8a39-ef084aaabe57"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a448c43d-dd99-4095-8a1d-c9012ce7061b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fd3daaf-99da-4b57-a0b2-57ac86792304"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce3d6ec5-4c5e-420a-b2a0-674e6cbde417"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
            // EditorMode
            m_EditorMode = asset.FindActionMap("EditorMode", throwIfNotFound: true);
            m_EditorMode_CameraMovement = m_EditorMode.FindAction("CameraMovement", throwIfNotFound: true);
            m_EditorMode_Zoom = m_EditorMode.FindAction("Zoom", throwIfNotFound: true);
            m_EditorMode_Rotate = m_EditorMode.FindAction("Rotate", throwIfNotFound: true);
            m_EditorMode_Place = m_EditorMode.FindAction("Place", throwIfNotFound: true);
            m_EditorMode_Remove = m_EditorMode.FindAction("Remove", throwIfNotFound: true);
            m_EditorMode_Exit = m_EditorMode.FindAction("Exit", throwIfNotFound: true);
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

        // EditorMode
        private readonly InputActionMap m_EditorMode;
        private IEditorModeActions m_EditorModeActionsCallbackInterface;
        private readonly InputAction m_EditorMode_CameraMovement;
        private readonly InputAction m_EditorMode_Zoom;
        private readonly InputAction m_EditorMode_Rotate;
        private readonly InputAction m_EditorMode_Place;
        private readonly InputAction m_EditorMode_Remove;
        private readonly InputAction m_EditorMode_Exit;
        public struct EditorModeActions
        {
            private GameInput m_Wrapper;
            public EditorModeActions(GameInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @CameraMovement => m_Wrapper.m_EditorMode_CameraMovement;
            public InputAction @Zoom => m_Wrapper.m_EditorMode_Zoom;
            public InputAction @Rotate => m_Wrapper.m_EditorMode_Rotate;
            public InputAction @Place => m_Wrapper.m_EditorMode_Place;
            public InputAction @Remove => m_Wrapper.m_EditorMode_Remove;
            public InputAction @Exit => m_Wrapper.m_EditorMode_Exit;
            public InputActionMap Get() { return m_Wrapper.m_EditorMode; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorModeActions set) { return set.Get(); }
            public void SetCallbacks(IEditorModeActions instance)
            {
                if (m_Wrapper.m_EditorModeActionsCallbackInterface != null)
                {
                    CameraMovement.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnCameraMovement;
                    CameraMovement.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnCameraMovement;
                    CameraMovement.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnCameraMovement;
                    Zoom.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnZoom;
                    Zoom.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnZoom;
                    Zoom.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnZoom;
                    Rotate.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRotate;
                    Rotate.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRotate;
                    Rotate.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRotate;
                    Place.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPlace;
                    Place.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPlace;
                    Place.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPlace;
                    Remove.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRemove;
                    Remove.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRemove;
                    Remove.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnRemove;
                    Exit.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnExit;
                    Exit.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnExit;
                    Exit.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnExit;
                }
                m_Wrapper.m_EditorModeActionsCallbackInterface = instance;
                if (instance != null)
                {
                    CameraMovement.started += instance.OnCameraMovement;
                    CameraMovement.performed += instance.OnCameraMovement;
                    CameraMovement.canceled += instance.OnCameraMovement;
                    Zoom.started += instance.OnZoom;
                    Zoom.performed += instance.OnZoom;
                    Zoom.canceled += instance.OnZoom;
                    Rotate.started += instance.OnRotate;
                    Rotate.performed += instance.OnRotate;
                    Rotate.canceled += instance.OnRotate;
                    Place.started += instance.OnPlace;
                    Place.performed += instance.OnPlace;
                    Place.canceled += instance.OnPlace;
                    Remove.started += instance.OnRemove;
                    Remove.performed += instance.OnRemove;
                    Remove.canceled += instance.OnRemove;
                    Exit.started += instance.OnExit;
                    Exit.performed += instance.OnExit;
                    Exit.canceled += instance.OnExit;
                }
            }
        }
        public EditorModeActions @EditorMode => new EditorModeActions(this);
        public interface IGamePlayActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnCameraZoom(InputAction.CallbackContext context);
        }
        public interface IEditorModeActions
        {
            void OnCameraMovement(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
            void OnPlace(InputAction.CallbackContext context);
            void OnRemove(InputAction.CallbackContext context);
            void OnExit(InputAction.CallbackContext context);
        }
    }
}
