using UnityEngine;
using System.Collections;

namespace Project.UI
{
    [RequireComponent(typeof(UIBase))]
    public class PlatformSpacificUI : MonoBehaviour
    {
        public GenericPlatform TargetPlatform;
        public UIBase UIComponent;
        private void Reset()
        {
            UIComponent = GetComponent<UIBase>();
        }
        private void Start()
        {
            UIComponent = GetComponent<UIBase>();
            if (TargetPlatform != Utility.GetGenericPlatform(Application.platform))
                UIComponent.HideImmediate();
        }

    }

}