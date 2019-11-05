using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using System;

namespace Project.GameMap.Editor
{
    public enum PlaceMode
    {
        Drag,
        Click
    }

    public class ComponentPlacement : MonoBehaviour
    {
        public UserComponentUIData Component;
        public BlockInstance BlockInstance;
        public PlaceMode PlaceMode;
        
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
            if(!Placed)
            {
                float2 position = Level.Instance.MainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).ToVector2();
                var snapOffset = math.frac(Component.Component.Bound.center.ToVector2());
                position -= snapOffset;
                position = math.round(position);
                position += snapOffset;
                rigidbody.MovePosition(position);
                CanPlace = !CheckBlockOccupation();
                if (CanPlace && UnityEngine.Input.GetKey(KeyCode.Mouse0))
                    Place();
            }
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
        void Place()
        {
            if (Placed)
                return;
            var offset = Component.Component.Bound.size.ToVector2() / 2;
            foreach (var block in Component.Component)
            {
                var pos = Component.Component.Bound.min + block.Position.ToVector3Int() + (transform.position.ToVector2() - offset).ToVector3Int();
                BlocksMap.Instance.PlacementLayer.SetTile(pos, Component.Component.BlockType);
            }
            Placed = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTrigger?.Invoke(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            onTrigger?.Invoke(collision);
        }

        public static ComponentPlacement Create(UserComponentUIData component, PlaceMode placeMode)
        {
            var instance = BlockInstance.CreateInstance(new BlockInstanceOptions()
            {
                BlockType = component.Component.BlockType,
                Blocks = component.Component,
                GenerateRenderer = true,
                GenerateCollider = true,
                IsTrigger = true,
                IsStatic = true
            });
            var placement = instance.gameObject.AddComponent<ComponentPlacement>();
            placement.rigidbody = instance.GetComponent<Rigidbody2D>();
            placement.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            placement.rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
            placement.BlockInstance = instance;
            placement.Component = component;
            placement.PlaceMode = placeMode;
            placement.transform.parent = BlocksMap.Instance.PlacementLayer.transform;
            return placement;
        }
    }

}