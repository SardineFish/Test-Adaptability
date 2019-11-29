using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="SlickBlock", menuName ="Blocks/Slick")]
    public class SlickBlock : Block
    {
        public float MaxMoveSpeed = 12;
        public float MoveForce = 50;

        public override IEnumerator ProcessPlayerContact(GameEntity player, BlockContactData contact)
        {
            if(Vector2.Dot(contact.Normal, Vector2.up) > 0.9f)
            {
                var playerController = player.GetComponent<Controller.PlayerController>();
                var motionController = player.GetComponent<Controller.PlayerMotionController>();
                var input = player.GetComponent<Controller.PlayerInput>();

                motionController.XControl = Controller.ControlType.Force;
                
                while(playerController.IsContactedWith(this))
                {
                    if (!motionController.OnGround)
                        yield break;
                    if(input.CachedJumpPress)
                    {
                        motionController.Jump(playerController.CalculateJumpVelocity(playerController.JumpHeight));
                        yield break;
                    }
                    motionController.Move(input.Movement * MoveForce);

                    yield return new WaitForFixedUpdate();
                }
            }
            yield break;
        }
    }

}
