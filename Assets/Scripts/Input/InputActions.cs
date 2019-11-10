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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
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
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""117e89a5-37b8-4568-9a51-6bb4e481c356"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
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
                },
                {
                    ""name"": ""ShowComponents"",
                    ""type"": ""Button"",
                    ""id"": ""37825812-efc0-4a7e-9beb-9da6c23663b8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleComponents"",
                    ""type"": ""Button"",
                    ""id"": ""ebd90d38-43bd-4bc6-92b7-eabe9f4a8938"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Button"",
                    ""id"": ""ee884e0d-72ef-4f40-9351-62c391c819e6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""bf58df67-726c-4424-8ad6-28c4b808420f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleEditMode"",
                    ""type"": ""Button"",
                    ""id"": ""1ed2c471-b298-42de-8125-a902af83fa78"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.4)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b2579fc-fa57-4e95-bf7b-7f3afe31e371"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2047cb49-d6fb-4f41-811b-29e42d52cbfb"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4de295f-006c-40e4-b27c-4ed064ae9188"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d0b46f1-c6a7-4379-a546-a61cfd185336"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a84c82e-e9f8-4fc1-a261-3eaa879a1fe6"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""274aaf2c-2ba5-427b-bf02-02435918eb71"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Arrow"",
                    ""id"": ""7eb3335f-9f82-4f09-a86c-f279a984b808"",
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
                    ""id"": ""42bb1c21-617f-4714-a280-833c33dcc1be"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ebfafa85-f311-486d-9776-38d93e731ca4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3d69369c-a5e7-4e5e-b19b-44ee3d996d74"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7e64353e-b114-4fc9-8bf2-c9003fe90627"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b7d6061a-8512-4e31-a48f-82eba9c3c840"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ShowComponents"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91b7c447-c6f5-4729-a103-6c2155636636"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ShowComponents"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1cf25ca-f912-4cb5-a4d4-5e51c3be1009"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ToggleComponents"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaf06426-6ccf-447f-86df-cbd52126bb00"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;KeyboardMouse;TouchScreen"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86e0113e-7af0-49f8-bbcf-91b460b3b1ac"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;KeyboardMouse;TouchScreen"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acd13554-636c-4895-bdfe-08e3ac4c4064"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleEditMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eb24570-147e-45f9-a7d6-5c6ce5cf01ff"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ToggleEditMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""0704b4ed-ff1c-461f-ad52-59756fd6cee9"",
            ""actions"": [
                {
                    ""name"": ""Navigation"",
                    ""type"": ""Button"",
                    ""id"": ""b1cfec09-add9-47fe-80a9-cd6b9cfc8028"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OK"",
                    ""type"": ""Button"",
                    ""id"": ""8d267357-8151-4b02-a4cf-16abf806bc6b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""d70cea21-482b-45c4-87f9-0eb248d15f64"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce48248b-f859-4901-a8b9-cb6ef0ca91c5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a1cd7a72-1ded-4f1a-aaa1-f94601af0bfa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f805fb5d-f112-4048-9ce7-1fbb5f25d5ff"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e21fd40-943f-4c89-b7e7-1651d1f06db3"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Arrow"",
                    ""id"": ""df7f6fbd-1cd1-4aa8-8d17-b985894433a6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4c45ed42-e6bc-4cb3-815e-2ff552e0c00f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1be46649-136b-4447-a1eb-e130ac117499"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9bdb55f5-1851-4b93-b544-75bc38e225a1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d634c865-42f3-4984-b87d-970b26e5c1af"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""893774ca-7e52-493e-aa30-a79f41e6c1ea"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee231604-130d-4ecd-8540-8261b8a8d897"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""OK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5e82576-631d-47c4-8591-ab9b93053f97"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""OK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7897cf24-ea67-4aa0-a7d4-7cf98c2fbc79"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ca37772-a495-4cc8-9ef7-1727f0ffc757"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a8b052a-804c-426c-8539-e3ad30cbf374"",
                    ""path"": ""*/{Point}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""TouchScreen;KeyboardMouse;Gamepad"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7937a8e-5069-4195-bac3-c4acdc8e7851"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse;Gamepad"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4182a46-3eab-4de3-b466-08691f88c2a6"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd350f43-9343-4643-be07-1a55e09d0a12"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""TouchScreen"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""TouchScreen"",
            ""bindingGroup"": ""TouchScreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
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
            m_EditorMode_Movement = m_EditorMode.FindAction("Movement", throwIfNotFound: true);
            m_EditorMode_CameraMovement = m_EditorMode.FindAction("CameraMovement", throwIfNotFound: true);
            m_EditorMode_Zoom = m_EditorMode.FindAction("Zoom", throwIfNotFound: true);
            m_EditorMode_Rotate = m_EditorMode.FindAction("Rotate", throwIfNotFound: true);
            m_EditorMode_Place = m_EditorMode.FindAction("Place", throwIfNotFound: true);
            m_EditorMode_Remove = m_EditorMode.FindAction("Remove", throwIfNotFound: true);
            m_EditorMode_Exit = m_EditorMode.FindAction("Exit", throwIfNotFound: true);
            m_EditorMode_ShowComponents = m_EditorMode.FindAction("ShowComponents", throwIfNotFound: true);
            m_EditorMode_ToggleComponents = m_EditorMode.FindAction("ToggleComponents", throwIfNotFound: true);
            m_EditorMode_Point = m_EditorMode.FindAction("Point", throwIfNotFound: true);
            m_EditorMode_Click = m_EditorMode.FindAction("Click", throwIfNotFound: true);
            m_EditorMode_ToggleEditMode = m_EditorMode.FindAction("ToggleEditMode", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Navigation = m_UI.FindAction("Navigation", throwIfNotFound: true);
            m_UI_OK = m_UI.FindAction("OK", throwIfNotFound: true);
            m_UI_Back = m_UI.FindAction("Back", throwIfNotFound: true);
            m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
            m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
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
        private readonly InputAction m_EditorMode_Movement;
        private readonly InputAction m_EditorMode_CameraMovement;
        private readonly InputAction m_EditorMode_Zoom;
        private readonly InputAction m_EditorMode_Rotate;
        private readonly InputAction m_EditorMode_Place;
        private readonly InputAction m_EditorMode_Remove;
        private readonly InputAction m_EditorMode_Exit;
        private readonly InputAction m_EditorMode_ShowComponents;
        private readonly InputAction m_EditorMode_ToggleComponents;
        private readonly InputAction m_EditorMode_Point;
        private readonly InputAction m_EditorMode_Click;
        private readonly InputAction m_EditorMode_ToggleEditMode;
        public struct EditorModeActions
        {
            private GameInput m_Wrapper;
            public EditorModeActions(GameInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_EditorMode_Movement;
            public InputAction @CameraMovement => m_Wrapper.m_EditorMode_CameraMovement;
            public InputAction @Zoom => m_Wrapper.m_EditorMode_Zoom;
            public InputAction @Rotate => m_Wrapper.m_EditorMode_Rotate;
            public InputAction @Place => m_Wrapper.m_EditorMode_Place;
            public InputAction @Remove => m_Wrapper.m_EditorMode_Remove;
            public InputAction @Exit => m_Wrapper.m_EditorMode_Exit;
            public InputAction @ShowComponents => m_Wrapper.m_EditorMode_ShowComponents;
            public InputAction @ToggleComponents => m_Wrapper.m_EditorMode_ToggleComponents;
            public InputAction @Point => m_Wrapper.m_EditorMode_Point;
            public InputAction @Click => m_Wrapper.m_EditorMode_Click;
            public InputAction @ToggleEditMode => m_Wrapper.m_EditorMode_ToggleEditMode;
            public InputActionMap Get() { return m_Wrapper.m_EditorMode; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorModeActions set) { return set.Get(); }
            public void SetCallbacks(IEditorModeActions instance)
            {
                if (m_Wrapper.m_EditorModeActionsCallbackInterface != null)
                {
                    Movement.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnMovement;
                    Movement.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnMovement;
                    Movement.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnMovement;
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
                    ShowComponents.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnShowComponents;
                    ShowComponents.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnShowComponents;
                    ShowComponents.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnShowComponents;
                    ToggleComponents.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleComponents;
                    ToggleComponents.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleComponents;
                    ToggleComponents.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleComponents;
                    Point.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPoint;
                    Point.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPoint;
                    Point.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnPoint;
                    Click.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnClick;
                    Click.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnClick;
                    Click.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnClick;
                    ToggleEditMode.started -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleEditMode;
                    ToggleEditMode.performed -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleEditMode;
                    ToggleEditMode.canceled -= m_Wrapper.m_EditorModeActionsCallbackInterface.OnToggleEditMode;
                }
                m_Wrapper.m_EditorModeActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Movement.started += instance.OnMovement;
                    Movement.performed += instance.OnMovement;
                    Movement.canceled += instance.OnMovement;
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
                    ShowComponents.started += instance.OnShowComponents;
                    ShowComponents.performed += instance.OnShowComponents;
                    ShowComponents.canceled += instance.OnShowComponents;
                    ToggleComponents.started += instance.OnToggleComponents;
                    ToggleComponents.performed += instance.OnToggleComponents;
                    ToggleComponents.canceled += instance.OnToggleComponents;
                    Point.started += instance.OnPoint;
                    Point.performed += instance.OnPoint;
                    Point.canceled += instance.OnPoint;
                    Click.started += instance.OnClick;
                    Click.performed += instance.OnClick;
                    Click.canceled += instance.OnClick;
                    ToggleEditMode.started += instance.OnToggleEditMode;
                    ToggleEditMode.performed += instance.OnToggleEditMode;
                    ToggleEditMode.canceled += instance.OnToggleEditMode;
                }
            }
        }
        public EditorModeActions @EditorMode => new EditorModeActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Navigation;
        private readonly InputAction m_UI_OK;
        private readonly InputAction m_UI_Back;
        private readonly InputAction m_UI_Point;
        private readonly InputAction m_UI_Click;
        public struct UIActions
        {
            private GameInput m_Wrapper;
            public UIActions(GameInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Navigation => m_Wrapper.m_UI_Navigation;
            public InputAction @OK => m_Wrapper.m_UI_OK;
            public InputAction @Back => m_Wrapper.m_UI_Back;
            public InputAction @Point => m_Wrapper.m_UI_Point;
            public InputAction @Click => m_Wrapper.m_UI_Click;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    Navigation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                    Navigation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                    Navigation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigation;
                    OK.started -= m_Wrapper.m_UIActionsCallbackInterface.OnOK;
                    OK.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnOK;
                    OK.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnOK;
                    Back.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                    Back.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                    Back.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                    Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Navigation.started += instance.OnNavigation;
                    Navigation.performed += instance.OnNavigation;
                    Navigation.canceled += instance.OnNavigation;
                    OK.started += instance.OnOK;
                    OK.performed += instance.OnOK;
                    OK.canceled += instance.OnOK;
                    Back.started += instance.OnBack;
                    Back.performed += instance.OnBack;
                    Back.canceled += instance.OnBack;
                    Point.started += instance.OnPoint;
                    Point.performed += instance.OnPoint;
                    Point.canceled += instance.OnPoint;
                    Click.started += instance.OnClick;
                    Click.performed += instance.OnClick;
                    Click.canceled += instance.OnClick;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        private int m_TouchScreenSchemeIndex = -1;
        public InputControlScheme TouchScreenScheme
        {
            get
            {
                if (m_TouchScreenSchemeIndex == -1) m_TouchScreenSchemeIndex = asset.FindControlSchemeIndex("TouchScreen");
                return asset.controlSchemes[m_TouchScreenSchemeIndex];
            }
        }
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
            void OnMovement(InputAction.CallbackContext context);
            void OnCameraMovement(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
            void OnPlace(InputAction.CallbackContext context);
            void OnRemove(InputAction.CallbackContext context);
            void OnExit(InputAction.CallbackContext context);
            void OnShowComponents(InputAction.CallbackContext context);
            void OnToggleComponents(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
            void OnToggleEditMode(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNavigation(InputAction.CallbackContext context);
            void OnOK(InputAction.CallbackContext context);
            void OnBack(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
        }
    }
}
