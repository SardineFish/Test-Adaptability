using UnityEngine;
using System.Collections;
using Project.Blocks;

namespace Project.GameMap
{
    public interface IBlockInstance
    {
        Block GetContactedBlock(Vector3 point, Vector3 normal);
    }
}
