using UnityEngine;
using System.Collections;

namespace Project.Controller
{
    public class PlayerMotionController : MotionController
    {

        protected override void Awake()
        {
            base.Awake();
        }

        public bool Move(Vector2 velocity)
        {
            if (Locked)
                return false;
            controlledMovement = velocity;
            return true;
        }

        public bool Jump(float velocity)
        {
            if (Locked)
                return false;
            forceVelocity.y = velocity;
            OnGround = false;
            return true;
        }

        public bool Jump(Vector2 velocity)
        {
            if (Locked)
                return false;
            forceVelocity = velocity;
            return true;
        }
    }
}