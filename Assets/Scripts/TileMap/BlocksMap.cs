using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Project.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace Project.GameMap
{
    [RequireComponent(typeof(TilePlatformManager))]
    public class BlocksMap : Singleton<BlocksMap>
    {
        public BlockSet BlockSet;
        public Tilemap SceneLayer;
        public Tilemap VisibilityLayer;
        public Tilemap EffectLayer;
        public Tilemap BaseLayer;
        public Tilemap UserLayer;
        public Tilemap PlacementLayer;
        public Tilemap GameMap;
        public Transform BoundaryObject;
        public Transform InstanceBlocks;
        public StaticBlocks StaticBlocks;
        public BoundsInt Bound;
        public List<BlockData> Blocks { get; private set; }

        private void Reset()
        {
            BaseLayer = GetOrCreateLayer("BaseLayer");
            UserLayer = GetOrCreateLayer("UserLyaer");
            EffectLayer = GetOrCreateLayer("EffectLayer");
            EffectLayer.color = Utility.SetAlpha(EffectLayer.color, 0.5f);
            EffectLayer.gameObject.layer = 15;
            SceneLayer = GetOrCreateLayer("SceneLayer");
            SceneLayer.color = Utility.SetAlpha(Color.green, 0.5f);
            SceneLayer.gameObject.layer = 15;
            VisibilityLayer = GetOrCreateLayer("VisibilityLayer");
            VisibilityLayer.color = Utility.SetAlpha(Color.cyan, 0.5f);
            VisibilityLayer.gameObject.layer = 15;
        }

        Tilemap GetOrCreateLayer(string name)
        {
            var obj = transform.Find(name)?.gameObject ?? new GameObject(name);
            obj.transform.parent = transform;
            var tilemap = obj.GetOrAddComponent<Tilemap>();
            var renderer = obj.GetOrAddComponent<TilemapRenderer>();
            return tilemap;
        }

        private void Awake()
        {
            BoundaryObject = transform.Find("Boundary");
            if (!BoundaryObject)
            {
                var obj = new GameObject();
                obj.name = "Boundary";
                obj.transform.parent = transform;
                obj.AddComponent<BoxCollider2D>();
                obj.AddComponent<CompositeCollider2D>();
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
                obj.transform.localPosition = obj.transform.localPosition.Set(z: -1);
            }
            if(!PlacementLayer)
            {
                var obj = (transform.Find("Placement")?.gameObject ?? new GameObject("Placement"));
                PlacementLayer = obj.AddComponent<Tilemap>();
                PlacementLayer.transform.parent = transform;
                PlacementLayer.transform.localPosition = PlacementLayer.transform.localPosition.Set(z: -2);
            }
            if(!GameMap)
            {
                var obj = new GameObject("GameMap");
                obj.transform.parent = transform;
                GameMap = obj.AddComponent<Tilemap>();
            }
        }

        private void Start()
        {
            UserLayer.gameObject.SetActive(false);
            InitBoundary();
            StartEditMode();
        }

        public List<Editor.UserComponentUIData> GetUserComponents()
        {
            var userComponents = GetUserMapComponents()
                .GroupBy(merged => merged, Utility.MakeEqualityComparer<MergedBlocks>((a, b) => a.Equals(b)))
                .Select(group => new UserBlockComponent(group.Key, group.Count()))
                .ToList();
            Vector2Int pos = new Vector2Int(0, -20);
            foreach (var component in userComponents)
            {
                foreach (var block in component.Blocks)
                {
                    StaticBlocks.TileMap.SetTile((pos + block.Position).ToVector3Int(), block.BlockType);
                }
                pos += new Vector2Int(component.Bound.size.x + 1, 0);
            }
            return userComponents.Select(component => new Editor.UserComponentUIData(component)).ToList();
        }

        public void InitBoundary()
        {
            // Set up boundary & set camera confine
            if (BoundaryObject)
            {
                BoundaryObject.gameObject.layer = 13;
                var collider = BoundaryObject.GetComponent<BoxCollider2D>();
                var composite = BoundaryObject.GetComponent<CompositeCollider2D>();
                var rigidBody = BoundaryObject.GetComponent<Rigidbody2D>();
                collider.usedByComposite = true;
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
                composite.generationType = CompositeCollider2D.GenerationType.Synchronous;
                rigidBody.bodyType = RigidbodyType2D.Static;

                collider.offset = new Vector2(Bound.position.x + Bound.size.x / 2.0f, Bound.position.y + Bound.size.y / 2.0f);
                collider.size = new Vector2(Bound.size.x, Bound.size.y);

                var confiner = Level.Instance.GamePlayCamera.GetComponent<Cinemachine.CinemachineConfiner>();
                confiner.m_BoundingShape2D = composite;
            }
        }

        public void StartEditorPlay()
        {
            GameMap.ClearAllTiles();
            GenerateGameMap();
            UserLayer.gameObject.SetActive(false);
            BaseLayer.gameObject.SetActive(false);
            Play();
        }

        public void StartEditMode()
        {
            InstanceBlocks.gameObject.ClearChildren();
            StaticBlocks.TileMap.ClearAllTiles();
            PlacementLayer.gameObject.SetActive(true);
            GameMap.ClearAllTiles();
            TraverseBlocks(BaseLayer)
                .ForEach(block => GameMap.SetTile(block.Position.ToVector3Int(), block.BlockType));
            UserLayer.gameObject.SetActive(false);
            BaseLayer.gameObject.SetActive(true);
            BaseLayer.color = Color.white;
        }
        public void StartPlayerMode()
        {
            GameMap.ClearAllTiles();

            TraverseBlocks(BaseLayer)
                .ForEach(block => GameMap.SetTile(block.Position.ToVector3Int(), block.BlockType));

            TraverseBlocks(PlacementLayer)
                .ForEach(block => GameMap.SetTile(block.Position.ToVector3Int(), block.BlockType));

            BaseLayer.gameObject.SetActive(false);

            PlacementLayer.GetComponentsInChildren<Editor.ComponentPlacement>()
                .ForEach(placement => placement.gameObject.SetActive(false));

            Play();
        }

        public void Play()
        {
            Blocks = TraverseBlocks(GameMap);
            foreach (var mergedBlock in GetMergeBlocks(Blocks))
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
        }

        public void GenerateGameMap()
        {
            for (var y = Bound.position.y; y < Bound.size.y + Bound.position.y; y++)
            {
                for (var x = Bound.position.x; x < Bound.size.x + Bound.position.x; x++)
                {
                    var block = BaseLayer.GetTile(new Vector3Int(x, y, 0));
                    GameMap.SetTile(new Vector3Int(x, y, 0), block);
                    var userBlock = UserLayer.GetTile(new Vector3Int(x, y, 0));
                    if (userBlock)
                        GameMap.SetTile(new Vector3Int(x, y, 0), userBlock);
                }
            }
            UserLayer.gameObject.SetActive(false);
            BaseLayer.gameObject.SetActive(false);
        }

        List<BlockData> TraverseBlocks(Tilemap tilemap)
        {
            var blocks = new List<BlockData>(Bound.size.x * Bound.size.y);

            for (var y = Bound.position.y; y < Bound.size.y + Bound.position.y; y++)
            {
                for (var x = Bound.position.x; x < Bound.size.x + Bound.position.x; x++)
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

        public IEnumerable<MergedBlocks> GetUserMapComponents()
        {
            var blocks = TraverseBlocks(UserLayer);
            HashSet<BlockData> visitedBlocks = new HashSet<BlockData>();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>(32);
            foreach (var startBlock in blocks)
            {
                var t = visitedBlocks;
                if (visitedBlocks.Contains(startBlock))
                    continue;
                var merged = new MergedBlocks();
                blocksToVisit.Clear();
                blocksToVisit.Enqueue(startBlock);
                while (blocksToVisit.Count > 0)
                {
                    var block = blocksToVisit.Dequeue();
                    if (visitedBlocks.Contains(block))
                        continue;
                    visitedBlocks.Add(block);
                    merged.Blocks.Add(block);
                    GetNeighborsFrom(block, UserLayer)
                        .Where(next => next != BlockData.Null)
                        .Where(next => !visitedBlocks.Contains(next))
                        .Where(next => next.BlockType == block.BlockType)
                        .ForEach(next => blocksToVisit.Enqueue(next));

                }
                merged.Blocks = merged.Blocks
                                      .OrderBy(b => b.Position, Utility.MakeComparer<Vector2Int>((u, v) => u.y == v.y ? u.x - v.x : u.y - v.y))
                                      .ToList();
                var boundMin = merged.Bound.min.ToVector2Int();
                merged.Blocks = merged.Blocks
                    .Select(block => new BlockData(block.Position - boundMin, block.BlockType))
                    .ToList();

                var pos = merged.Blocks.Select(b => b.Position).ToArray();
                yield return merged;
            }
        }

        BlockData GetNeighbor(BlockData block, Vector2Int delta)
        {
            return new BlockData(block.Position + delta, GameMap.GetTile<Block>((block.Position + delta).ToVector3Int()));
        }

        IEnumerable<BlockData> GetNeighborsFrom(BlockData block, Tilemap tilemap)
        {
            yield return new BlockData(block.Position + new Vector2Int(1, 0), tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(-1, 0), tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(0, 1), tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(0, -1), tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
        }

        IEnumerable<BlockData> GetNeighbors(BlockData block)
        {
            if(block.BlockType.MergeMode == BlockMergeMode.Either)
            {
                var tile = GetNeighbor(block, new Vector2Int(1, 0));
                if(tile!=null)
                {
                    yield return tile;
                    yield return GetNeighbor(block, new Vector2Int(-1, 0));
                    yield break;
                }
                tile = GetNeighbor(block, new Vector2Int(-1, 0));
                if(tile!=null)
                {
                    yield return tile;
                    yield break;
                }

                tile = GetNeighbor(block, new Vector2Int(0, 1));
                if (tile != null)
                {
                    yield return tile;
                    yield return GetNeighbor(block, new Vector2Int(0, -1));
                    yield break;
                }
                tile = GetNeighbor(block, new Vector2Int(0, -1));
                if (tile != null)
                {
                    yield return tile;
                    yield break;
                }
                yield break;
            }
            if((block.BlockType.MergeMode & BlockMergeMode.Horizontal) == BlockMergeMode.Horizontal)
            {
                yield return new BlockData(block.Position + new Vector2Int(1, 0), GameMap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
                yield return new BlockData(block.Position + new Vector2Int(-1, 0), GameMap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
            }
            if((block.BlockType.MergeMode & BlockMergeMode.Vertical) == BlockMergeMode.Vertical)
            {
                yield return new BlockData(block.Position + new Vector2Int(0, 1), GameMap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
                yield return new BlockData(block.Position + new Vector2Int(0, -1), GameMap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int())); 
            }
        }

        MergedBlocks MergeBlocks(BlockData startBlock, HashSet<BlockData> visitedBlocks)
        {
            var mergedBlocks = new MergedBlocks();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>(32);
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
                    .Where(next => next != BlockData.Null)
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
            var tile = Instance.GameMap.GetTile<TypedTile>(new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0));
            if (tile)
                return tile.BlockType;
            return BlockType.None;
        }

        public static BlockInstance CreateBlockInstance(BlockInstanceOptions options)
        {
            options.positionZ = Instance.InstanceBlocks.transform.position.z;
            var instance = BlockInstance.CreateInstance(options);
            instance.transform.parent = Instance.InstanceBlocks.transform;
            return instance;
        }

        public static void RemoveBlock(Vector2Int pos)
            => Instance.GameMap.SetTile(pos.ToVector3Int(), null);

        public static void GetBlock(Vector2Int pos)
            => Instance.GameMap.GetTile<Block>(pos.ToVector3Int());

        public static void GetBlock<T>(Vector2Int pos) where T : Block
            => Instance.GameMap.GetTile<T>(pos.ToVector3Int());

        private void OnDrawGizmosSelected()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bound.center, Bound.size);
        }
    }
}