using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Project.GameMap.Editor
{
    public class MapEidtoUI : Singleton<MapEidtoUI>
    {
        public ComponentsPanel ComponentPanel;
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
            ComponentPanel.SetComponents(components);
        }
    }
}
