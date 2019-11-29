using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

namespace Project
{
    public enum GameState
    {
        Pause,
        EditMode,
        Playing,
        PlayerDead,
    }

    [RequireComponent(typeof(ScenesManager))]
    public class Level : Singleton<Level>
    {
        [DisplayInInspector]
        public GameState GameState { get; private set; }
        public GameObject PlayerPrefab;
        public Transform SpawnPoint;
        public Player ActivePlayer;
        public bool TestMode = false;

        public UnityEngine.Events.UnityEvent OnPlayerDead;
        public UnityEvent OnLevelRestart;

        private void Awake()
        {
            ActivePlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);

            GameMap.BlocksMap.Instance.AfterMapGeneration += () =>
            {
                if (TestMode)
                {
                    GameMap.BlocksMap.Instance.PlaceAllUserBlocks();
                }
                this.WaitForSecond(RestartLevel, .1f);
            };
            Input.InputManager.Input.EditorMode.ToggleEditMode.performed+=(ctx)=>
            {
                if (GameState == GameState.EditMode)
                    StartGamePlay();
                else if (GameState == GameState.Playing)
                    StartLevelEdit();
            };
        }

        private void Start()
        {
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
            if(GameState == GameState.PlayerDead)
            {
                if(Input.InputManager.Input.Global.Accept.UsePressed())
                {
                    OnLevelRestart?.Invoke();
                    //RestartScene();
                }
            }
        }

        private void Level_OnPlayerDead()
        {
            Debug.Log("Player Dead.");
            OnPlayerDead?.Invoke();
            GameState = GameState.PlayerDead;
        }

        public void StartGamePlay()
        {
            if (GameState == GameState.Playing)
                return;
            GameState = GameState.Playing;
            EditorManager.StopEdit();


            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();
            ActivePlayer.OnPlayerDead += Level_OnPlayerDead;


            ActivePlayer.transform.position = ScenesManager.Instance.CurrentScene.SpawnPoint.ToVector3();
        }

        public void StartLevelEdit()
        {
            if (GameState == GameState.EditMode)
                return;
            GameState = GameState.EditMode;
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            EditorManager.StartEditMode();
        }

        public void RestartScene()
        {
            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);

            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();
            ActivePlayer.OnPlayerDead += Level_OnPlayerDead;
            ActivePlayer.transform.position = ScenesManager.Instance.CurrentScene.SpawnPoint.ToVector3();
            GameState = GameState.Playing;
        }

        public void RestartLevel()
        {
            var startupScene = ScenesManager.GetSceneAt(SpawnPoint.position.ToVector2Int());
            var spawnPos = startupScene.SpawnPoint;


            GameState = GameState.Playing;
            GameMap.Editor.MapEidtoUI.Instance.gameObject.SetActive(false);
            GameMap.BlocksMap.Instance.SwitchToPlayMap();

            Physics2D.Simulate(Time.fixedDeltaTime);

            if (ActivePlayer)
                Destroy(ActivePlayer.gameObject);
            ActivePlayer = Instantiate(PlayerPrefab).GetComponent<Player>();
            ActivePlayer.OnPlayerDead += Level_OnPlayerDead;


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
