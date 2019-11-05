using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

namespace Project
{
    public enum GameState
    {
        Pause,
        EditMode,
        Playing,
    }

    [RequireComponent(typeof(ScenesManager))]
    public class Level : Singleton<Level>
    {
        [DisplayInInspector]
        public GameState GameState { get; private set; }
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
            RestartLevel();
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
            GameState = GameState.Playing;
            EditorManager.StopEdit();


            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();


            ActivePlayer.transform.position = ScenesManager.Instance.CurrentScene.SpawnPoint.ToVector3();
        }

        public void StartLevelEdit()
        {
            GameState = GameState.EditMode;
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            EditorManager.StartEditMode();
        }

        public void RestartLevel()
        {
            var startupScene = ScenesManager.GetSceneAt(SpawnPoint.position.ToVector2Int());
            var spawnPos = startupScene.SpawnPoint;


            GameState = GameState.Playing;
            GameMap.Editor.MapEidtoUI.Instance.gameObject.SetActive(false);
            GameMap.BlocksMap.Instance.SwitchToPlayMap();


            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();


            ActivePlayer.transform.position = spawnPos.ToVector3();
        }

        private void OnDrawGizmos()
        {
            if(SpawnPoint)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(SpawnPoint.position, 1);
            }
        }
    }
}
