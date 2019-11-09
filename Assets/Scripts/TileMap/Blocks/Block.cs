using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;
using System;

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
    public class Block : Tile, IBlockGroup
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

        public virtual void ProcessMergedBlocks(BlocksCollection blocks)
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

        public virtual IEnumerator ProcessPlayerContacted(GameEntity player, BlockContactData contact)
        {
            return null;
        }

        public virtual bool OverrideSpecialState(Block previous)
        {
            return false;
        }

        public virtual BlockData ToBlockData(Vector2Int pos)
            => new BlockData(pos, this);

        public Block GetDefault()
        {
            return this;
        }

        public Block GetNext(Block block)
        {
            return this;
        }

        public bool HasBlock(Block block)
        {
            return block == this;
        }
    }

    

    public struct BlockData
    {
        public Vector2Int Position;
        public Block BlockType;
        public bool IsNull { get; private set; }
        public static BlockData Null => new BlockData() { IsNull = true };

        public BlockData(BlockData src)
        {
            Position = src.Position;
            BlockType = src.BlockType;
            IsNull = src.IsNull;
        }

        public BlockData(Vector2Int position, Block blockType)
        {
            Position = position;
            BlockType = blockType;
            IsNull = blockType is null;
        }

        public static bool operator ==(BlockData a, BlockData b)
        {
            if (a.IsNull && b.IsNull)
                return true;
            else if (a.IsNull || b.IsNull)
                return false;

            return a.BlockType == b.BlockType && a.Position == b.Position;
        }
        public static bool operator !=(BlockData a, BlockData b)
            => !(a == b);

        public static bool operator ==(BlockData a, BlockData? b)
        {
            if (a.IsNull && b is null)
                return true;
            else if (a.IsNull || b is null)
                return false;
            return a == b.Value;
        }
        public static bool operator !=(BlockData a, BlockData? b)
            => !(a == b);


        public override bool Equals(object obj)
        {
            return obj is BlockData data &&
                   Position.Equals(data.Position) &&
                   EqualityComparer<Block>.Default.Equals(BlockType, data.BlockType) &&
                   IsNull == data.IsNull;
        }

        public override int GetHashCode()
        {
            if (IsNull)
                return 0;
            var hashCode = 1214254880;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Block>.Default.GetHashCode(BlockType);
            hashCode = hashCode * -1521134295 + IsNull.GetHashCode();
            return hashCode;
        }
    }
}