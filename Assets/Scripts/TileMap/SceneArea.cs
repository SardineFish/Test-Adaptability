using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Project.Blocks;
using System.Linq;
using UnityEngine.Tilemaps;

namespace Project.GameMap
{
    public class SceneArea
    {
        public string Name { get; set; }
        public Vector2Int SpawnPoint;
        public List<UserBlockComponent> UserComponents { get; private set; } = new List<UserBlockComponent>();
        public BlocksCollection Blocks { get; private set; }
        public BoundsInt Bound => Blocks.Bound;
        public Collider2D BoundaryCollider { get; set; }
        public TilemapRenderer BoundaryRenderer { get; set; }

        public SceneArea(BlocksCollection blocks, string name = null)
        {
            this.Blocks = blocks;
            if (name is null)
                name = System.Guid.NewGuid().ToString();
            Name = name;
            try
            {
                SpawnPoint = blocks.Where(block => block.BlockType is GameMap.Data.SceneData data && data.DataType == Data.SceneData.Type.SpawnPoint)
                    .First()
                    .Position;
            }
            catch
            {
                Debug.LogError($"Cannot found spawn point of scene {Name}.");
            }
        }

        public bool InScene(Vector2Int position)
            => Blocks.Has(position);
    }

}