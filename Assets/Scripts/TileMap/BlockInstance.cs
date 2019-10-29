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
        public Block BlockType;
        public MergedBlocks MergedBlocks;
        public List<BlockData> Blocks => MergedBlocks.Blocks;
        public List<GameObject> BlocksObject { get; private set; }
        public BoxCollider2D BoxCollider { get; private set; }
        public bool EnableRenderer;
        public bool EnableCollider;
        BlockInstanceData data;

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
            transform.position = MergedBlocks.Bound.center.Set(z: transform.position.z);
            if (EnableCollider && BlockType.MergeMode == BlockMergeMode.Both)
            {
                var composite = gameObject.AddComponent<CompositeCollider2D>();
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
            }
            else if (EnableCollider)
            {
                var merged = new MergedBlocks() { Blocks = Blocks };
                BoxCollider = gameObject.AddComponent<BoxCollider2D>();
                BoxCollider.size = MergedBlocks.Bound.size.ToVector2();
            }
            if (EnableRenderer || EnableCollider)
            {
                BlocksObject =
                Blocks.Select(block =>
                {
                    var obj = new GameObject();
                    obj.layer = 11;
                    obj.name = $"Block-{block.Position.x},{block.Position.y}";
                    obj.transform.parent = transform;
                    obj.transform.position = block.Position.ToVector3(transform.position.z) + new Vector3(.5f, .5f, 0);
                    if (EnableRenderer)
                    {
                        var renderer = obj.AddComponent<SpriteRenderer>();
                        renderer.sprite = block.BlockType.sprite;
                    }
                    if (EnableCollider && BlockType.MergeMode == BlockMergeMode.Both)
                    {
                        var collider = obj.AddComponent<BoxCollider2D>();
                        collider.offset = Vector2.zero;
                        collider.size = Vector2.one;
                        collider.usedByComposite = true;
                    }
                    BlockType.OnBlockObjectCreated(this, obj, block);
                    return obj;
                }).ToList();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            BlockType.UpdateInstance(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BlockType.OnCollision(this, collision);
        }

        public T GetData<T>() where T : BlockInstanceData
            => data as T;
        public void SetData<T>(T data) where T : BlockInstanceData
            => this.data = data;

        public T GetData<T>(Vector3 point, Vector3 normal) where T : BlockInstanceData
            => data as T;
    }

    public abstract class BlockInstanceData { }
}
