using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Project.GameMap
{
    public enum BlockType
    {
        None,
        Platform,
        SolidBlock,
        SlickBlock,
    }

    [CreateAssetMenu(fileName ="Tile",menuName ="Typed Tile")]
    public class TypedTile : Tile
    {
        public BlockType BlockType;
    }

}