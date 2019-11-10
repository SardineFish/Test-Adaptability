using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Linq;
using Project.Blocks;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Project.GameMap.Editor
{
    public enum PlaceMode
    {
        Drag,
        Click,
        None,
    }

    public class ComponentPlacement : Selectable
    {
        public RectTransform UI;
        public UserComponentUIData ComponentData;
        public BlockInstance BlockInstance;
        public Material FXMaterial;
        [ColorUsage(true, true)]
        public Color PlaceColor;
        [ColorUsage(true, true)]
        public Color SelectColor;
        [ColorUsage(true, true)]
        public Color ErrorColor;
        public float GamepadMoveSpeed = 1;

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
        SpriteRenderer[] renderers;
        Material defaultMat;
        protected override void Awake()
        {
            base.Awake();
            defaultMat = new Material(Shader.Find("Sprites/Default"));
            onTrigger += (collider) =>
            {
                if (collider.attachedRigidbody.GetComponent<IBlockInstance>() != null)
                    CanPlace = false;
            };
            Input.InputManager.Input.EditorMode.Rotate.performed += Rotate_performed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Input.InputManager.Input.EditorMode.Rotate.performed -= Rotate_performed;
        }
        private void Rotate_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Rotate();
        }

        private void Update()
        {
            if (!Dragging && Input.InputManager.CurrentInputScheme == Input.InputSchemes.GamePad)
                StartDrag(PlaceMode.Click);

        }
        private void FixedUpdate()
        {
            if(EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if(Placed && Input.InputManager.CurrentInputScheme == Input.InputSchemes.GamePad && Input.InputManager.Input.EditorMode.Place.UsePressed())
                {
                    Pick();
                    if (Input.InputManager.CurrentInputScheme == Input.InputSchemes.GamePad)
                        StartDrag(PlaceMode.Click);
                }
            }

            if (!Placed && Input.InputManager.Input.EditorMode.Remove.UsePressed())
            {
                Remove();
            }
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            if (Placed)
                SetFx(FXMaterial, SelectColor);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (Placed)
                SetFx(defaultMat, Color.white);
        }
        IEnumerable<BlockData> BlocksInWorldSpace()
        {
            var offset = rotatedBlocks.Bound.size.ToVector2() / 2;
            foreach (var block in rotatedBlocks)
            {
                var pos = block.Position.ToVector3Int() - rotatedBlocks.Bound.min + (transform.position.ToVector2() - offset).ToVector3Int();
                yield return new BlockData(pos.ToVector2Int(), block.BlockType);
            }
        }
        bool CheckBlockOccupation()
        {
            foreach(var block in BlocksInWorldSpace())
            {
                if (BlocksMap.Instance.BaseLayer.GetTile(block.Position.ToVector3Int()) != null)
                    return true;
                if (BlocksMap.Instance.PlacementLayer.GetTile(block.Position.ToVector3Int()) != null)
                    return true;
                if (ScenesManager.GetSceneAt(block.Position) != ScenesManager.Instance.CurrentScene)
                    return true;
            }
            return false;
        }
        public void Replace()
        {
            foreach(var block in BlocksInWorldSpace())
            {
                BlocksMap.Instance.PlacementLayer.SetTile(block.Position.ToVector3Int(), block.BlockType);
            }
            Placed = true;
        }
        void Place()
        {
            if (Placed)
                return;

            CanPlace = !CheckBlockOccupation();
            if (!CanPlace)
                return;
            if (dragCoroutine != null)
            {
                StopCoroutine(dragCoroutine);
                dragCoroutine = null;
            }

            UI.gameObject.SetActive(false);
            SetFx(defaultMat, Color.white);

            Replace();
        }
        public void Pick()
        {
            if (!Placed)
                return;
            if (!EditorManager.TryPickUpPlacement(this))
                return;
            Placed = false;
            Dragging = false;
            UI.gameObject.SetActive(true);

            foreach(var block in BlocksInWorldSpace())
            {
                BlocksMap.Instance.PlacementLayer.SetTile(block.Position.ToVector3Int(), null);
            }
            SetFx(FXMaterial, PlaceColor);

        }

        void SetFx(Material mat, Color color)
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_FXColor", color);
            renderers
                .Where(renderer=>renderer)
                .ForEach(renderer =>
            {
                renderer.material = mat;
                renderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_FXColor", color);
                renderer.SetPropertyBlock(propertyBlock);

            });
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
                else if (Input.InputManager.Input.EditorMode.Place.UsePressed())
                {
                    yield return ClickMove();
                    yield break;
                }
                yield return null;
            }
            yield return DragMove();
            dragCoroutine = null;
        }

        IEnumerator DragMove()
        {
            //CameraManager.Instance.EditorCamera.Follow = transform;
            var position = transform.position;
            while (true)
            {
                yield return null;

                if (Project.Input.InputManager.CurrentInputScheme == Input.InputSchemes.Keyboard)
                {
                    position = CameraManager.Instance.CinemachineBrain.GetComponent<Camera>().ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                }
                else if (Input.InputManager.CurrentInputScheme == Input.InputSchemes.GamePad)
                {
                    position += (Input.InputManager.Input.EditorMode.Movement.ReadValue<Vector2>() * GamepadMoveSpeed * Time.deltaTime).ToVector3();
                }
                MoveTo(position);

                CanPlace = !CheckBlockOccupation();

                if (CanPlace)
                    SetFx(FXMaterial, PlaceColor);
                else
                    SetFx(FXMaterial, ErrorColor);

                if (CanPlace && UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Dragging = false;
                    //CameraManager.Instance.EditorCamera.Follow = null;
                    yield break;
                }
            }
        }

        IEnumerator ClickMove()
        {
            //CameraManager.Instance.EditorCamera.Follow = transform;
            var position = transform.position;
            while (true)
            {
                yield return null;

                if(Project.Input.InputManager.CurrentInputScheme == Input.InputSchemes.Keyboard)
                {
                    position = CameraManager.Instance.CinemachineBrain.GetComponent<Camera>().ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                }
                else if ( Input.InputManager.CurrentInputScheme == Input.InputSchemes.GamePad)
                {
                    position += (Input.InputManager.Input.EditorMode.Movement.ReadValue<Vector2>() * GamepadMoveSpeed * Time.deltaTime).ToVector3();
                }
                MoveTo(position);

                CanPlace = !CheckBlockOccupation();

                if(CanPlace)
                {
                    SetFx(FXMaterial, PlaceColor);
                    if(Input.InputManager.Input.EditorMode.Place.UsePressed())
                    {
                        Dragging = false;
                        Place();
                        //CameraManager.Instance.EditorCamera.Follow = null;
                        yield break;
                    }
                }
                else
                {
                    SetFx(FXMaterial, ErrorColor);
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
            if (Placed)
                return;
            if (Dragging)
                return;
            Dragging = true;
            Input.InputManager.Input.EditorMode.Place.UsePressed();
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
            if (Placed)
                return;
            rotateStep++;
            rotateStep %= 4;
            GenerateBlockInstance();

            CanPlace = !CheckBlockOccupation();

            if (CanPlace)
                SetFx(FXMaterial, PlaceColor);
            else
                SetFx(FXMaterial, ErrorColor);
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
            renderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void SetComponent(UserComponentUIData data)
        {
            ComponentData = data;
            GenerateBlockInstance();
        }
    }

}