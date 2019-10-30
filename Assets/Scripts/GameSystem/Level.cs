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
        public GameObject PlayerPrefab;
        public Transform SpawnPoint;
        public Player ActivePlayer;
        public event Action OnPlayerDead;

        private void Awake()
        {
            OnPlayerDead += () =>
            {
                Debug.Log("Player Dead.");
            };
        }

        private void Start()
        {
            if(ActivePlayer)
                ActivePlayer.OnPlayerDead += Level_OnPlayerDead;
        }

        private void Level_OnPlayerDead()
        {
            ActivePlayer.transform.position = SpawnPoint.position;
        }

        public void Restart()
        {
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();

            ActivePlayer.transform.position = SpawnPoint.position;

        }
    }
}
