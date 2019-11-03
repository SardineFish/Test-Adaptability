using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Project.Blocks;

namespace Project.GameMap
{
    public class SceneArea
    {
        public List<UserBlockComponent> UserComponents { get; private set; }
        public BoundsInt Bound { get; private set; }
        
    }

}