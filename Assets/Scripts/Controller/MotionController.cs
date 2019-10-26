using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Project.Controller
{
    [RequireComponent(typeof(Rigidbody2D), typeof(GameEntity))]
    public class MotionController : EntityBehaviour
    {
        public float JumpHeight = 4;
        public float JumpTime = 0.5f;
        public bool EnableGravity = true;
        public bool IgnoreX = false;
        public bool IgnoreY = true;
        public float OnGroundThreshold = 0.0625f;
        public float OnGroundCacheTime = 0.1f;
        public Locker Locker = new Locker();
        public bool Locked => Locker.Locked;
        [ReadOnly]
        public bool OnGround { get; protected set; }
        public BooleanCache CachedOnGround { get; protected set; }

        protected event Action<ContactPoint2D> OnHitGround;
        protected event Action<Collision2D> OnCollide;

        new Rigidbody2D rigidbody;
        protected Vector2 controlledVelocity;
        protected Vector2 forceVelocity;

        public Vector2 ControlledVelocity
        {
            get
            {
                if (Locked)
                    return Vector2.zero;
                return new Vector2(
                    IgnoreX ? 0 : controlledVelocity.x,
                    IgnoreY ? 0 : controlledVelocity.y
                );
            }
        }
        public Vector2 velocity => rigidbody.velocity;

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody2D>();
            CachedOnGround = new BooleanCache(OnGroundCacheTime);

            OnCollide += (collision) =>
            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    var contract = collision.GetContact(i);
                    var localPoint = transform.worldToLocalMatrix.MultiplyPoint(contract.point);
                    var dot = Vector2.Dot(contract.normal, Vector2.up);
                    if (dot < Mathf.Sqrt(.5f))
                        continue;
                    else if (Mathf.Approximately(1, dot)
                        && Mathf.Abs(localPoint.y) <= OnGroundThreshold
                        && contract.relativeVelocity.y >= -0.01)
                    {
                        //Debug.Log("ground");
                        OnHitGround?.Invoke(contract);
                    }
                    else if (dot < 1 && Mathf.Abs(localPoint.y) <= 2 * OnGroundThreshold && contract.normalImpulse >= 0)
                    {
                        //Debug.Log("ground");
                        OnHitGround?.Invoke(contract);
                    }
                }
            };
            OnHitGround += (contact) =>
            {
                OnGround = true;
                CachedOnGround.Record(Time.fixedUnscaledTime);
            };
        }


        protected virtual void FixedUpdate()
        {
            CachedOnGround.Update(Time.fixedUnscaledTime);
            if(EnableGravity)
            {
                // To allow jumping adjustment by height & time.
                var gravity = 2 * JumpHeight / Mathf.Pow(JumpTime / 2, 2);
                var jumpVelocity = Mathf.Sqrt(2 * gravity * JumpHeight);
                rigidbody.gravityScale = gravity / Mathf.Abs(Physics2D.gravity.y);

            }
            else
            {
                rigidbody.gravityScale = 0;
            }

            var velocity = controlledVelocity;
            var v = new Vector2(
                IgnoreX ? rigidbody.velocity.x : velocity.x,
                IgnoreY ? rigidbody.velocity.y : velocity.y
            );
            v.x = forceVelocity.x != 0 ? forceVelocity.x : v.x;
            v.y = forceVelocity.y != 0 ? forceVelocity.y : v.y;
            rigidbody.velocity = v;

            controlledVelocity = Vector2.zero;
            forceVelocity = Vector2.zero;
            OnGround = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollide?.Invoke(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollide?.Invoke(collision);
        }
    }

}