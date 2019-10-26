using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Controller
{
    [RequireComponent(typeof(Animator), typeof(Player), typeof(PlayerMotionController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : EntityBehaviour
    {
        public Locker Locker = new Locker();

        Animator animator;
        PlayerMotionController motionController;
        PlayerInput input;

        [ReadOnly]
        public string CurrentState { get; private set; } = "";
        

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            motionController = GetComponent<PlayerMotionController>();
            input = GetComponent<PlayerInput>();
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
        }

        void SetStateParameters(bool crouch, bool move)
        {
            animator.SetBool("Crouch", crouch);
            animator.SetBool("Move", move);
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

                if (input.Movement.magnitude > 0.01)
                {
                    ChangeState(PlayerMove());
                    yield break;
                }
                else if(input.Crouch)
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
                else if(input.Jump)
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
            CurrentState = "Fall";
            while (true)
            {
                motionController.Move(input.Movement);
                SetMotionParameters();
                // to idle
                if (motionController.OnGround)
                {
                    ChangeState(PlayerIdle());
                    yield break;
                }
                // to airborne
                else if(!input.Crouch || !input.Jump)
                {
                    ChangeState(PlayerAirborne());
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }

}