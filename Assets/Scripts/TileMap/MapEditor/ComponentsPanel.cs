using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Project.GameMap.Editor
{
    public class ComponentsPanel : MonoBehaviour
    {
        List<UserComponentUIData> components;
        public List<ComponentView> ComponentViews = new List<ComponentView>();
        public GameObject ComponentViewPrefab;

        // Use this for initialization
        void Start()
        {
            extendProcess = 0;
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y * extendProcess);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetComponents(List<UserComponentUIData> components)
        {
            this.components = components;
            ComponentViews.ForEach(view => Destroy(view.gameObject));
            ComponentViews = components.Select(component =>
            {
                var obj = Instantiate(ComponentViewPrefab, transform);
                var view = obj.GetComponent<ComponentView>();
                view.Component = component;
                return view;
            }).ToList();

        }

        float extendProcess = 0;

        IEnumerator HideProcess()
        {
            var rectTransform = GetComponent<RectTransform>();
            foreach(var t in Utility.TimerNormalized(.5f))
            {
                extendProcess = Mathf.Lerp(extendProcess, 1, t);
                rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y * extendProcess);
                yield return null;
            }
            gameObject.SetActive(false);
        }
        IEnumerator ShowProcess()
        {
            var rectTransform = GetComponent<RectTransform>();
            foreach (var t in Utility.TimerNormalized(.5f))
            {
                extendProcess = Mathf.Lerp(extendProcess, 0, t);
                rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y * extendProcess);
                yield return null;
            }
        }

        public void Hide()
        {
            if (!gameObject.activeInHierarchy)
                return;
            StopAllCoroutines();
            StartCoroutine(HideProcess());
        }
        public void Show()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }
    }

}