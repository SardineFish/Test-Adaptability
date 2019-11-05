using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Linq;

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
        public UserComponentUIData ComponentData;
        public BlockInstance BlockInstance;

        int rotateStep = 0;
        public float Angle => rotateStep * 90;
        public bool Dragging { get; private set; }
        
        [DisplayInInspector]
        public bool CanPlace { get; private set; }
        [DisplayInInspector]
        public bool Placed { get; private set; }
        
        event Action<Collider2D> onTrigger;
        new Rigidbody2D rigidbody;
        BlockInstance blockInstance;
        Blocks.BlocksCollection rotatedBlocks;
        private void Awake()
        {
            onTrigger += (collider) =>
            {
                if (collider.attachedRigidbody.GetComponent<IBlockInstance>() != null)
                    CanPlace = false;
            };
            Input.InputManager.Input.EditorMode.Rotate.performed += Rotate_performed;
        }

        private void OnDestroy()
        {
            Input.InputManager.Input.EditorMode.Rotate.performed -= Rotate_performed;
        }

        private void Rotate_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Rotate();
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
            var offset = rotatedBlocks.Bound.size.ToVector2() / 2;
            foreach(var block in rotatedBlocks)
            {
                var pos = rotatedBlocks.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                if (BlocksMap.Instance.BaseLayer.GetTile(pos) != null)
                    return true;
                if (BlocksMap.Instance.PlacementLayer.GetTile(pos) != null)
                    return true;
            }
            return false;
        }
        public void Replace()
        {
            var offset = rotatedBlocks.Bound.size.ToVector2() / 2;
            foreach (var block in rotatedBlocks)
            {
                var pos = rotatedBlocks.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                BlocksMap.Instance.PlacementLayer.SetTile(pos, rotatedBlocks.First().BlockType);
            }
            Placed = true;
        }
        void Place()
        {
            if (Placed)
                return;

            UI.gameObject.SetActive(false);

            Replace();
        }
        public void Pick()
        {
            if (!Placed)
                return;
            Placed = false;
            Dragging = false;
            UI.gameObject.SetActive(true);
            var offset = rotatedBlocks.Bound.size.ToVector2() / 2;
            foreach (var block in rotatedBlocks)
            {
                var pos = rotatedBlocks.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
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

                var position = Level.Instance.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                MoveTo(position);

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

                var position = Level.Instance.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                MoveTo(position);

                CanPlace = !CheckBlockOccupation();
                if (CanPlace && UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Dragging = false;
                    yield break;
                }
            }
        }

        public void MoveTo(Vector2 position)
        {
            var snapOffset = (Vector2)math.frac(rotatedBlocks.Bound.center.ToVector2());
            position -= snapOffset;
            position = math.round(position);
            position += snapOffset;
            transform.position = math.float3(position.x, position.y, 0);
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
        public void Rotate()
        {
            rotateStep++;
            rotateStep %= 4;
            GenerateBlockInstance();
        }

        void GenerateBlockInstance()
        {
            var position = transform.position.ToVector2();
            rotatedBlocks = new Blocks.BlocksCollection(ComponentData.Component.Rotate(rotateStep));
            if(!blockInstance)
            {
                blockInstance = BlockInstance.CreateInstance(new BlockInstanceOptions()
                {
                    BlockType = rotatedBlocks.First().BlockType,
                    Blocks = rotatedBlocks,
                    GenerateRenderer = true,
                    GenerateCollider = true,
                    IsTrigger = true,
                    IsStatic = true,
                    GameObject = gameObject
                });
                var rigidbody = blockInstance.GetComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
                blockInstance.transform.parent = transform;
                blockInstance.transform.localPosition = Vector3.zero;
                BlockInstance = blockInstance;
            }
            else
            {
                blockInstance.UpdateInstance(new BlockInstanceOptions()
                {
                    BlockType = rotatedBlocks.First().BlockType,
                    Blocks = rotatedBlocks,
                    GenerateRenderer = true,
                    GenerateCollider = true,
                    IsTrigger = true,
                    IsStatic = true,
                    GameObject = gameObject
                });
            }
            UI.sizeDelta = rotatedBlocks.Bound.size.ToVector2();
            MoveTo(position);
        }

        public void SetComponent(UserComponentUIData data)
        {
            ComponentData = data;
            GenerateBlockInstance();
        }
    }

}