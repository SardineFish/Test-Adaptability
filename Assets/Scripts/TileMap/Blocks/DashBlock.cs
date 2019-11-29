using UnityEngine;
using System.Collections;
using System.Linq;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="DashBlock",menuName ="Blocks/Dash")]
    public class DashBlock : Block
    {
        public float Speed;
        public override bool OverrideSpecialState(Block previous)
        {
            if(this.BlockDirection == BlockDirection.Up || BlockDirection == BlockDirection.Down)
            {
                if (previous.BlockDirection == BlockDirection.Left || BlockDirection == BlockDirection.Right)
                    return true;
            }
            return false;
        }
        public override IEnumerator ProcessPlayerContact(GameEntity player, BlockContactData contact)
        {
            var motionController = player.GetComponent<Controller.PlayerMotionController>();
            var playerController = player.GetComponent<Controller.PlayerController>();
            var input = player.GetComponent<Controller.PlayerInput>();
            if (Vector2.Dot(contact.Normal, DirectionVector) < -0.9f)
                yield break;
            if(BlockDirection == BlockDirection.Down || BlockDirection == BlockDirection.Up)
            {
                motionController.YControl = Controller.ControlType.Velocity;
                motionController.ControlledVelocityLimit = new Vector2(Speed, Speed);
                motionController.Move(Speed * DirectionVector);
                while(playerController.IsContactedWith(this))
                {
                    motionController.Move(Speed * DirectionVector);
                    
                    if(Vector2.Dot(input.Movement, contact.Normal) > 0)
                    {
                        motionController.YControl = Controller.ControlType.Ignored;
                        yield break;
                    }
                    if (input.CachedJumpPress)
                    {
                        motionController.YControl = Controller.ControlType.Ignored;
                        yield break;
                    }

                    yield return new WaitForFixedUpdate();
                }
                motionController.YControl = Controller.ControlType.Ignored;
                yield break;
            }
            else if (BlockDirection == BlockDirection.Left || BlockDirection == BlockDirection.Right)
            {
                motionController.XControl = Controller.ControlType.Velocity;
                motionController.Move(Speed * DirectionVector);
                motionController.ControlledVelocityLimit = new Vector2(Speed, Speed);
                while (playerController.IsContactedWith(this))
                {
                    motionController.Move(Speed * DirectionVector);
                    if (input.CachedJumpPress)
                    {
                        motionController.YControl = Controller.ControlType.Ignored;
                        motionController.XControl = Controller.ControlType.Velocity;
                        yield break;
                    }

                    yield return new WaitForFixedUpdate();
                }
                yield break;
            }
        }
    }

}