using UnityEngine;
using System.Collections;

namespace Project
{
    public struct BlockContactData
    {
        public Blocks.Block Block;
        public Vector2 Normal;
        public bool IsMainContact;
        public float ContactWeight;
    }

}