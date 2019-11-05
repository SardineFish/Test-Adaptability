using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Project.GameMap.Data
{
    [CreateAssetMenu(fileName ="Scene", menuName ="MapData/Scene")]
    public class SceneData : DataBlock
    {
        public enum Type
        {
            SceneArea,
            SpawnPoint,
        }

        public Type DataType = Type.SceneArea;
    }
}
