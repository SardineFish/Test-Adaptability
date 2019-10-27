using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Project.Blocks
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical,
    }
    [CreateAssetMenu(fileName ="MotionBlock",menuName ="Blocks/MotionBlock")]
    public class MotionBlock : Block
    {
        MoveDirection Direction;
    }

}