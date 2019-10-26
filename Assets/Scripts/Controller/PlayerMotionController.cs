using UnityEngine;
using System.Collections;

namespace Project.Controller
{
    public class PlayerMotionController : MotionController
    {
        public float MaxGroundSpeed = 15;
        public float MaxAirborneSpeed = 10;
        public float MaxJumpSpeedX = 10;
        
        [ReadOnly]
        float JumpVelocity => 2 * JumpHeight / (JumpTime/2);

        protected override void Awake()
        {
            base.Awake();
        }

        public bool Move(Vector2 movement)
        {
            if (Locked)
                return false;
            if (OnGround)
                controlledVelocity = movement * MaxGroundSpeed;
            else
                controlledVelocity = movement * MaxAirborneSpeed;
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
            forceVelocity.y = 2 * JumpHeight / JumpTime;
            forceVelocity.x = speedX * MaxJumpSpeedX;
            return true;
        }
    }
}