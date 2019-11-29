using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.GameMap.Data
{
    [CreateAssetMenu(fileName ="DeadArea",menuName ="MapData/DeadArea")]
    public class DeadArea : EffectArea
    {
        public override void ProcessPlayerContact(GameEntity player)
        {
            (player as Player).Kill();
        }

    }
}
