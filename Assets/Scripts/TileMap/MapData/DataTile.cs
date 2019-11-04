using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Project.GameMap.Data
{
    [CreateAssetMenu(fileName ="MapData", menuName ="MapData/Data")]
    public class DataBlock : Blocks.Block
    {
        void Awake()
        {
            sprite = Resources.Load<Sprite>("Texture/white-16");    
        }
    }
}
