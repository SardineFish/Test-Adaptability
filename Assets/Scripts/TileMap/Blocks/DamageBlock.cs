using UnityEngine;
using System.Collections;
using Project.GameMap;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="DamageBlock", menuName ="Blocks/Damage")]
    public class DamageBlock : Block
    {
        public override void ProcessMergedBlocks(BlocksCollection blocks)
        {
            var instance = GameMap.BlocksMap.CreateBlockInstance(new GameMap.BlockInstanceOptions()
            {
                BlockType = this,
                Blocks = blocks,
                GenerateRenderer = true,
                GenerateCollider = true,
                IsTrigger = true,
            });
        }

        public override void OnTrigger(BlockInstance instance, Collider2D collider)
        {
            collider.attachedRigidbody?.GetComponent<Player>()?.Kill();
        }
    }

}