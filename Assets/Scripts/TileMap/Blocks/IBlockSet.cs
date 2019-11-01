using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Blocks
{
    public interface IBlockGroup
    {
        Block GetDefault();
        Block GetNext(Block block);
        bool HasBlock(Block block);
    }
}
