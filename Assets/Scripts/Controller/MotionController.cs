using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Project.Controller
{
    public enum ControlType
    {
        Velocity,
        Force,
        Ignored,
        Disable,
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(GameEntity))]
    public class MotionController : EntityBehaviour
    {
        public Vector2 VelocityLimit = new Vector2(100, 100);
        public float FallDownVelocityLimit = 40;
        public Vector2 ControlledVelocityLimit = new Vector2(-1, -1);
        public bool EnableGravity = true;
        public ControlType XControl = ControlType.Velocity;
        public ControlType YControl = ControlType.Ignored;
        public float OnGroundThreshold = 0.0625f;
        public Locker Locker = new Locker();
        public BoxCollider2D BodyCollider;
        public bool Locked => Locker.Locked;
        [DisplayInInspector]
        public float Gravity { get; set; }
        [DisplayInInspector]
        public bool OnGround { get; protected set; }
        [DisplayInInspector]
        public bool WallContacted { get; protected set; }
        public Vector2 SurfaceVelocity
            => velocity - groundBlockVelocity;

        protected event Action<Collision2D> OnCollide;
        public event Action<ContactPoint2D> OnHitGround;
        public event Action<ContactPoint2D> OnHitWall;
        public event Action OnPreBlockDetect;
        public event Action<Blocks.Block, Vector2, Vector2> OnBlockContacted;
        public event Action<Blocks.Block, Vector2> OnBlockWallContacted;
        public event Action<Blocks.Block> OnBlockGroundContacted;

        new Rigidbody2D rigidbody;
        protected Vector2 controlledMovement;
        protected Vector2 forceVelocity;
        protected Vector2 groundBlockVelocity;

        RaycastHit2D[] hits = new RaycastHit2D[64];

        public Vector2 ControlledMovement
        {
            get
            {
                if (Locked)
                    return Vector2.zero;
                return new Vector2(
                    XControl == ControlType.Velocity || XControl == ControlType.Force ? controlledMovement.x : 0,
                    YControl == ControlType.Velocity || YControl == ControlType.Force ? controlledMovement.y : 0
                );
            }
        }
        public Vector2 velocity
        {
            get => rigidbody.velocity;
            set => rigidbody.velocity = value;
        }

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody2D>();

            OnCollide += (collision) =>
            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    var contract = collision.GetContact(i);
                    var localPoint = transform.worldToLocalMatrix.MultiplyPoint(contract.point);
                    var dot = Vector2.Dot(contract.normal, Vector2.up);
                    Debug.DrawLine(contract.point, contract.point + contract.normal, Color.red);
                    if (Mathf.Approximately(1, dot)
                        //&& localPoint.y <= OnGroundThreshold
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
            };
            OnHitWall += (contact) =>
            {
                WallContacted = true;
            };
            PhysicsSystem.BeforePhysicsSimulation += PlayerMotionUpdate;
            PhysicsSystem.BeforePhysicsSimulation += ValueResetBeforePhyiscalUpdate;
            PhysicsSystem.AfterPhysicsSimulation += GetBlockContact;
        }
        private void OnDestroy()
        {
            PhysicsSystem.BeforePhysicsSimulation -= PlayerMotionUpdate;
            PhysicsSystem.BeforePhysicsSimulation -= ValueResetBeforePhyiscalUpdate;
            PhysicsSystem.AfterPhysicsSimulation -= GetBlockContact;
        }

        protected virtual void ValueResetBeforePhyiscalUpdate()
        {
            controlledMovement = Vector2.zero;
            forceVelocity = Vector2.zero;
            OnGround = false;
            WallContacted = false;
        }

        protected virtual void PlayerMotionUpdate()
        {
            groundBlockVelocity = Vector2.zero;
            if (EnableGravity)
            {
                rigidbody.gravityScale = Gravity / Mathf.Abs(Physics2D.gravity.y);

                // Follow the motion block
                if (OnGround)
                {
                    var count = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hits, 0.0625f, 1 << 11);
                    for (int i = 0; i < count; i++)
                    {
                        var data = hits[i].rigidbody
                            ?.GetComponent<GameMap.IBlockInstance>()
                            ?.GetData<Blocks.MotionBlock.MotionData>(hits[i].point, hits[i].normal);
                        if (data != null)
                            groundBlockVelocity = Vector2.Dot(data.velocity, Vector2.right) * Vector2.right;
                    }
                }
            }
            else
            {
                rigidbody.gravityScale = 0;
            }


            var velocity = controlledMovement;
            Vector2 v = velocity;
            switch (XControl)
            {
                case ControlType.Disable:
                    v.x = 0;
                    break;
                case ControlType.Velocity:
                    v.x = Mathf.Clamp(controlledMovement.x, -ControlledVelocityLimit.x, ControlledVelocityLimit.x);
                    break;
                case ControlType.Force:
                    v.x = rigidbody.velocity.x;
                    if (ControlledVelocityLimit.x > 0 && Mathf.Abs(v.x) >= ControlledVelocityLimit.x && MathUtility.SignInt(controlledMovement.x) == MathUtility.SignInt(v.x))
                        break;
                    rigidbody.AddForce(new Vector2(controlledMovement.x, 0), ForceMode2D.Force);
                    break;
                case ControlType.Ignored:
                    v.x = rigidbody.velocity.x;
                    break;
            }
            switch (YControl)
            {
                case ControlType.Disable:
                    v.y = 0;
                    break;
                case ControlType.Velocity:
                    v.y = controlledMovement.y;
                    break;
                case ControlType.Force:
                    v.y = rigidbody.velocity.y;
                    rigidbody.AddForce(new Vector2(0, controlledMovement.y), ForceMode2D.Force);
                    break;
                case ControlType.Ignored:
                    v.y = rigidbody.velocity.y;
                    break;
            }
            v.x = forceVelocity.x != 0 ? forceVelocity.x : v.x;
            v.y = forceVelocity.y != 0 ? forceVelocity.y : v.y;
            if (VelocityLimit.x >= 0)
                v.x = Mathf.Clamp(v.x, -VelocityLimit.x, VelocityLimit.x);
            if (VelocityLimit.y > 0)
                v.y = Mathf.Clamp(v.y, -VelocityLimit.y, VelocityLimit.y);

            // Limit fall down speed;
            if (v.y < 0)
                v.y = Mathf.Clamp(v.y, -FallDownVelocityLimit, 0);

            rigidbody.velocity = v + groundBlockVelocity;

        }

        protected virtual void GetBlockContact()
        {
            // Cast left
            OnPreBlockDetect?.Invoke();
            var count = Physics2D.RaycastNonAlloc(BodyCollider.transform.position.ToVector2() + BodyCollider.offset, Vector2.left, hits, BodyCollider.size.x / 2 + 0.0625f, 1 << 11);
            for (var i = 0; i < count; i++)
            {
                var block = hits[i].rigidbody
                    ?.GetComponent<GameMap.IBlockInstance>()
                    ?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (block)
                {
                    OnBlockContacted?.Invoke(block, hits[i].point, Vector2.right);
                    OnBlockWallContacted?.Invoke(block, hits[i].normal);
                }
            }
            // Cast right
            count = Physics2D.RaycastNonAlloc(BodyCollider.transform.position.ToVector2() + BodyCollider.offset, Vector2.right, hits, BodyCollider.size.x / 2 + 0.0625f, 1 << 11);
            for (var i = 0; i < count; i++)
            {
                var block = hits[i].rigidbody
                    ?.GetComponent<GameMap.IBlockInstance>()
                    ?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (block)
                {
                    OnBlockContacted?.Invoke(block, hits[i].point, Vector2.left);
                    OnBlockWallContacted?.Invoke(block, hits[i].normal);
                }
            }
            // Cast down
            count = Physics2D.RaycastNonAlloc(BodyCollider.transform.position.ToVector2() + BodyCollider.offset, Vector2.down, hits, BodyCollider.size.y / 2 + 0.0625f, 1 << 11);
            for (var i = 0; i < count; i++)
            {
                var block = hits[i].rigidbody
                    ?.GetComponent<GameMap.IBlockInstance>()
                    ?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (block)
                {
                    OnBlockContacted?.Invoke(block, hits[i].point, Vector2.up);
                    OnBlockGroundContacted?.Invoke(block);
                }
            }
            // Cast up
            count = Physics2D.RaycastNonAlloc(BodyCollider.transform.position.ToVector2() + BodyCollider.offset, Vector2.up, hits, BodyCollider.size.y / 2 + 0.0625f, 1 << 11);
            for (var i = 0; i < count; i++)
            {
                var block = hits[i].rigidbody
                    ?.GetComponent<GameMap.IBlockInstance>()
                    ?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (block)
                {
                    OnBlockContacted?.Invoke(block, hits[i].point, Vector2.down);
                }
            }
        }

        void UpdateCollision()
        {

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