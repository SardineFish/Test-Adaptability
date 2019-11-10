using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Project.GameMap.Editor
{
    public class ComponentView : Selectable, IPointerClickHandler, ISubmitHandler
    {
        public RawImage ComponentImage;
        public UserComponentUIData Component;
        public Text Text;
        public Image TextWrapper;
        public Color ActiveNumberColor;
        public Color DisableNumberColor;

        bool selected = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            EditorManager.CreatePlacement(Component)?.StartDrag(PlaceMode.Click);
        }
        public void OnSubmit(BaseEventData eventData)
        {
            Input.InputManager.Input.EditorMode.Place.UsePressed();
            EditorManager.CreatePlacement(Component)?.StartDrag(PlaceMode.Click);
        }

        protected override void Start()
        {
            base.Start();
            ComponentImage.texture = Component.Texture;
            var fitter = ComponentImage.GetComponent<AspectRatioFitter>();
            fitter.aspectRatio = Component.Texture.width / (float)Component.Texture.height;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            selected = true;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            selected = false;
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
