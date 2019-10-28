using UnityEngine;
using System.Collections;
using Project.Blocks;
using UnityEngine.Tilemaps;

namespace Project.GameMap
{
    [RequireComponent(typeof(Tilemap))]
    public class BlocksTilemap : MonoBehaviour, IBlockInstance
    {
        Tilemap tilemap;
        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }
        public Block GetContactedBlock(Vector3 point, Vector3 normal)
        {
            point = point - normal * 0.01625f;
            return tilemap.GetTile<Block>(new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0));
        }
    }
}
