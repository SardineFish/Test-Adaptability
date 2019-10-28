using UnityEngine;
using System.Collections;
using Project.Blocks;
using System.Collections.Generic;

namespace Project.GameMap
{
    public class BlockInstance : MonoBehaviour, IBlockInstance
    {
        public Blocks.Block BlockType;
        public List<BlockData> Blocks;
        public bool EnableRenderer;

        public Block GetContactedBlock(Vector3 point, Vector3 normal)
        {
            return BlockType;
        }

        // Use this for initialization
        void Start()
        {
            if(EnableRenderer)
            {
                Blocks.ForEach(block =>
                {
                    var obj = new GameObject();
                    obj.name = $"Renderer-{block.Position.x},{block.Position.y}";
                    obj.transform.parent = transform;
                    obj.transform.position = block.Position.ToVector3(transform.position.z) + new Vector3(.5f, .5f, 0);
                    var renderer = obj.AddComponent<SpriteRenderer>();
                    renderer.sprite = block.BlockType.sprite;
                });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
