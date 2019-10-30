using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="PlatformBlock", menuName ="Blocks/Platform")]
    public class Platform : Block
    {
        public override void ProcessMergedBlocks(MergedBlocks blocks)
        {
            var instance = GameMap.BlocksMap.CreateBlockInstance(new GameMap.BlockInstanceOptions()
            {
                Blocks = blocks,
                BlockType = this,
                GenerateRenderer = true,
            });
            var collider = instance.gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(blocks.Bound.size.x, blocks.Bound.size.y);
            collider.usedByEffector = true;
            var effector = instance.gameObject.AddComponent<PlatformEffector2D>();
            effector.useColliderMask = false;
            effector.sideArc = 0;
            effector.surfaceArc = 170;
            var rigidBody = instance.gameObject.GetComponent<Rigidbody2D>();
            rigidBody.bodyType = RigidbodyType2D.Static;
            var platform = instance.gameObject.AddComponent<GameMap.PlatformInstance>();
        }

        public override void PostBlockProcess(BlockData data)
        {
            GameMap.BlocksMap.RemoveBlock(data.Position);
        }
    }

}