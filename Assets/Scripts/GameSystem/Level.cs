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
        public Camera MainCamera;
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
            MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Start()
        {
            ActivePlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            StartLevelEdit();
        }

        private void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.F1))
            {
                StartLevelEdit();
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.F2))
            {
                StartGamePlay();
            }
        }

        private void Level_OnPlayerDead()
        {
            ActivePlayer.transform.position = SpawnPoint.position;
        }

        public void StartGamePlay()
        {
            GameMap.Editor.MapEidtoUI.Instance.gameObject.SetActive(false);
            GameMap.BlocksMap.Instance.StartPlayerMode();


            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();

            ActivePlayer.transform.position = SpawnPoint.position;
            GamePlayCamera.Follow = ActivePlayer.transform;
        }

        public void StartLevelEdit()
        {
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            GameMap.Editor.MapEidtoUI.Instance.gameObject.SetActive(true);
            GameMap.BlocksMap.Instance.StartEditMode();
            GameMap.Editor.MapEidtoUI.Instance.SetComponents(GameMap.BlocksMap.Instance.GetUserComponents());
        }
    }
}
