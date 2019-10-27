using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Project.Blocks
{
    public enum BlockMergeMode
    {
        None,
        Horizontal,
        Vertical,
        Both = Horizontal | Vertical,
    }
    [CreateAssetMenu(fileName ="Block",menuName ="Blocks/Block")]
    public class Block : Tile
    {
        public BlockMergeMode MergeMode = BlockMergeMode.None;
        public virtual BlockData GetBlockData(Vector2Int pos)
        {
            return new BlockData(pos, this);
        }

        public virtual void ProcessMergedBlocks(MergedBlocks blocks)
        {

        }
    }

    public class MergedBlocks
    {
        public List<BlockData> Blocks;
    }

    public class BlockData
    {
        public Vector2Int Position;
        public Block BlockType;

        public BlockData()
        {
        }

        public BlockData(BlockData src)
        {
            Position = src.Position;
            BlockType = src.BlockType;
        }

        public BlockData(Vector2Int position, Block blockType)
        {
            Position = position;
            BlockType = blockType;
        }
    }
}