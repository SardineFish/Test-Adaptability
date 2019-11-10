using UnityEngine;
using System.Collections;
using Project.GameMap.Editor;
using System.Collections.Generic;
using Project.GameMap;
using System.Linq;
using Project.Input;

namespace Project
{
    public class EditorManager : Singleton<EditorManager>
    {
        public GameObject PlacementPrefab;


        Dictionary<SceneArea, SceneEditorState> sceneEditorStates = new Dictionary<SceneArea, SceneEditorState>();

        SceneEditorState CurrentEditorState => sceneEditorStates[ScenesManager.Instance.CurrentScene];
        private ComponentPlacement currentPlacement;
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
            if (Instance.currentPlacement)
                return null;
            if (data.Component.Count <= 0)
                return null;
            data.Component.Count -= 1;
            var obj = Instantiate(Instance.PlacementPrefab);
            var placement = obj.GetComponent<ComponentPlacement>();
            placement.SetComponent(data);
            placement.transform.parent = GameMap.BlocksMap.Instance.PlacementLayer.transform;
            Instance.CurrentEditorState.PlacedComponents.Add(placement);
            placement.transform.position = CameraManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            MapEidtoUI.Instance.ComponentPanel.Hide();
            Instance.ChangeState(Instance.ComponentPlacement(placement));
            return placement;
        }
        public static void RemovePlacement(ComponentPlacement placement)
        {
            if (placement == Instance.currentPlacement)
                Instance.currentPlacement = null;
            Instance.CurrentEditorState.PlacedComponents.Remove(placement);
            placement.ComponentData.Component.Count++;
            Destroy(placement.gameObject);
            if (InputManager.CurrentInputScheme == InputSchemes.GamePad)
                Instance.ChangeState(Instance.SceneView());
            else
                Instance.ChangeState(Instance.ComponentSelect());
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
            var firstSelect = MapEidtoUI.Instance.ComponentPanel.GetComponentsInChildren<ComponentView>().FirstOrDefault();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelect.gameObject);
            if (InputManager.CurrentInputScheme == InputSchemes.GamePad)
                Instance.ChangeState(Instance.SceneView());
            else
                Instance.ChangeState(Instance.ComponentSelect());
        }

        public static void StopEdit()
        {
            Instance.StopAllCoroutines();
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

        public static bool TryPickUpPlacement(ComponentPlacement placement)
        {
            if (Instance.currentPlacement)
                return false;
            Instance.ChangeState(Instance.ComponentPlacement(placement));
            return true;
        }
        void ChangeState(IEnumerator nextState)
        {
            StopAllCoroutines();
            StartCoroutine(nextState);
        }

        IEnumerator SceneView(ComponentPlacement placement = null)
        {
            currentPlacement = null;
            Debug.Log("View");
            MapEidtoUI.Instance.ComponentPanel.Hide();
            if (!placement)
            {
                var screenCenter = CameraManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0)).ToVector2();
                placement = CurrentEditorState.PlacedComponents.MinOf(component => (component.transform.position.ToVector2() - screenCenter).magnitude);

            }
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(placement?.gameObject);


            while (true)
            {
                if (InputManager.CurrentInputScheme == InputSchemes.GamePad && InputManager.Input.EditorMode.ShowComponents.IsPressed())
                {
                    ChangeState(ComponentSelect());
                }
                else if (InputManager.CurrentInputScheme == InputSchemes.Keyboard && InputManager.Input.EditorMode.ToggleComponents.UsePressed())
                    ChangeState(ComponentSelect());

                yield return null;
            }
        }

        IEnumerator ComponentSelect()
        {
            currentPlacement = null;
            Debug.Log("Component");
            MapEidtoUI.Instance.ComponentPanel.Show();
            var componentUI = MapEidtoUI.Instance.ComponentPanel.ComponentViews.FirstOrDefault();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(componentUI?.gameObject);
            while(true)
            {
                if (Input.InputManager.CurrentInputScheme == InputSchemes.GamePad && !Input.InputManager.Input.EditorMode.ShowComponents.IsPressed())
                {
                    ChangeState(SceneView());
                }
                else if (InputManager.CurrentInputScheme == InputSchemes.Keyboard && InputManager.Input.EditorMode.ToggleComponents.UsePressed())
                    ChangeState(SceneView());
                yield return null;
            }
        }

        IEnumerator ComponentPlacement(ComponentPlacement placement)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            currentPlacement = placement;
            Debug.Log("Placement");
            MapEidtoUI.Instance.ComponentPanel.Hide();
            do
                yield return null;
            while (!placement.Placed);
            if (InputManager.CurrentInputScheme == InputSchemes.GamePad)
                ChangeState(SceneView(placement));
            else
                ChangeState(ComponentSelect());
        }


        class SceneEditorState
        {
            public SceneArea Scene;
            public List<ComponentPlacement> PlacedComponents = new List<ComponentPlacement>();
            public List<UserComponentUIData> AvailableComponents;
        }
    }

}