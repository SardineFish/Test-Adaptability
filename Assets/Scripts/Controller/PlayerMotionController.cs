using UnityEngine;
using System.Collections;

namespace Project.Controller
{
    public class PlayerMotionController : MotionController
    {
        
        [ReadOnly]
        float JumpVelocity => 2 * JumpHeight / (JumpTime/2);

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

        public bool Jump()
        {
            if (Locked)
                return false;
            forceVelocity.y = JumpVelocity;
            OnGround = false;
            return true;
        }

        public bool JumpWithSpeed(float speedX)
        {
            if (Locked)
                return false;
            forceVelocity.y = JumpVelocity;
            forceVelocity.x = speedX;
            return true;
        }
    }
}