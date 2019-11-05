using UnityEngine;
using System.Collections;
using Project.GameMap;
using System.Collections.Generic;
using Project.GameMap.Editor;
using System.Linq;

namespace Project
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        public SceneArea CurrentScene { get; private set; } = null;
        public List<SceneArea> Scenes { get; private set; }

        Dictionary<SceneArea, SceneEditorState> sceneEditorStates;

        private void Awake()
        {
            GameMap.BlocksMap.Instance.AfterMapGeneration += () =>
            {
                Scenes = BlocksMap.Instance.Scenes;
            };
        }
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
                CurrentScene = scene;
            }
        }

        public static void StartEditMode()
        {
            MapEidtoUI.Instance.gameObject.SetActive(true);
            BlocksMap.Instance.StartEditMode();
            MapEidtoUI.Instance.SetComponents(Instance.CurrentScene.ComponentsUIData);
        }

        public static SceneArea GetSceneAt(Vector2Int position)
            => Instance?.Scenes
                .Where(scene => scene.Blocks.Has(position))
                .FirstOrDefault();

        class SceneEditorState
        {
            public SceneArea Scene;
            public List<ComponentPlacement> PlacedComponents;
            public List<UserComponentUIData> AvailableComponents;
        }
    }
}

