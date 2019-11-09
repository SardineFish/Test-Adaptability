using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Controller
{
    [RequireComponent(typeof(Animator), typeof(Player), typeof(PlayerMotionController))]
    [RequireComponent(typeof(PlayerInput), typeof(ActionController))]
    public class PlayerController : EntityBehaviour
    {
        public Locker Locker = new Locker();
        public Collider2D PlatformCollider;
        public float SpeedOnGround = 10;
        [Range(0, 1)]
        public float GroundDamping = 0.5f;
        public float ForceInAir = 100;
        public Vector2 AirSpeedLimit = new Vector2(10, 40);
        [Delayed]
        public float JumpHeight = 3;
        [Delayed]
        public float JumpTime = 0.5f;
        public float WallJumpVelocityX = 12;
        public float WallJumpHeight = 3;
        public float WallJumpFreezeTime = 0.3f;
        public float CoyoteTime = 0.05f;

        Animator animator;
        PlayerMotionController motionController;
        PlayerInput input;
        ActionController actionController;

        event Action OnPlatformCollide;
        HashSet<Blocks.Block> contactedBlocks = new HashSet<Blocks.Block>();

        [DisplayInInspector]
        BooleanCache CachedWallContact;
        [DisplayInInspector]
        BooleanCache CachedGroundContact;
        [DisplayInInspector]
        Vector2 contactedWallNormal;

        [DisplayInInspector]
        float WallJumpVelocityY => CalculateJumpVelocity(WallJumpHeight);
        [DisplayInInspector]
        float JumpVelocity => 2 * JumpHeight / (JumpTime / 2);
        [DisplayInInspector]
        float Gravity => 2 * JumpHeight / Mathf.Pow(JumpTime / 2, 2);

        [DisplayInInspector]
        public string CurrentState { get; private set; } = "";
        

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            motionController = GetComponent<PlayerMotionController>();
            input = GetComponent<PlayerInput>();
            actionController = GetComponent<ActionController>();
            CachedWallContact = new BooleanCache(CoyoteTime);
            CachedGroundContact = new BooleanCache(CoyoteTime);

            (Entity as Player).OnPlayerDead += () =>
            {
                StopAllCoroutines();
                StartCoroutine(PlayerIdle());
            };
            motionController.OnBlockWallContacted += (block, normal) =>
            {
                contactedWallNormal = normal;
                if (block && block.AllowWallJump)
                    CachedWallContact.Record(Time.fixedUnscaledTime);
            };
            motionController.OnBlockGroundContacted += (contact) =>
            {
                CachedGroundContact.Record(Time.fixedUnscaledTime);
            };
            motionController.OnPreBlockDetect += () =>
            {
                contactedBlocks.Clear();
            };
            motionController.OnBlockContacted += (contact) =>
            {
                contactedBlocks.Add(contact.Block);
                if(contact.IsMainContact)
                {
                    var processor = contact.Block.ProcessPlayerContacted(Entity, contact);
                    if (processor != null)
                    {
                        StopAllCoroutines();
                        StartCoroutine(SpecialState(processor));
                    }
                }
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PlayerIdle());
        }

        // Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            CachedWallContact.Update(Time.fixedUnscaledTime);
            CachedGroundContact.Update(Time.fixedUnscaledTime);
            motionController.Gravity = Gravity;
            contactedBlocks.Clear();
        }
        public bool IsContactedWith(Blocks.Block block)
            => contactedBlocks.Contains(block);
        public float CalculateJumpVelocity(float height)
            => Mathf.Sqrt(2 * Gravity * height);

        void SetMotionParameters()
        {
            animator.SetBool("OnGround", motionController.OnGround);
            animator.SetFloat("VelocityX", motionController.velocity.x);
            animator.SetFloat("VelocityY", motionController.velocity.y);
            animator.SetFloat("ControlledMovementX", Mathf.Abs(motionController.ControlledMovement.x));
            actionController.SetDirection(input.Movement.x);
        }

        void SetStateParameters(bool crouch = false, bool move = false, bool fall = false)
        {
            animator.SetBool("Crouch", crouch);
            animator.SetBool("Move", move);
            animator.SetBool("Fall", fall);
        }

        Coroutine ChangeState(IEnumerator state) => StartCoroutine(state);


        bool DoJump()
        {
            if(input.CachedJumpPress && CachedGroundContact)
            {
                motionController.Jump(JumpVelocity);
                animator.SetTrigger("Jump");
                input.CachedJumpPress.Clear();
                CachedGroundContact.Clear();
                input.CachedJumpPress.Clear();
                return true;
            }
            return false;
        }

        bool DoWallJump()
        {
            if(CachedWallContact && input.CachedJumpPress)
            {
                actionController.SetDirection(contactedWallNormal.x);
                motionController.Jump(new Vector2(WallJumpVelocityX * contactedWallNormal.x, WallJumpVelocityY));
                input.CachedJumpPress.Clear();
                CachedWallContact.Clear();
                return true;
            }
            return false;
        }

        void DoMoveGround()
        {
            var damping = (1 - Mathf.Sqrt(GroundDamping)) * 60;
            var velocity = Mathf.Lerp(motionController.SurfaceVelocity.x, input.Movement.x * SpeedOnGround, Time.fixedDeltaTime * damping);
            motionController.Move(Vector2.right * velocity);
        }

        IEnumerator PlayerIdle()
        {
            CurrentState = "Idle";
            motionController.ControlledVelocityLimit = new Vector2(SpeedOnGround, AirSpeedLimit.y);
            motionController.XControl = ControlType.Velocity;
            motionController.YControl = ControlType.Ignored;
            while(true)
            {
                SetMotionParameters();
                SetStateParameters(false, false);
                if (Locker.Locked)
                    yield return null;

                if (input.Movement.magnitude > 0.1)
                {
                    DoMoveGround();
                    if(motionController.ControlledMovement.magnitude > 0.1)
                    {
                        ChangeState(PlayerMove());
                        yield break;
                    }
                }
                if(input.Crouch)
                {
                    // Fall down
                    if(input.Jump)
                    {
                        ChangeState(PlayerFall());
                        yield break;
                    }
                    ChangeState(PlayerCrouch());
                    yield break;
                }
                // jump to airborne
                if(DoJump())
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                // fall to airborne
                if (!motionController.OnGround)
                {
                    motionController.velocity = Vector2.Scale(motionController.velocity, new Vector2(0, 1));
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator PlayerMove()
        {
            CurrentState = "Move";
            motionController.ControlledVelocityLimit = new Vector2(SpeedOnGround, AirSpeedLimit.y);
            while (true)
            {
                DoMoveGround();
                SetMotionParameters();
                SetStateParameters(false, true);

                // to idle
                if (motionController.ControlledMovement.magnitude < 0.01)
                {
                    ChangeState(PlayerIdle());
                    yield break;
                }
                else if (input.Crouch)
                {
                    // to fall
                    if(input.Jump)
                    {
                        ChangeState(PlayerFall());
                        yield break;
                    }
                    // to crouch
                    ChangeState(PlayerCrouch());
                    yield break;
                }
                // jump to airborne
                else if (DoJump())
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                // fall to airborne
                if(!motionController.OnGround)
                {
                    motionController.velocity = Vector2.Scale(motionController.velocity, new Vector2(0, 1));
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }


        IEnumerator PlayerAirborne()
        {
            CurrentState = "Airborne";
            motionController.ControlledVelocityLimit = AirSpeedLimit;
            motionController.FallDownVelocityLimit = AirSpeedLimit.y;
            motionController.XControl = ControlType.Force;
            while (true)
            {
                motionController.Move(new Vector2(input.Movement.x * ForceInAir, 0));
                SetMotionParameters();
                if(input.Crouch && input.Jump)
                {
                    ChangeState(PlayerFall());
                    yield break;
                }
                else if (DoWallJump())
                {
                    ChangeState(PlayerWallJump());
                    yield break;
                }
                // Jump in coyote time
                if(DoJump())
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                if(motionController.OnGround)
                {
                    motionController.XControl = ControlType.Velocity;
                    ChangeState(PlayerIdle());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator PlayerCrouch()
        {
            CurrentState = "Crouch";
            while (true)
            {
                SetStateParameters(true, false);
                SetMotionParameters();
                // to idle
                if(!input.Crouch)
                {
                    SetStateParameters(false, false);
                    ChangeState(PlayerIdle());
                    yield break;
                }
                // to fall
                else if (input.Jump)
                {
                    ChangeState(PlayerFall());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator PlayerFall()
        {
            if (PlatformCollider)
                PlatformCollider.enabled = false;
            CurrentState = "Fall";

            motionController.ControlledVelocityLimit = AirSpeedLimit;
            motionController.FallDownVelocityLimit = AirSpeedLimit.y;
            motionController.XControl = ControlType.Force;

            GameMap.TilePlatformManager.Platforms.ForEach(platform => platform.AllowPass(Entity));
            while (true)
            {
                motionController.Move(input.Movement);
                SetMotionParameters();
                SetStateParameters(fall: true);

                yield return new WaitForFixedUpdate();

                // to idle
                if (motionController.OnGround)
                {
                    if (PlatformCollider)
                        PlatformCollider.enabled = true;
                    GameMap.TilePlatformManager.Platforms.ForEach(platform => platform.BlockPass(Entity));
                    motionController.XControl = ControlType.Velocity;
                    ChangeState(PlayerIdle());
                    yield break;
                }
                // to airborne
                else if (!input.Crouch || !input.Jump)
                {
                    if (PlatformCollider)
                        PlatformCollider.enabled = true;
                    GameMap.TilePlatformManager.Platforms.ForEach(platform => platform.BlockPass(Entity));
                    ChangeState(PlayerAirborne());
                    yield break;
                }
            }
        }

        IEnumerator PlayerWallJump()
        {
            var dir = contactedWallNormal.x;
            motionController.ControlledVelocityLimit = AirSpeedLimit;
            motionController.XControl = ControlType.Ignored;

            foreach (var t in Utility.Timer(WallJumpFreezeTime))
            {
                SetMotionParameters();
                actionController.SetDirection(dir);

                if (motionController.OnGround)
                {
                    motionController.XControl = ControlType.Velocity;
                    ChangeState(PlayerIdle());
                    yield break;
                }
                if (DoWallJump())
                {
                    ChangeState(PlayerWallJump());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
            ChangeState(PlayerAirborne());
        }

        IEnumerator SpecialState(IEnumerator processor)
        {
            while(processor.MoveNext())
            {
                SetMotionParameters();
                yield return processor.Current;

            }

            if (motionController.OnGround)
                StartCoroutine(PlayerIdle());
            else
                StartCoroutine(PlayerAirborne());
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }

}