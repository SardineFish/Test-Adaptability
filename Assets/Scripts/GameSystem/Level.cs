using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

namespace Project
{
    public class Level : Singleton<Level>
    {
        public CinemachineVirtualCamera GamePlayCamera;
        public CinemachineVirtualCamera EditModeCamera;
    }
}
