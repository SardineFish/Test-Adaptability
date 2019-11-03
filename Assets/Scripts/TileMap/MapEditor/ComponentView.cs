using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Project.GameMap.Editor
{
    public class ComponentView : MonoBehaviour,IPointerClickHandler
    {
        public RawImage ComponentImage;
        public UserComponentUIData Component;

        public void OnPointerClick(PointerEventData eventData)
        {
            ComponentPlacement.Create(Component, PlaceMode.Click);
        }

        // Use this for initialization
        void Start()
        {
            ComponentImage.texture = Component.Texture;
            var fitter = ComponentImage.GetComponent<AspectRatioFitter>();
            fitter.aspectRatio = Component.Texture.width / (float)Component.Texture.height;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
