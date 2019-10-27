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
        public float GroundMoveSpeed = 12;
        public float AirMoveSpeed = 12;
        public float MaxDropSpeed = 20;
        public float AirMoveForce = 10;
        public float CoyoteTime = 0.05f;

        Animator animator;
        PlayerMotionController motionController;
        PlayerInput input;
        ActionController actionController;

        event Action OnPlatformCollide;
        List<GameMap.BlockType> ContactedWallTypes = new List<GameMap.BlockType>(16);

        [ReadOnly]
        BooleanCache CachedWallContact;
        [ReadOnly]
        BooleanCache CachedGroundContact;
        Vector2 contactedWallNormal;

        [ReadOnly]
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

            motionController.OnHitWall += (contact) =>
            {
                contactedWallNormal = contact.normal;

                var type = GameMap.BlocksMap.GetTouchedBlockType(contact.point, contact.normal);
                ContactedWallTypes.Add(type);

                if (type == GameMap.BlockType.SolidBlock)
                    CachedWallContact.Record(Time.fixedUnscaledTime);
            };
            motionController.OnHitGround += (contact) =>
            {
                CachedGroundContact.Record(Time.fixedUnscaledTime);
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
            CachedWallContact.Update(Time.fixedUnscaledTime);
            CachedGroundContact.Update(Time.fixedUnscaledTime);
        }

        void FixedUpdate()
        {
            ContactedWallTypes.Clear();
        }

        void SetMotionParameters()
        {
            animator.SetBool("OnGround", motionController.OnGround);
            animator.SetFloat("VelocityX", motionController.velocity.x);
            animator.SetFloat("VelocityY", motionController.velocity.y);
            actionController.SetDirection(input.Movement.x);
        }

        void SetStateParameters(bool crouch = false, bool move = false, bool fall = false)
        {
            animator.SetBool("Crouch", crouch);
            animator.SetBool("Move", move);
            animator.SetBool("Fall", fall);
        }

        Coroutine ChangeState(IEnumerator state) => StartCoroutine(state);


        void DoJump()
        {
            motionController.Jump();
            animator.SetTrigger("Jump");
            input.CachedJumpPress.Clear();
        }

        IEnumerator PlayerIdle()
        {
            CurrentState = "Idle";
            motionController.VelocityLimit = new Vector2(GroundMoveSpeed, MaxDropSpeed);
            while(true)
            {
                SetMotionParameters();
                SetStateParameters(false, false);
                if (Locker.Locked)
                    yield return null;

                if (input.Movement.magnitude > 0.1)
                {
                    motionController.Move(input.Movement * GroundMoveSpeed);
                    if(motionController.ControlledVelocity.magnitude > 0.1)
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
                if(input.CachedJump && CachedGroundContact)
                {
                    DoJump();
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                // fall to airborne
                if (!motionController.OnGround)
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator PlayerMove()
        {
            CurrentState = "Move";
            motionController.VelocityLimit = new Vector2(GroundMoveSpeed, -1);
            while (true)
            {
                motionController.Move(input.Movement * GroundMoveSpeed);
                SetMotionParameters();
                SetStateParameters(false, true);

                // to idle
                if (motionController.ControlledVelocity.magnitude < 0.01)
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
                else if (input.CachedJump && CachedGroundContact)
                {
                    DoJump();
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                // fall to airborne
                if(!motionController.OnGround)
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }


        IEnumerator PlayerAirborne()
        {
            CurrentState = "Airborne";
            motionController.VelocityLimit = new Vector2(AirMoveSpeed, MaxDropSpeed);
            motionController.XControl = ControlType.Force;
            while (true)
            {
                motionController.Move(new Vector2(input.Movement.x * AirMoveForce, 0));
                SetMotionParameters();
                if(input.Crouch && input.Jump)
                {
                    ChangeState(PlayerFall());
                    yield break;
                }
                else if (motionController.WallContacted && input.CachedJumpPress && ContactedWallTypes.Contains(GameMap.BlockType.SolidBlock))
                {
                    actionController.SetDirection(contactedWallNormal.x);
                    motionController.JumpWithSpeed(contactedWallNormal.x * AirMoveSpeed);
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
            motionController.VelocityLimit = new Vector2(AirMoveSpeed, MaxDropSpeed);
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

        void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }

}