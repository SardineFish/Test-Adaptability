using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public float ContactThreshold = 0.0625f;
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
        public float GravityScale { get; set; }
        [DisplayInInspector]
        public bool OnGround { get; protected set; }
        [DisplayInInspector]
        public bool WallContacted { get; protected set; }
        public Vector2 SurfaceVelocity
            => velocity - groundBlockVelocity;

        public List<BlockContactData> ContactedBlocks { get; protected set; } = new List<BlockContactData>(32);


        protected event Action<BlockContactData> OnHitGround;
        protected event Action<BlockContactData> OnHitWall;

        public event Action OnPreBlockDetect;

        public event Action<BlockContactData> OnBlockContacted;
        public event Action<Blocks.Block, Vector2> OnBlockWallContacted;
        public event Action<Blocks.Block> OnBlockGroundContacted;

        new Rigidbody2D rigidbody;
        [DisplayInInspector]
        protected Vector2 controlledMovement;
        [DisplayInInspector]
        protected Vector2 forceVelocity;
        [DisplayInInspector]
        protected Vector2 groundBlockVelocity;
        private Vector2 groundBlockVelocityInLastFrame;

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
                rigidbody.gravityScale = Gravity * GravityScale / Mathf.Abs(Physics2D.gravity.y);

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
                    v.x -= groundBlockVelocityInLastFrame.x;
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
            groundBlockVelocityInLastFrame = groundBlockVelocity;
        }

        Dictionary<Blocks.Block, float> contactCount = new Dictionary<Blocks.Block, float>(32);
        void PerformRayCasst(Vector2 center, Vector2 dir, float distance)
        {
            var count = Physics2D.RaycastNonAlloc(center, dir, hits, distance, 1 << 11);
            Debug.DrawLine(center, center + dir * distance);
            for (var i = 0; i < count; i++)
            {
                if (hits[i].collider.isTrigger)
                    continue;
                var block = hits[i].rigidbody
                    ?.GetComponent<GameMap.IBlockInstance>()
                    ?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (!block)
                    continue;
                if (contactCount.ContainsKey(block))
                    contactCount[block]++;
                else
                    contactCount[block] = 1;
            }
        }
        void DetectContact(Vector2 center, Vector2 dir, float offset, float distance, Vector2 extendDir, float size, List<BlockContactData> contactedBlocks)
        {
            contactCount.Clear();
            PerformRayCasst(center + dir * offset + extendDir * size / 2, dir, distance);
            PerformRayCasst(center + dir * offset, dir, distance);
            PerformRayCasst(center + dir * offset - extendDir * size / 2, dir, distance);

            float sum = 0;
            float max = 0;
            foreach(var value in contactCount.Values)
            {
                sum += value;
                max = Mathf.Max(max, value);
            }
            foreach(var pair in contactCount)
            {
                contactedBlocks.Add(new BlockContactData()
                {
                    Block = pair.Key,
                    ContactWeight = pair.Value / sum,
                    IsMainContact = pair.Value == max,
                    Normal = -dir
                });
            }
        }

        protected virtual void GetBlockContact()
        {
            // Cast left
            OnPreBlockDetect?.Invoke();

            ContactedBlocks.Clear();

            var center = BodyCollider.transform.position.ToVector2() + BodyCollider.transform.localToWorldMatrix.MultiplyVector(BodyCollider.offset).ToVector2();
            var size = BodyCollider.size + Vector2.one * BodyCollider.edgeRadius * 2;
            DetectContact(center, Vector2.left, size.x / 2 , ContactThreshold, Vector2.up, size.y, ContactedBlocks);
            DetectContact(center, Vector2.right, size.x / 2 , ContactThreshold, Vector2.up, size.y, ContactedBlocks);
            DetectContact(center, Vector2.down, size.y / 2 , ContactThreshold, Vector2.left, size.x, ContactedBlocks);
            DetectContact(center, Vector2.up, size.y / 2 , ContactThreshold, Vector2.left, size.x, ContactedBlocks);

            // First pass for internal physical update.
            for (var i = 0; i < ContactedBlocks.Count; i++)
            {
                var contact = ContactedBlocks[i];
                if (Mathf.Abs(Vector2.Dot(Vector2.up, contact.Normal)) < 0.01f)
                {
                    OnHitWall?.Invoke(contact);
                }
                else if(Vector2.Dot(Vector2.up, contact.Normal) >0.9f)
                {
                    OnHitGround?.Invoke(contact);
                }
            }
            
            // Second pass for external usage.
            for (var i = 0; i < ContactedBlocks.Count; i++)
            {
                var contact = ContactedBlocks[i];
                OnBlockContacted?.Invoke(contact);
                if (Mathf.Abs(Vector2.Dot(Vector2.up, contact.Normal)) < 0.01f)
                {
                    OnBlockWallContacted?.Invoke(contact.Block, contact.Normal);
                }
                else if (Vector2.Dot(Vector2.up, contact.Normal) > 0.9f)
                {
                    OnBlockGroundContacted?.Invoke(contact.Block);
                }
            }
        }

        void UpdateCollision()
        {

        }
    }

}