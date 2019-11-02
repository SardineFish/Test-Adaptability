using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Blocks;

namespace Project.GameMap
{
    public class UserBlockComponent : MergedBlocks
    {
        public int Count;
        public UserBlockComponent(MergedBlocks blocks, int count)
        {
            this.Blocks = blocks.Blocks;
            this.Count = count;
        }
    }
}
