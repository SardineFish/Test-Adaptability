using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

namespace Project.Blocks
{
    public enum BlockMergeMode
    {
        None,
        Horizontal,
        Vertical,
        Both = Horizontal | Vertical,
        Either,
    }
    public enum BlockDirection : int
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
    [CreateAssetMenu(fileName ="Block",menuName ="Blocks/Block")]
    public class Block : Tile
    {
        public bool AllowWallJump = false;
        public bool Static = true;
        public BlockMergeMode MergeMode = BlockMergeMode.None;
        public BlockDirection BlockDirection = BlockDirection.Up;
        public Vector2 DirectionVector
            => Quaternion.Euler(0, 0, -(int)BlockDirection * 90) * Vector2.up;

        public virtual BlockData GetBlockData(Vector2Int pos)
        {
            return new BlockData(pos, this);
        }

        public virtual void ProcessMergedBlocks(MergedBlocks blocks)
        {

        }

        public virtual void PostBlockProcess(BlockData data)
        {

        }

        public virtual void OnBlockObjectCreated(GameMap.BlockInstance instance, GameObject obj, BlockData block)
        {

        }

        public virtual void UpdateInstance(GameMap.BlockInstance instance)
        {

        }

        public virtual void OnCollision(GameMap.BlockInstance instance, Collision2D collision)
        {

        }

        public virtual void OnTrigger(GameMap.BlockInstance instance, Collider2D collider)
        {

        }

        public virtual IEnumerator ProcessPlayerContacted(GameEntity player, Vector2 point, Vector2 normal)
        {
            return null;
        }

        public virtual BlockData ToBlockData(Vector2Int pos)
            => new BlockData(pos, this);
    }

    public class MergedBlocks
    {
        public List<BlockData> Blocks = new List<BlockData>(4);
        public BoundsInt Bound
        {
            get
            {
                var xMin = Blocks.Min(block => block.Position.x);
                var yMin = Blocks.Min(block => block.Position.y);
                var xMax = Blocks.Max(block => block.Position.x) + 1;
                var yMax = Blocks.Max(block => block.Position.y) + 1;
                return new BoundsInt(
                    xMin, yMin, 0, 
                    xMax - xMin, yMax - yMin, 0);
            }
        }
    }

    public class BlockData
    {
        public Vector2Int Position;
        public Block BlockType;

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

        public override bool Equals(object obj)
        {
            return obj is BlockData data &&
                   Position.Equals(data.Position) &&
                   EqualityComparer<Block>.Default.Equals(BlockType, data.BlockType);
        }

        public override int GetHashCode()
        {
            var hashCode = -36372948;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Block>.Default.GetHashCode(BlockType);
            return hashCode;
        }
    }
}