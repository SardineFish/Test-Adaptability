using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Project.GameMap
{
    public class PlatformInstance : MonoBehaviour
    {
        [DisplayInInspector]
        bool enablePass = false;
        new BoxCollider2D collider;
        PlatformEffector2D platformEffector;
        List<GameEntity> passingEntities;

        private void OnDestroy()
        {
            TilePlatformManager.RemoveEntity(this);
        }

        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
            platformEffector = GetComponent<PlatformEffector2D>();
            TilePlatformManager.RegisterEntity(this);
        }

        Collider2D[] overlapColliders = new Collider2D[16];
        void Update()
        {
            if (enablePass)
                return;
            if(!Physics2D.OverlapBox(transform.position.ToVector2() + collider.offset, collider.size, 0, 1 << 9))
            {
                collider.enabled = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {

        }

        public void AllowPass(GameEntity entity)
        {
            enablePass = true;
            collider.enabled = false;
        }

        public void BlockPass(GameEntity entity)
        {
            enablePass = false;
        }
    }

}
