using UnityEngine;
using System.Collections;
using System;

namespace Project.Input
{

    public class InputManager : Singleton<InputManager>
    {
        public static event Action BeforeInputUpdate;
        public static event Action AfterInputUpdate;
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