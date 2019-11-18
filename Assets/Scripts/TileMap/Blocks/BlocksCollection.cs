using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Blocks
{
    public class BlocksCollection : GameMap.IDataMap<BlockData>
    {
        protected List<BlockData> blocksList = new List<BlockData>(4);
        Dictionary<Vector2Int, BlockData> locatedBlocks = new Dictionary<Vector2Int, BlockData>(4);

        public BoundsInt Bound { get; private set; }
        /*
        {
            get
            {
                var xMin = blocksList.Min(block => block.Position.x);
                var yMin = blocksList.Min(block => block.Position.y);
                var xMax = blocksList.Max(block => block.Position.x) + 1;
                var yMax = blocksList.Max(block => block.Position.y) + 1;
                return new BoundsInt(
                    xMin, yMin, 0,
                    xMax - xMin, yMax - yMin, 0);
            }
        }*/
        public ReadOnlyCollection<BlockData> BlocksList { get; private set; }
        public BlocksCollection()
        {
            BlocksList = new ReadOnlyCollection<BlockData>(blocksList);
        }
        private void UpdateBound()
        {
            var xMin = blocksList.Min(block => block.Position.x);
            var yMin = blocksList.Min(block => block.Position.y);
            var xMax = blocksList.Max(block => block.Position.x) + 1;
            var yMax = blocksList.Max(block => block.Position.y) + 1;
            Bound = new BoundsInt(
                xMin, yMin, 0,
                xMax - xMin, yMax - yMin, 0);
        }
        public BlocksCollection(IEnumerable<BlockData> source)
        {
            foreach(var block in source)
            {
                this.Set(block);
            }
        }
        protected BlocksCollection(BlocksCollection clone)
        {
            this.blocksList = clone.blocksList;
            this.locatedBlocks = clone.locatedBlocks;
            BlocksList = new ReadOnlyCollection<BlockData>(blocksList);
            UpdateBound();
        }

        public override bool Equals(object obj)
        {
            return obj is BlocksCollection blocks &&
                   this.blocksList.Count == blocks.blocksList.Count &&
                   this.blocksList.All((block, idx) => block == blocks.blocksList[idx]);
        }

        public override int GetHashCode()
        {
            return 54238299 + EqualityComparer<List<BlockData>>.Default.GetHashCode(blocksList);
        }

        public BlockData Get(Vector2Int position)
            => locatedBlocks.ContainsKey(position)
                ? locatedBlocks[position]
                : BlockData.Null;

        public void Set(BlockData block)
            => Set(block.Position, block);
        public void Set(Vector2Int position, BlockData data)
        {
            if (!locatedBlocks.ContainsKey(position))
            {
                blocksList.Add(data);
            }
            locatedBlocks[position] = data;

            UpdateBound();
        }

        public bool Has(Vector2Int position)
            => locatedBlocks.ContainsKey(position);

        public IEnumerator<BlockData> GetEnumerator()
            => blocksList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => blocksList.GetEnumerator();

        public IEnumerable<BlockData> OrderBy<T>(Func<BlockData, T> selector, Func<T, T, int> comparer)
        {
            return blocksList = blocksList.OrderBy(selector, Utility.MakeComparer<T>(comparer)).ToList();
        }

        public void MoveAll(Vector2Int delta)
        {
            blocksList = blocksList.Select(block => new BlockData(block.Position + delta, block.BlockType)).ToList();
            locatedBlocks.Clear();
            blocksList.ForEach(block => locatedBlocks[block.Position] = block);

        }

        public static bool operator ==(BlocksCollection a, BlocksCollection b)
        {
            if (a is null && b is null)
                return true;
            else if (a is null)
                return false;
            return a.Equals(b);
        }
        public static bool operator !=(BlocksCollection a, BlocksCollection b)
        {
            return !(a == b);
        }

    }
}
