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
        public Collider PlatformCollider;

        Animator animator;
        PlayerMotionController motionController;
        PlayerInput input;
        ActionController actionController;
        

        [ReadOnly]
        public string CurrentState { get; private set; } = "";
        

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            motionController = GetComponent<PlayerMotionController>();
            input = GetComponent<PlayerInput>();
            actionController = GetComponent<ActionController>();
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

        IEnumerator PlayerIdle()
        {
            CurrentState = "Idle";
            while(true)
            {
                SetMotionParameters();
                SetStateParameters(false, false);
                if (Locker.Locked)
                    yield return null;

                if (input.Movement.magnitude > 0.1)
                {
                    motionController.Move(input.Movement);
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
                if(input.Jump)
                {
                    motionController.Jump();
                    animator.SetTrigger("Jump");
                    ChangeState(PlayerAirborne());
                    yield break;
                }
                
                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator PlayerMove()
        {
            CurrentState = "Move";
            while (true)
            {
                motionController.Move(input.Movement);
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
                else if (input.Jump)
                {
                    motionController.Jump();
                    animator.SetTrigger("Jump");
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }


        IEnumerator PlayerAirborne()
        {
            CurrentState = "Airborne";
            while (true)
            {
                motionController.Move(input.Movement);
                SetMotionParameters();
                if(motionController.OnGround)
                {
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
            while (true)
            {
                motionController.Move(input.Movement);
                SetMotionParameters();
                SetStateParameters(fall: true);

                yield return new WaitForFixedUpdate();

                // to idle
                if (motionController.OnGround)
                {
                    ChangeState(PlayerIdle());
                    yield break;
                }
                // to airborne
                else if (!input.Crouch || !input.Jump)
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }
            }
        }
    }

}