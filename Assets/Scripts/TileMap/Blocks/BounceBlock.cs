using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="BounceBlock",menuName ="Blocks/Bounce")]
    public class BounceBlock : Block
    {
        public float BounceHeight = 5;
        public override IEnumerator ProcessPlayerContact(GameEntity player, BlockContactData contact)
        {
            if(Vector2.Dot(contact.Normal, Vector2.up) > 0.9f)
            {
                var motionController = player.GetComponent<Controller.PlayerMotionController>();
                var playerController = player.GetComponent<Controller.PlayerController>();
                motionController.Jump(new Vector2(motionController.velocity.x, playerController.CalculateJumpVelocity(BounceHeight)));
                yield break;
            }
            yield break;

        }
    }

}