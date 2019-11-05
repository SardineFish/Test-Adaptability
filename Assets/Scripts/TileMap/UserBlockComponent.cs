using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Blocks;
using UnityEngine;

namespace Project.GameMap
{
    public class UserBlockComponent : BlocksCollection
    {
        public int Count;
        public Block BlockType => blocksList[0].BlockType;
        public GameMap.SceneArea Scene { get; private set; }
        public UserBlockComponent(BlocksCollection blocks, int count, SceneArea scene) : base(blocks)
        {
            this.Count = count;
            this.Scene = scene;
        }
        public IEnumerable<BlockData> Rotate(float angle)
        {
            foreach(var block in this)
            {
                yield return new BlockData(MathUtility.Rotate(block.Position, angle * Mathf.Deg2Rad).RoundToVector2Int(), block.BlockType);
            }
        }
        public BoundsInt RotateBound(float angle)
        {
            var bound = this.Bound;
            bound.size = MathUtility.Rotate(bound.size.ToVector2(), angle * Mathf.Deg2Rad).ToVector3Int();
            return bound;
        }
    }
}
