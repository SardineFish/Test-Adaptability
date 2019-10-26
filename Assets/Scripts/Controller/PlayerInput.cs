﻿using UnityEngine;
using System.Collections;
using Project.Input;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Project.Controller
{
    public class PlayerInput : MonoBehaviour, GameInput.IGamePlayActions
    {
        public float JumpCacheTime = 0.1f;
        [ReadOnly]
        public Vector2 Movement { get; private set; }
        [ReadOnly]
        public bool Jump { get; private set; }
        [ReadOnly]
        public bool Crouch { get; private set; }
        [ReadOnly]
        public bool Interact { get; private set; }
        [ReadOnly]
        public float CameraZoom { get; private set; }
        public BooleanCache CachedJump { get; private set; }

        GameInput input;
        void Awake()
        {
            input = new GameInput();
            input.GamePlay.SetCallbacks(this);
            CachedJump = new BooleanCache(JumpCacheTime);
        }

        void OnEnable()
        {
            input.Enable();
        }

        void OnDisable()
        {
            input.Disable();
        }

        void FixedUpdate()
        {
            CachedJump.Update(Time.fixedUnscaledTime);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValue<float>() > 0.5f;
            if (Jump)
                CachedJump.Record(Time.fixedUnscaledTime);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValue<float>() > 0.5;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Interact = context.ReadValue<float>() > 0.5;
        }

        public void OnCameraZoom(InputAction.CallbackContext context)
        {
            CameraZoom = context.ReadValue<float>();
        }
    }

}
