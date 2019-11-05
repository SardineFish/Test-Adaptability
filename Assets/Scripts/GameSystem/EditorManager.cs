using UnityEngine;
using System.Collections;
using Project.GameMap.Editor;
using System.Collections.Generic;
using Project.GameMap;
using System.Linq;

namespace Project
{
    public class EditorManager : Singleton<EditorManager>
    {
        public GameObject PlacementPrefab;


        Dictionary<SceneArea, SceneEditorState> sceneEditorStates = new Dictionary<SceneArea, SceneEditorState>();

        SceneEditorState CurrentEditorState => sceneEditorStates[ScenesManager.Instance.CurrentScene];
        private void Awake()
        {
            BlocksMap.Instance.AfterMapGeneration += () =>
            {
                BlocksMap.Instance.Scenes.ForEach(scene =>
                {
                    var editorState = new SceneEditorState();
                    editorState.Scene = scene;
                    editorState.AvailableComponents = scene.UserComponents.Select(component => new UserComponentUIData(component)).ToList();
                    sceneEditorStates[scene] = editorState;
                });
            };
        }
        public static ComponentPlacement CreatePlacement(UserComponentUIData data)
        {
            if (data.Component.Count <= 0)
                return null;
            data.Component.Count -= 1;
            var obj = Instantiate(Instance.PlacementPrefab);
            var placement = obj.GetComponent<ComponentPlacement>();
            placement.SetComponent(data);
            placement.transform.parent = GameMap.BlocksMap.Instance.PlacementLayer.transform;
            Instance.CurrentEditorState.PlacedComponents.Add(placement);
            return placement;
        }
        public static void RemovePlacement(ComponentPlacement placement)
        {
            Instance.CurrentEditorState.PlacedComponents.Remove(placement);
            placement.Component.Component.Count++;
            Destroy(placement.gameObject);
        }

        public static void StartEditMode()
        {
            MapEidtoUI.Instance.gameObject.SetActive(true);
            BlocksMap.Instance.SwitchToEditorMap();
            MapEidtoUI.Instance.SetComponents(Instance.CurrentEditorState.AvailableComponents);
            foreach (var placed in Instance.CurrentEditorState.PlacedComponents)
            {
                placed.gameObject.SetActive(true);
            }
        }

        public static void StopEdit()
        {
            MapEidtoUI.Instance.gameObject.SetActive(false);

            for(var i=0;i<Instance.CurrentEditorState.PlacedComponents.Count;i++)
            {
                var placement = Instance.CurrentEditorState.PlacedComponents[i];
                if (!placement.Placed)
                {
                    RemovePlacement(placement);
                    i--;
                    continue;
                }
                placement.gameObject.SetActive(false);
            }

            GameMap.BlocksMap.Instance.SwitchToPlayMap();
        }


        class SceneEditorState
        {
            public SceneArea Scene;
            public List<ComponentPlacement> PlacedComponents = new List<ComponentPlacement>();
            public List<UserComponentUIData> AvailableComponents;
        }
    }

}