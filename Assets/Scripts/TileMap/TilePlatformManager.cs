using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;

namespace Project.GameMap
{
    public class TilePlatformManager : CustomLifeCycleManager<PlatformInstance>
    {
        public static IEnumerable<PlatformInstance> Platforms
            => Entities.Where(entity => entity);
    }

}
