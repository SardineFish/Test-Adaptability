using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.GameMap.Data
{
    public abstract class EffectArea : DataBlock
    {
        public abstract void ProcessPlayerContact(GameEntity player);
    }
}
