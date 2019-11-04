using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Project.Blocks;

namespace Project.GameMap
{
    public class SceneArea
    {
        public List<UserBlockComponent> UserComponents { get; private set; }
        public BlocksCollection Blocks { get; private set; }
        public BoundsInt Bound => Blocks.Bound;
        public List<Editor.ComponentPlacement> PlacedComponents { get; private set; }
        public Collider2D BoundaryCollider { get; set; }

        public SceneArea(BlocksCollection blocks)
        {
            this.Blocks = blocks;
        }

        public bool InScene(Vector2Int position)
            => Blocks.Has(position);
    }

}