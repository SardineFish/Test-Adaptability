using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Blocks
{
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
