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
        public float CoyoteTime = 0.033f;
        public Locker Locker = new Locker();
        public bool Locked => Locker.Locked;
        [ReadOnly]
        public bool OnGround { get; protected set; }
        [ReadOnly]
        public bool WallContacted { get; protected set; }
        public Vector2 ContactedWallNormal { get; protected set; }
        [ReadOnly]
        public BooleanCache CachedOnGround { get; protected set; }
        public BooleanCache CachedWallContacted { get; protected set; }

        protected event Action<ContactPoint2D> OnHitGround;
        protected event Action<Collision2D> OnCollide;
        protected event Action<ContactPoint2D> OnHitWall;

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
            CachedOnGround = new BooleanCache(CoyoteTime);
            CachedWallContacted = new BooleanCache(CoyoteTime);

            OnCollide += (collision) =>
            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    var contract = collision.GetContact(i);
                    var localPoint = transform.worldToLocalMatrix.MultiplyPoint(contract.point);
                    var dot = Vector2.Dot(contract.normal, Vector2.up);
                    Debug.DrawLine(contract.point, contract.point + contract.normal, Color.red);
                    if (Mathf.Approximately(1, dot)
                        && Mathf.Abs(localPoint.y) <= OnGroundThreshold
                        && contract.relativeVelocity.y >= -0.01)
                    {
                        //Debug.Log("ground");
                        OnHitGround?.Invoke(contract);
                    }
                    /*
                    else if (dot < 1 && Mathf.Abs(localPoint.y) <= 2 * OnGroundThreshold && contract.normalImpulse >= 0)
                    {
                        //Debug.Log("ground");
                        OnHitGround?.Invoke(contract);
                    }*/
                    dot = Mathf.Abs(Vector2.Dot(contract.normal, Vector2.right));

                    if(dot > 0.9)
                    {
                        OnHitWall?.Invoke(contract);
                    }

                }
            };
            OnHitGround += (contact) =>
            {
                OnGround = true;
                CachedOnGround.Record(Time.fixedUnscaledTime);
            };
            OnHitWall += (contact) =>
            {
                WallContacted = true;
                CachedWallContacted.Record(Time.fixedUnscaledTime);
                ContactedWallNormal = contact.normal;
            };
        }


        protected virtual void FixedUpdate()
        {
            CachedOnGround.Update(Time.fixedUnscaledTime);
            CachedWallContacted.Update(Time.fixedUnscaledTime);

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
            WallContacted = false;
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