using UnityEngine;
using System.Collections;
using Project.GameMap;
using System.Collections.Generic;
using Cinemachine;

namespace Project
{
    public class CameraManager : Singleton<CameraManager>
    {
        public float CameraZ = -10;
        public CinemachineBrain CinemachineBrain;
        public Camera MainCamera;
        public GameObject GamePlayCameraPrefab;
        public GameObject EditorCameraPrefab;
        public CinemachineVirtualCamera GamePlayCamera;
        public CinemachineVirtualCamera EditorCamera;

        [DisplayInInspector]
        public Vector2 ScreenWorldSize
        {
            get
            {
                var leftBottom = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
                var rightTop = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
                return (rightTop - leftBottom).ToVector2();
            }
        }

        Dictionary<SceneArea, CinemachineVirtualCamera> SceneCameras = new Dictionary<SceneArea, CinemachineVirtualCamera>();
        Dictionary<SceneArea, CinemachineVirtualCamera> SceneEditorCameras = new Dictionary<SceneArea, CinemachineVirtualCamera>();
        private void Awake()
        {
            BlocksMap.Instance.AfterMapGeneration += AfterMapGeneration;
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            CinemachineBrain = MainCamera.gameObject.GetOrAddComponent<CinemachineBrain>();

            if (GamePlayCamera)
                Destroy(GamePlayCamera.gameObject);
            if (EditorCamera)
                Destroy(EditorCamera.gameObject);
        }

        private void AfterMapGeneration()
        {
            foreach(var scene in BlocksMap.Instance.Scenes)
            {
                SceneCameras[scene] = CreateGamePlayCamera(scene);
                SceneEditorCameras[scene] = CreateEditorCamera(scene);
            }
            var startupScene = BlocksMap.Instance.GetSceneAt(Level.Instance.SpawnPoint.transform.position.ToVector2Int());
            GamePlayCamera = SceneCameras[startupScene];
            GamePlayCamera.gameObject.SetActive(true);
            EditorCamera = SceneEditorCameras[startupScene];
        }

        CinemachineVirtualCamera CreateGamePlayCamera(SceneArea scene)
        {
            var obj = Instantiate(GamePlayCameraPrefab);
            obj.name = $"Camera-{scene.BoundaryCollider.name}";
            obj.transform.parent = transform;
            obj.transform.position = obj.transform.position.Set(z: CameraZ);
            obj.SetActive(false);
            var camera = obj.GetComponent<CinemachineVirtualCamera>();
            var confiner = obj.GetComponent<CinemachineConfiner>();
            confiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
            confiner.m_BoundingShape2D = scene.BoundaryCollider;
            return camera;
        }

        CinemachineVirtualCamera CreateEditorCamera(SceneArea scene)
        {
            var obj = Instantiate(EditorCameraPrefab);
            obj.name = $"EditorCamera-{scene.BoundaryCollider.name}";
            obj.transform.parent = transform;
            obj.transform.position = obj.transform.position.Set(z: CameraZ);
            obj.SetActive(false);
            var camera = obj.GetComponent<CinemachineVirtualCamera>();
            var confiner = obj.GetComponent<CinemachineConfiner>();
            confiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
            confiner.m_BoundingShape2D = scene.BoundaryCollider;
            return camera;
        }

        void CreateEditorCamera()
        {
            var obj = Instantiate(EditorCameraPrefab);
            obj.transform.parent = transform;
            obj.transform.position = transform.position.Set(z: CameraZ);
            var cinemachine = obj.GetComponent<CinemachineVirtualCamera>();
            var confiner = obj.GetComponent<CinemachineConfiner>();
            EditorCamera = cinemachine;
            obj.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {

        }

        private void Update()
        {
            if(Level.Instance.GameState == GameState.Playing)
            {
                var player = Level.Instance.ActivePlayer;
                var scene = GameMap.BlocksMap.Instance.GetSceneAt(player.transform.position.ToVector2Int());
                if (scene == null)
                {
                    GamePlayCamera.GetComponent<CinemachineConfiner>().m_Damping = 0.1f;
                }
                if(scene!=null && SceneCameras[scene] != GamePlayCamera)
                {
                    GamePlayCamera.GetComponent<CinemachineConfiner>().m_Damping = 0;
                    GamePlayCamera.gameObject.SetActive(false);
                    GamePlayCamera = SceneCameras[scene];
                    GamePlayCamera.gameObject.SetActive(true);
                }
                if(!GamePlayCamera.Follow)
                    GamePlayCamera.Follow = player.transform;
                if (EditorCamera.gameObject.activeInHierarchy)
                {
                    EditorCamera.gameObject.SetActive(false);
                    GamePlayCamera.gameObject.SetActive(true);
                }
            }
            else if (Level.Instance.GameState == GameState.EditMode)
            {
                EditorCamera = SceneEditorCameras[ScenesManager.Instance.CurrentScene];
                EditorCamera.gameObject.SetActive(true);
                //EditorCamera.gameObject.SetActive(true);
                GamePlayCamera.gameObject.SetActive(false);
                //EditorCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = ScenesManager.Instance.CurrentScene.BoundaryCollider;
            }
        }
    }

}