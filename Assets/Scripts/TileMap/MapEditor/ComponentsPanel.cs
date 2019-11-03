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
    }

}