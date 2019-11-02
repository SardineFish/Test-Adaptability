using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="BlocksGroup",menuName ="Blocks/Group")]
    public class BlockGroup : ScriptableObject, IBlockGroup
    {
        public List<Block> Blocks = new List<Block>();

        public Block GetDefault()
        {
            if (Blocks.Count <= 0)
                return null;
            return Blocks[0];
        }

        public Block GetNext(Block block)
        {
            var idx = Blocks.IndexOf(block);
            if (idx >= 0)
                idx = (idx + 1) % Blocks.Count;
            return Blocks[idx];
        }

        public bool HasBlock(Block block)
        {
            return Blocks.Contains(block);
        }
    }

}