using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;

namespace Project.GameMap.Editor
{
    public enum PlaceMode
    {
        Drag,
        Click,
        None,
    }

    public class ComponentPlacement : MonoBehaviour
    {
        public RectTransform UI;
        public UserComponentUIData Component;
        public BlockInstance BlockInstance;
        public bool Dragging { get; private set; }
        
        [DisplayInInspector]
        public bool CanPlace { get; private set; }
        [DisplayInInspector]
        public bool Placed { get; private set; }
        
        event Action<Collider2D> onTrigger;
        new Rigidbody2D rigidbody;
        private void Awake()
        {
            onTrigger += (collider) =>
            {
                if (collider.attachedRigidbody.GetComponent<IBlockInstance>() != null)
                    CanPlace = false;
            };
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }
        bool CheckBlockOccupation()
        {
            var offset = Component.Component.Bound.size.ToVector2() / 2;
            foreach(var block in Component.Component)
            {
                var pos = Component.Component.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                if (BlocksMap.Instance.BaseLayer.GetTile(pos) != null)
                    return true;
                if (BlocksMap.Instance.PlacementLayer.GetTile(pos) != null)
                    return true;
            }
            return false;
        }
        public void Replace()
        {
            var offset = Component.Component.Bound.size.ToVector2() / 2;
            foreach (var block in Component.Component)
            {
                var pos = Component.Component.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                BlocksMap.Instance.PlacementLayer.SetTile(pos, Component.Component.BlockType);
            }
            Placed = true;
        }
        void Place()
        {
            if (Placed)
                return;

            UI.gameObject.SetActive(false);

            var offset = Component.Component.Bound.size.ToVector2() / 2;
            foreach (var block in Component.Component)
            {
                var pos = Component.Component.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                BlocksMap.Instance.PlacementLayer.SetTile(pos, Component.Component.BlockType);
            }
            Placed = true;
        }
        public void Pick()
        {
            if (!Placed)
                return;
            Placed = false;
            Dragging = false;
            UI.gameObject.SetActive(true);
            var offset = Component.Component.Bound.size.ToVector2() / 2;
            foreach (var block in Component.Component)
            {
                var pos = Component.Component.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                BlocksMap.Instance.PlacementLayer.SetTile(pos, null);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTrigger?.Invoke(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            onTrigger?.Invoke(collision);
        }

        Coroutine dragCoroutine;
        private void OnMouseDown()
        {
            if (!Placed)
                StartDrag(PlaceMode.None);
        }
        private void OnMouseUpAsButton()
        {
            if (Placed)
                Pick();
        }

        IEnumerator DragProcess()
        {
            var startPos = UnityEngine.Input.mousePosition;
            foreach(var t in Utility.Timer(1))
            {
                if((UnityEngine.Input.mousePosition - startPos).magnitude > 30)
                {
                    yield return DragMove();
                    yield break;
                }
                else if (UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                {
                    yield return ClickMove();
                    yield break;
                }
                yield return null;
            }
            yield return DragMove();
        }

        IEnumerator DragMove()
        {
            while(true)
            {
                yield return null;

                float2 position = Level.Instance.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                var snapOffset = math.frac(Component.Component.Bound.center.ToVector2());
                position -= snapOffset;
                position = math.round(position);
                position += snapOffset;
                transform.position = math.float3(position.x, position.y, 0);
                CanPlace = !CheckBlockOccupation();

                if (CanPlace && UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Dragging = false;
                    yield break;
                }
            }
        }

        IEnumerator ClickMove()
        {
            while (true)
            {
                yield return null;

                float2 position = Level.Instance.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                var snapOffset = math.frac(Component.Component.Bound.center.ToVector2());
                position -= snapOffset;
                position = math.round(position);
                position += snapOffset;
                transform.position = math.float3(position.x, position.y, 0);
                CanPlace = !CheckBlockOccupation();

                if (CanPlace && UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Dragging = false;
                    yield break;
                }
            }
        }

        public void StartDrag(PlaceMode placeMode)
        {
            if (Dragging)
                return;
            Dragging = true;
            switch(placeMode)
            {
                case PlaceMode.None:
                    dragCoroutine = StartCoroutine(DragProcess());
                    break;
                case PlaceMode.Click:
                    dragCoroutine = StartCoroutine(ClickMove());
                    break;
                case PlaceMode.Drag:
                    dragCoroutine = StartCoroutine(ClickMove());
                    break;
            }

        }
        

        public void Remove()
        {
            EditorManager.RemovePlacement(this);
        }
        public void Done()
        {
            Place();
        }

        public void SetComponent(UserComponentUIData data)
        {
            Component = data;
            var instance = BlockInstance.CreateInstance(new BlockInstanceOptions()
            {
                BlockType = data.Component.BlockType,
                Blocks = data.Component,
                GenerateRenderer = true,
                GenerateCollider = true,
                IsTrigger = true,
                IsStatic = true,
                GameObject = gameObject
            });
            var rigidbody = instance.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
            instance.transform.parent = transform;
            instance.transform.localPosition = Vector3.zero;
            BlockInstance = instance;
            UI.sizeDelta = data.Component.Bound.size.ToVector2();

        }
    }

}