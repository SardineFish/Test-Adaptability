using UnityEngine;
using System.Collections;
using Project.GameMap;

namespace Project
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        public SceneArea CurrentScene { get; private set; } = null;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Level.Instance.GameState == GameState.Playing)
            {
                var player = Level.Instance.ActivePlayer;
                var scene = GameMap.BlocksMap.Instance.GetSceneAt(player.transform.position.ToVector2Int());
                if(scene != CurrentScene)
                {

                }
            }
        }
    }
}

