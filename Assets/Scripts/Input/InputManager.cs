﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;

namespace Project.Input
{
    public enum InputSchemes
    {
        GamePad,
        Keyboard,
        Touch,
    }

    public class InputManager : Singleton<InputManager>
    {
        public static GameInput Input;
        public static event Action BeforeInputUpdate;
        public static event Action AfterInputUpdate;
        PlayerInput playerInput;
        public static InputSchemes CurrentInputScheme
        {
            get
            {
                if (Utility.GetGenericPlatform(Application.platform) == GenericPlatform.Mobile)
                    return InputSchemes.Touch;
                switch(Instance.playerInput.currentControlScheme)
                {
                    case "Gamepad":
                        return InputSchemes.GamePad;
                    case "KeyboardMouse":
                        return InputSchemes.Keyboard;
                    case "TouchScreen":
                        return InputSchemes.Touch;
                }
                return InputSchemes.GamePad;
            }
        }
       
        private void Awake()
        {
            Input = new GameInput();
            Input.Enable();
            playerInput = GetComponent<PlayerInput>();
            if (Utility.GetGenericPlatform(Application.platform) == GenericPlatform.Mobile)
            {
                playerInput.enabled = false;
                
            }
            InputUtility.Init(Input);
        }
        // Use this for initialization
        void Start()
        {

        }

        void FixedUpdate()
        {
            InputUtility.Reset();
            BeforeInputUpdate?.Invoke();

            UnityEngine.InputSystem.InputSystem.Update();

            AfterInputUpdate?.Invoke();

        }
    }

}