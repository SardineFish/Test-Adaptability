using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="BlockSet",menuName ="Blocks/BlockSet")]
    public class BlockSet : ScriptableObject
    {
        [SerializeField]
        [Header("Block Groups")]
        List<ScriptableObject> m_Groups;


        public IEnumerable<IBlockGroup> BlockGroups
            => m_Groups.Where(obj => obj is IBlockGroup)
                       .Select(obj => obj as IBlockGroup);

        public IEnumerable<Block> GetAllBlocks()
        {
            foreach(var group in BlockGroups)
            {
                var block = group.GetDefault();
                do
                {
                    yield return block;
                    block = group.GetNext(block);
                } while (block != group.GetDefault());
            }
        }

        public Block GetNext(Block block)
        {
            var next = BlockGroups
                .Where(group => group.HasBlock(block))
                .FirstOrDefault()
                ?.GetNext(block);

            return next is null ? block : next;
        }
    }
}