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
        public StaticBlocks StaticBlocks;
        public BoundsInt Bound;
        public List<BlockData> Blocks { get; private set; }
        public Tilemap tileMap;


        private void Awake()
        {
            tileMap = GetComponent<Tilemap>();
            BoundaryObject = transform.Find("Boundary");
            if (!BoundaryObject)
            {
                var obj = new GameObject();
                obj.name = "Boundary";
                obj.transform.parent = transform;
                BoundaryObject = obj.transform;
            }
            InstanceBlocks = transform.Find("Instances");
            if (!InstanceBlocks)
            {
                var obj = new GameObject();
                obj.name = "Instances";
                obj.transform.parent = transform;
                InstanceBlocks = obj.transform;
            }
            var staticBlockTransform = transform.Find("StaticBlocks");
            if (!staticBlockTransform)
            {
                var obj = new GameObject();
                obj.name = "StaticBlocks";
                obj.transform.parent = transform;
                StaticBlocks = obj.AddComponent<StaticBlocks>();

            }
        }

        private void Start()
        {
            if(BoundaryObject)
            {
                BoundaryObject.gameObject.layer = 13;
                var collider = BoundaryObject.GetComponent<BoxCollider2D>() ?? BoundaryObject.gameObject.AddComponent<BoxCollider2D>();
                var composite = BoundaryObject.GetComponent<CompositeCollider2D>() ?? BoundaryObject.gameObject.AddComponent<CompositeCollider2D>();
                var rigidBody = BoundaryObject.GetComponent<Rigidbody2D>() ?? BoundaryObject.gameObject.AddComponent<Rigidbody2D>();
                collider.usedByComposite = true;
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
                composite.generationType = CompositeCollider2D.GenerationType.Synchronous;
                rigidBody.bodyType = RigidbodyType2D.Static;

                collider.offset = new Vector2(Bound.position.x + Bound.size.x / 2.0f, Bound.position.y + Bound.size.y / 2.0f);
                collider.size = new Vector2(Bound.size.x, Bound.size.y);

                var confiner =  Level.Instance.GamePlayCamera.GetComponent<Cinemachine.CinemachineConfiner>();
                confiner.m_BoundingShape2D = composite;
            }

            Blocks = TraverseBlocks();
            foreach(var mergedBlock in GetMergeBlocks(Blocks))
            {
                mergedBlock.Blocks[0].BlockType.ProcessMergedBlocks(mergedBlock);
            }

            foreach (var block in Blocks)
            {
                block.BlockType.PostBlockProcess(block);
            }

            Blocks
                .Where(block => block.BlockType.Static)
                .ForEach(block => this.StaticBlocks.TileMap.SetTile(block.Position.ToVector3Int(), block.BlockType));
            GetComponent<TilemapRenderer>().enabled = false;
        }

        List<BlockData> TraverseBlocks()
        {
            var blocks = new List<BlockData>(Bound.size.x * Bound.size.y);

            for (var y = Bound.position.y; y < Bound.size.y; y++)
            {
                for (var x = Bound.position.x; x < Bound.size.x; x++)
                {
                    var block = tileMap.GetTile(new Vector3Int(x, y, 0)) as Block;
                    if(!(block is null))
                    {
                        blocks.Add(block.GetBlockData(new Vector2Int(x, y)));
                    }
                }
            }
            return blocks;
        }

        IEnumerable<BlockData> GetNeighbors(BlockData block)
        {
            yield return tileMap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int())?.ToBlockData((block.Position + new Vector2Int(1, 0)));
            yield return tileMap.GetTile<Block>((block.Position + new Vector2Int(-1, 0)).ToVector3Int())?.ToBlockData((block.Position + new Vector2Int(-1, 0)));
            yield return tileMap.GetTile<Block>((block.Position + new Vector2Int(0, 1)).ToVector3Int())?.ToBlockData((block.Position + new Vector2Int(0, 1)));
            yield return tileMap.GetTile<Block>((block.Position + new Vector2Int(0, -1)).ToVector3Int())?.ToBlockData((block.Position + new Vector2Int(0, -1)));
        }

        MergedBlocks MergeBlocks(BlockData startBlock, HashSet<BlockData> visitedBlocks)
        {
            var mergedBlocks = new MergedBlocks();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>();
            blocksToVisit = new Queue<BlockData>();
            blocksToVisit.Enqueue(startBlock);
            while (blocksToVisit.Count > 0)
            {
                var x = blocksToVisit.Reverse().Take(100).ToArray();
                var block = blocksToVisit.Dequeue();
                if (visitedBlocks.Contains(block))
                    continue;
                visitedBlocks.Add(block);
                mergedBlocks.Blocks.Add(block);
                GetNeighbors(block)
                    .Where(next => next != null)
                    .Where(next => !visitedBlocks.Contains(next))
                    .Where(next => next.BlockType == block.BlockType)
                    .ForEach(next => blocksToVisit.Enqueue(next));
            }
            return mergedBlocks;
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
            var tile = Instance.tileMap.GetTile<TypedTile>(new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0));
            if (tile)
                return tile.BlockType;
            return BlockType.None;
        }

        public static BlockInstance CreateBlockInstance(Block block)
            => CreateBlockInstance<BlockInstanceData>(block, null);
        public static BlockInstance CreateBlockInstance<T>(Block block, T data) where T : BlockInstanceData
        {
            var obj = new GameObject();
            obj.transform.parent = Instance.InstanceBlocks;
            var instance = obj.AddComponent<BlockInstance>();
            instance.BlockType = block;
            instance.SetData(data);
            
            return instance;
        }

        public static void RemoveBlock(Vector2Int pos)
            => Instance.tileMap.SetTile(pos.ToVector3Int(), null);

        public static void GetBlock(Vector2Int pos)
            => Instance.tileMap.GetTile<Block>(pos.ToVector3Int());

        public static void GetBlock<T>(Vector2Int pos) where T : Block
            => Instance.tileMap.GetTile<T>(pos.ToVector3Int());

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Bound.center, Bound.size);
        }
    }
}