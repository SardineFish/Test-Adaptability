using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Project.Blocks;
using System.Collections.Generic;
using System.Linq;
using System;

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
        public Transform InstanceBlocks;
        public StaticBlocks StaticBlocks;
        public BoundsInt Bound;
        public List<BlockData> Blocks { get; private set; }
        public List<SceneArea> Scenes { get; private set; }

        TiledDataCollection<SceneArea> blockInScene = new TiledDataCollection<SceneArea>();

        public event Action BeforeMapGeneration;
        public event Action AfterMapGeneration;

        private void Reset()
        {
            BaseLayer = GetOrCreateLayer("BaseLayer");
            UserLayer = GetOrCreateLayer("UserLayer");
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
            /*BoundaryObject = transform.Find("Boundary");
            if (!BoundaryObject)
            {
                var obj = new GameObject();
                obj.name = "Boundary";
                obj.transform.parent = transform;
                obj.AddComponent<BoxCollider2D>();
                obj.AddComponent<CompositeCollider2D>();
                BoundaryObject = obj.transform;
            }*/
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
            BeforeMapGeneration?.Invoke();
            UserLayer.gameObject.SetActive(false);
            Scenes = GetSceneAreas().ToList();
            InitBoundary();
            GetSceneComponents();
            SwitchToEditorMap();
            foreach(var scene in Scenes)
            {
                foreach (var block in scene.Blocks)
                    blockInScene.Set(block.Position, scene);
            }
            AfterMapGeneration?.Invoke();
        }

        public SceneArea GetSceneAt(Vector2Int position)
            => blockInScene.Get(position);

        SceneArea GetSceneOfBlocks(BlocksCollection blocks)
            => Scenes.Where(scene => scene.Blocks
                     .Has(blocks.First().Position))
                     .First();
        bool InSameScene(BlocksCollection a, BlocksCollection b)
            => GetSceneOfBlocks(a) == GetSceneOfBlocks(b);

        void GetSceneComponents()
        {
            var userComponents = GetUserMapComponents()
                .GroupBy(merged => merged, Utility.MakeEqualityComparer<UserBlockComponent>((a, b) => a.Scene == b.Scene && a.Equals(b)))
                .Select(group => new UserBlockComponent(group.Key, group.Count(), group.Key.Scene))
                .ToList();
            // debug
            Vector2Int pos = new Vector2Int(0, -20);
            foreach (var component in userComponents)
            {
                /*foreach (var block in component)
                {
                    StaticBlocks.TileMap.SetTile((pos + block.Position).ToVector3Int(), block.BlockType);
                }
                pos += new Vector2Int(component.Bound.size.x + 1, 0);*/

                var scene = component.Scene;
                scene.UserComponents.Add(component);
            }
            //return userComponents.Select(component => new Editor.UserComponentUIData(component)).ToList();
        }

        public void InitBoundary()
        {
            foreach(var scene in Scenes)
            {
                var obj = new GameObject(scene.Name);
                obj.transform.parent = SceneLayer.transform;
                obj.layer = 13;
                var tilemap = obj.AddComponent<Tilemap>();
                foreach (var block in scene.Blocks)
                    tilemap.SetTile(block.Position.ToVector3Int(), block.BlockType);
                var collider = obj.AddComponent<TilemapCollider2D>();
                collider.usedByComposite = true;
                var composite = obj.AddComponent<CompositeCollider2D>();
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
                composite.offsetDistance = 0.01f;
                var rigidbody = obj.GetComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Static;
                scene.BoundaryCollider = composite;
            }
        }



        public void StartEditorPlay()
        {
            GameMap.ClearAllTiles();
            GenerateGameMap();
            UserLayer.gameObject.SetActive(false);
            BaseLayer.gameObject.SetActive(false);
            GenerateBlockInstances();
        }

        public void SwitchToEditorMap()
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
        public void SwitchToPlayMap()
        {
            GameMap.ClearAllTiles();

            TraverseBlocks(BaseLayer)
                .ForEach(block => GameMap.SetTile(block.Position.ToVector3Int(), block.BlockType));

            TraverseBlocks(PlacementLayer)
                .ForEach(block => GameMap.SetTile(block.Position.ToVector3Int(), block.BlockType));

            BaseLayer.gameObject.SetActive(false);

            /*PlacementLayer.GetComponentsInChildren<Editor.ComponentPlacement>()
                .ForEach(placement => placement.gameObject.SetActive(false));*/

            GenerateBlockInstances();
        }

        void GenerateBlockInstances()
        {
            Blocks = TraverseBlocks(GameMap);
            foreach (var mergedBlock in GetMergeBlocks(Blocks))
            {
                mergedBlock.First().BlockType.ProcessMergedBlocks(mergedBlock);
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

        public IEnumerable<SceneArea> GetSceneAreas()
        {
            int i = 0;
            foreach (var blocks in GetMergedBlocksFrom(SceneLayer, (a, b) => true))
            {
                yield return new SceneArea(blocks, $"Scene-{i++}");
            }
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

        public IEnumerable<BlocksCollection> GetMergedBlocksFrom(Tilemap tilemap)
            => GetMergedBlocksFrom(tilemap, (block, neighbor) => block.BlockType == neighbor.BlockType);
        public IEnumerable<BlocksCollection> GetMergedBlocksFrom(Tilemap tilemap, Func<BlockData, BlockData, bool> mergeRule)
        {
            var blocks = TraverseBlocks(tilemap);
            HashSet<BlockData> visitedBlocks = new HashSet<BlockData>();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>(32);
            foreach (var startBlock in blocks)
            {
                var t = visitedBlocks;
                if (visitedBlocks.Contains(startBlock))
                    continue;
                var merged = new BlocksCollection();
                blocksToVisit.Clear();
                blocksToVisit.Enqueue(startBlock);
                while (blocksToVisit.Count > 0)
                {
                    var block = blocksToVisit.Dequeue();
                    if (visitedBlocks.Contains(block))
                        continue;
                    visitedBlocks.Add(block);
                    merged.Set(block);
                    GetNeighborsFrom(block, tilemap)
                        .Where(next => next != BlockData.Null)
                        .Where(next => !visitedBlocks.Contains(next))
                        .Where(next => mergeRule(block, next))
                        .ForEach(next => blocksToVisit.Enqueue(next));

                }
                yield return merged;
            }
        }

        IEnumerable<UserBlockComponent> GetUserMapComponents()
        {
            foreach(var merged in GetMergedBlocksFrom(UserLayer))
            {
                var scene = GetSceneOfBlocks(merged);
                merged.OrderBy(b => b.Position, (u, v) => u.y == v.y ? u.x - v.x : u.y - v.y);
                var boundMin = merged.Bound.min.ToVector2Int();
                merged.MoveAll(Vector2Int.zero - boundMin);
                yield return new UserBlockComponent(merged, 0, scene);
            }
        }

        BlockData GetNeighbor(BlockData block, Vector2Int delta)
        {
            return new BlockData(block.Position + delta, GameMap.GetTile<Block>((block.Position + delta).ToVector3Int()));
        }

        IEnumerable<BlockData> GetNeighborsFrom(BlockData block, Tilemap tilemap)
        {
            yield return new BlockData(block.Position + new Vector2Int(1, 0), tilemap.GetTile<Block>((block.Position + new Vector2Int(1, 0)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(-1, 0), tilemap.GetTile<Block>((block.Position + new Vector2Int(-1, 0)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(0, 1), tilemap.GetTile<Block>((block.Position + new Vector2Int(0, 1)).ToVector3Int()));
            yield return new BlockData(block.Position + new Vector2Int(0, -1), tilemap.GetTile<Block>((block.Position + new Vector2Int(0, -1)).ToVector3Int()));
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
                yield return new BlockData(block.Position + new Vector2Int(-1, 0), GameMap.GetTile<Block>((block.Position + new Vector2Int(-1, 0)).ToVector3Int()));
            }
            if((block.BlockType.MergeMode & BlockMergeMode.Vertical) == BlockMergeMode.Vertical)
            {
                yield return new BlockData(block.Position + new Vector2Int(0, 1), GameMap.GetTile<Block>((block.Position + new Vector2Int(0, 1)).ToVector3Int()));
                yield return new BlockData(block.Position + new Vector2Int(0, -1), GameMap.GetTile<Block>((block.Position + new Vector2Int(0, -1)).ToVector3Int())); 
            }
        }

        BlocksCollection MergeBlocks(BlockData startBlock, HashSet<BlockData> visitedBlocks)
        {
            var mergedBlocks = new BlocksCollection();
            Queue<BlockData> blocksToVisit = new Queue<BlockData>(32);
            blocksToVisit.Enqueue(startBlock);
            while (blocksToVisit.Count > 0)
            {
                var x = blocksToVisit.Reverse().Take(100).ToArray();
                var block = blocksToVisit.Dequeue();
                if (visitedBlocks.Contains(block))
                    continue;
                visitedBlocks.Add(block);
                mergedBlocks.Set(block);
                GetNeighbors(block)
                    .Where(next => next != BlockData.Null)
                    .Where(next => !visitedBlocks.Contains(next))
                    .Where(next => next.BlockType == block.BlockType)
                    .ForEach(next => blocksToVisit.Enqueue(next));
            }
            return mergedBlocks;
        }

        IEnumerable<BlocksCollection> GetMergeBlocks(List<BlockData> blocks)
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