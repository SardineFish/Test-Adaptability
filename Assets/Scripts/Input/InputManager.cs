using UnityEngine;
using System.Collections;
using System;

namespace Project.Input
{

    public class InputManager : Singleton<InputManager>
    {
        public static GameInput Input;
        public static event Action BeforeInputUpdate;
        public static event Action AfterInputUpdate;
        private void Awake()
        {
            Input = new GameInput();
            Input.Enable();
        }
        // Use this for initialization
        void Start()
        {

        }

        void FixedUpdate()
        {
            BeforeInputUpdate?.Invoke();

            UnityEngine.InputSystem.InputSystem.Update();

            AfterInputUpdate?.Invoke();
        }
    }

}