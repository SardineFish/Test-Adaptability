using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Project.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace Project.GameMap
{
    public class BlocksMap : Singleton<BlocksMap>
    {
        public Transform BoundaryObject;
        public Transform InstanceBlocks;
        public BoundsInt Bound;
        public List<Block> Blocks { get; private set; }
        Tilemap tilemap;

        private void Reset()
        {
            BoundaryObject = transform.Find("Boundary");
            if(!BoundaryObject)
            {
                var obj = new GameObject();
                obj.name = "Boundary";
                obj.transform.parent = transform;
            }
            InstanceBlocks = transform.Find("Instances");
            if(!InstanceBlocks)
            {
                var obj = new GameObject();
                obj.name = "Instances";
                obj.transform.parent = transform;
            }
        }

        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }

        private void Start()
        {
            if(BoundaryObject)
            {
                var collider = BoundaryObject.GetComponent<BoxCollider2D>();
                collider.offset = new Vector2(Bound.position.x, Bound.position.y);
                collider.size = new Vector2(Bound.size.x, Bound.size.y);
            }

        }

        List<BlockData> TraverseBlocks()
        {
            var blocks = new List<BlockData>(Bound.size.x * Bound.size.y);

            for (var y = Bound.position.y; y < Bound.size.y; y++)
            {
                for (var x = Bound.position.x; x < Bound.size.x; x++)
                {
                    var block = tilemap.GetTile(new Vector3Int(x, y, 0)) as Block;
                    if(!(block is null))
                    {
                        blocks.Add(block.GetBlockData(new Vector2Int(x, y)));
                    }
                }
            }
            return blocks;
        }

        MergedBlocks MergeBlocks(BlockData startBlock, HashSet<BlockData> visitedBlocks)
        {
            throw new System.NotImplementedException();
            /*visitedBlocks.Add(startBlock);
            var blocks = new MergedBlocks();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>();
            blocksToVisit = new Queue<BlockData>();
            while (blocksToVisit.Count > 0)
            {
                var block = blocksToVisit.Dequeue();
                blocks.Blocks.Add(block);
                var next = tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int());
                if(next && !visitedBlocks.Contains(next))
                {
                    visitedBlocks
                }
                    
            }*/
        }

        IEnumerable<MergedBlocks> GetMergeBlocks(List<BlockData> blocks)
        {
            HashSet<BlockData> visitedBlocks = new HashSet<BlockData>();
            return blocks
                .Where(block => !visitedBlocks.Contains(block))
                .Select((block) => MergeBlocks(block, visitedBlocks));
        }

        public static BlockType GetTouchedBlockType(Vector2 point, Vector2 normal)
        {
            point = point - normal * 0.01625f;
            var tile = Instance.tilemap.GetTile<TypedTile>(new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0));
            if (tile)
                return tile.BlockType;
            return BlockType.None;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Bound.center, Bound.size);
        }
    }
}