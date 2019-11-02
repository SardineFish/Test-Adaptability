using UnityEngine;
using System.Collections;
using Project.GameMap;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="ExplosionBlock", menuName ="Blocks/Explosion")]
    public class ExplosionBlock : Block
    {
        [Range(0, 4)]
        public int ExplosionRange = 1;
        public float CountDownTime = 1f;
        public float RecoverTime = 1f;
        public bool TriggerByBlock = false;
        public RuntimeAnimatorController ExplosionAnimator;
        public override void ProcessMergedBlocks(MergedBlocks blocks)
        {
            var instance = BlocksMap.CreateBlockInstance(new BlockInstanceOptions()
            {
                BlockType = this,
                Blocks = blocks,
                GenerateRenderer = true,
                GenerateCollider = true,
                Data = new Data(),
            });
        }
        public override void OnBlockObjectCreated(BlockInstance instance, GameObject obj, BlockData block)
        {
            var explosion = obj.AddComponent<ExplosionBlockInstance>();
            explosion.BlockInstance = instance;
            explosion.BlockData = block;
            instance.GetData<Data>().Blocks.SetDataAt(block.Position, explosion);
        }
        public class Data  : GameMap.BlockInstanceData
        {
            public BlockDataCollection<GameMap.ExplosionBlockInstance> Blocks = new BlockDataCollection<ExplosionBlockInstance>();
        }
    }

}