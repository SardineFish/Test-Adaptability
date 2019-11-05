using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Blocks;

namespace Project.GameMap
{
    public class UserBlockComponent : BlocksCollection
    {
        public int Count;
        public Block BlockType => blocksList[0].BlockType;
        public GameMap.SceneArea Scene { get; private set; }
        public UserBlockComponent(BlocksCollection blocks, int count, SceneArea scene) : base(blocks)
        {
            this.Count = count;
            this.Scene = scene;
        }
    }
}
