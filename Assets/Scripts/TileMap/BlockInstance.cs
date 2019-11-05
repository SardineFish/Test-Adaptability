using UnityEngine;
using System.Collections;
using Project.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace Project.GameMap
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BlockInstance : MonoBehaviour, IBlockInstance
    {
        public Block BlockType { get; private set; }
        public BlocksCollection Blocks { get; private set; }
        public List<GameObject> BlocksObject { get; private set; }
        public BoxCollider2D BoxCollider { get; private set; }
        public Collider2D Collider { get; private set; }

        BlockInstanceData data;

        public bool IsStatic { get; set; }

        public Block GetContactedBlock(Vector3 point, Vector3 normal)
        {
            return BlockType;
        }

        private void Awake()
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.layer = 11;
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (!IsStatic)
                BlockType.UpdateInstance(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsStatic)
                BlockType.OnCollision(this, collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsStatic)
                BlockType.OnTrigger(this, collision);
        }

        public T GetData<T>() where T : BlockInstanceData
            => data as T;
        public void SetData<T>(T data) where T : BlockInstanceData
            => this.data = data;

        public T GetData<T>(Vector3 point, Vector3 normal) where T : BlockInstanceData
            => data as T;

        public void UpdateInstance(BlockInstanceOptions options)
        {
            if (Collider)
                Destroy(Collider);
            if (BlocksObject != null)
                BlocksObject.ForEach(block => Destroy(block));

            Blocks = options.Blocks;
            BlockType = options.BlockType;
            data = options.Data;
            IsStatic = options.IsStatic;
            gameObject.transform.position = options.Blocks.Bound.center.Set(z: options.positionZ);

            if (options.GenerateCollider && options.BlockType.MergeMode == BlockMergeMode.Both)
            {
                var composite = gameObject.AddComponent<CompositeCollider2D>();
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
                composite.offsetDistance = 0.01f;
                composite.isTrigger = options.IsTrigger;
                Collider = composite;
            }
            else if (options.GenerateCollider)
            {
                BoxCollider = gameObject.AddComponent<BoxCollider2D>();
                BoxCollider.size = options.Blocks.Bound.size.ToVector2();
                BoxCollider.isTrigger = options.IsTrigger;
                Collider = BoxCollider;
            }

            if (options.GenerateCollider || options.GenerateRenderer)
            {
                BlocksObject =
                options.Blocks.Select(block =>
                {
                    var obj = new GameObject();
                    obj.layer = 11;
                    obj.name = $"Block-{block.Position.x},{block.Position.y}";
                    obj.transform.parent = transform;
                    obj.transform.position = block.Position.ToVector3(transform.position.z) + new Vector3(.5f, .5f, 0);
                    if (options.GenerateRenderer)
                    {
                        var renderer = obj.AddComponent<SpriteRenderer>();
                        renderer.sprite = block.BlockType.sprite;
                    }
                    if (options.GenerateCollider && options.BlockType.MergeMode == BlockMergeMode.Both)
                    {
                        var collider = obj.AddComponent<BoxCollider2D>();
                        collider.offset = Vector2.zero;
                        collider.size = Vector2.one;
                        collider.usedByComposite = true;
                    }
                    options.BlockType.OnBlockObjectCreated(this, obj, block);
                    return obj;
                }).ToList();
            }
        }

        public static BlockInstance CreateInstance(BlockInstanceOptions options)
        {
            var gameobject = options.GameObject ?? new GameObject($"{options.BlockType.name}{options.Blocks.Bound.center.x},{options.Blocks.Bound.center.y}");
            var instance = gameobject.AddComponent<BlockInstance>();

            instance.UpdateInstance(options);

            return instance;
        }
    }

    public abstract class BlockInstanceData { }
    public struct BlockInstanceOptions
    {
        public float positionZ;
        public bool GenerateRenderer;
        public bool GenerateCollider;
        public bool IsTrigger;
        public BlocksCollection Blocks;
        public Block BlockType;
        public BlockInstanceData Data;
        public bool IsStatic;
        public GameObject GameObject;
        public BlockInstanceOptions(Block block)
        {
            positionZ = 0;
            GenerateRenderer = true;
            GenerateCollider = true;
            IsTrigger = false;
            Blocks = null;
            BlockType = block;
            Data = null;
            IsStatic = false;
            GameObject = null;
        }
    }
}
