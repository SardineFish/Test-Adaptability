using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Project.GameMap.Editor
{
    public class ComponentView : MonoBehaviour, IPointerClickHandler
    {
        public RawImage ComponentImage;
        public UserComponentUIData Component;
        public Text Text;
        public Image TextWrapper;
        public Color ActiveNumberColor;
        public Color DisableNumberColor;

        public void OnPointerClick(PointerEventData eventData)
        {
            EditorManager.CreatePlacement(Component)?.StartDrag(PlaceMode.Click);
            // ComponentPlacement.Create(Component, PlaceMode.Click);
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
            Text.text = Component.Component.Count.ToString();
            if(Component.Component.Count<=0)
            {
                TextWrapper.color = DisableNumberColor;
            }
            else
            {
                TextWrapper.color = ActiveNumberColor;
            }
        }
    }
}
