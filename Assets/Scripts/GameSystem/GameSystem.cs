using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project
{
    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance { get; private set; }
        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }
    }
}
