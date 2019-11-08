using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Project.Blocks;

namespace Project.GameMap
{
    public class PlatformInstance : BlockInstance
    {
        [DisplayInInspector]
        bool enablePass = false;
        PlatformEffector2D platformEffector;
        List<GameEntity> passingEntities;

        private void OnDestroy()
        {
            TilePlatformManager.RemoveEntity(this);
        }

        protected override void Awake()
        {
            base.Awake();
            platformEffector = GetComponent<PlatformEffector2D>();
            TilePlatformManager.RegisterEntity(this);
        }

        Collider2D[] overlapColliders = new Collider2D[16];
        void Update()
        {
            if (enablePass)
                return;
            if(!Physics2D.OverlapBox(transform.position.ToVector2() + BoxCollider.offset, BoxCollider.size, 0, 1 << 9))
            {
                BoxCollider.enabled = true;
            }
        }

        public override Block GetContactedBlock(Vector3 point, Vector3 normal)
        {
            if (Mathf.Abs(point.y - (transform.position.y + BoxCollider.offset.y + BoxCollider.size.y / 2)) < 0.0625f)
                return BlockType;
            return null;
        }

        public void AllowPass(GameEntity entity)
        {
            enablePass = true;
            BoxCollider.enabled = false;
        }

        public void BlockPass(GameEntity entity)
        {
            enablePass = false;
        }

        public override void UpdateInstance(BlockInstanceOptions options)
        {
            base.UpdateInstance(options);
            BoxCollider.usedByEffector = true;
            platformEffector = gameObject.GetOrAddComponent<PlatformEffector2D>();
            platformEffector.useColliderMask = false;
            platformEffector.sideArc = 0;
            platformEffector.surfaceArc = 170;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

}
