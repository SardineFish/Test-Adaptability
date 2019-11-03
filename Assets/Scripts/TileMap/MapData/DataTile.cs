using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Project.GameMap.Data
{
    [CreateAssetMenu(fileName ="MapData", menuName ="MapData/Data")]
    public class DataTile : Tile
    {
        void Awake()
        {
            sprite = Resources.Load<Sprite>("Texture/white-16");    
        }
    }
}
