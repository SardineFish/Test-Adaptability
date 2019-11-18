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
}