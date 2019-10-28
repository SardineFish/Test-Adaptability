using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="PlatformBlock", menuName ="Blocks/Platform")]
    public class Platform : Block
    {
        public override void ProcessMergedBlocks(MergedBlocks blocks)
        {
            var instance = GameMap.BlocksMap.CreateBlockInstance(this);
            instance.transform.position = blocks.Bound.center;
            instance.Blocks = blocks.Blocks;
            instance.EnableRenderer = true;
            instance.gameObject.layer = 10;
            instance.gameObject.name = $"Platform{blocks.Bound.center.x}-{blocks.Bound.center.y}";
            var collider = instance.gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(blocks.Bound.size.x, blocks.Bound.size.y);
            collider.usedByEffector = true;
            var effector = instance.gameObject.AddComponent<PlatformEffector2D>();
            effector.useColliderMask = false;
            effector.sideArc = 0;
            effector.surfaceArc = 170;
            var rigidBody = instance.gameObject.AddComponent<Rigidbody2D>();
            rigidBody.bodyType = RigidbodyType2D.Static;
            var platform = instance.gameObject.AddComponent<GameMap.PlatformInstance>();
        }

        public override void PostBlockProcess(BlockData data)
        {
            GameMap.BlocksMap.RemoveBlock(data.Position);
        }
    }

}