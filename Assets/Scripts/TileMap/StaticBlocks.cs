using UnityEngine;
using System.Collections;
using Project.Blocks;
using UnityEngine.Tilemaps;

namespace Project.GameMap
{
    [RequireComponent(typeof(Tilemap), typeof(CompositeCollider2D), typeof(TilemapCollider2D))]
    [RequireComponent(typeof(TilemapRenderer), typeof(Rigidbody2D))]
    public class StaticBlocks : MonoBehaviour, IBlockInstance
    {
        public Tilemap TileMap { get; private set; }
        private void Awake()
        {
            gameObject.layer = 11;
            TileMap = GetComponent<Tilemap>();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<TilemapCollider2D>().usedByComposite = true;
            GetComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;
        }
        public Block GetContactedBlock(Vector3 point, Vector3 normal)
        {
            point = point - normal * 0.01625f;
            return TileMap.GetTile<Block>(new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0));
        }

        public T GetData<T>(Vector3 point, Vector3 normal) where T : BlockInstanceData
            => null;
    }
}
