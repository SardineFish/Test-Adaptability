using UnityEngine;
using System.Collections;
using Project.Blocks;

namespace Project.GameMap
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
    public class ExplosionBlockInstance : MonoBehaviour
    {
        public BlockData BlockData;
        public BlockInstance BlockInstance;
        public ExplosionBlock BlockType => BlockInstance.BlockType as ExplosionBlock;
        public RuntimeAnimatorController ExplosionAnimator => BlockType.ExplosionAnimator;
        bool Contacted = false;
        bool exploded = false;
        Collider2D[] overlapColliders = new Collider2D[16];
        new BoxCollider2D collider;
        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
        }
        private void Start()
        {
            StartCoroutine(WaitForContact());
            var trigger = gameObject.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
            GetComponent<Animator>().runtimeAnimatorController = BlockType.ExplosionAnimator;
        }

        private void FixedUpdate()
        {
            Contacted = false;
        }
        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody?.GetComponent<Player>() || (BlockType.TriggerByBlock && collision.attachedRigidbody?.GetComponent<BlockInstance>()))
                Contacted = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody?.GetComponent<Player>() || (BlockType.TriggerByBlock && collision.attachedRigidbody?.GetComponent<BlockInstance>()))
                Contacted = true;
        }

        public void TriggerExplosion()
        {
            if (exploded)
                return;
            StopAllCoroutines();
            StartCoroutine(Explosion());
        }

        IEnumerator WaitForContact()
        {
            exploded = false;
            collider.enabled = true;
            while (!Contacted)
                yield return new WaitForFixedUpdate();
            StartCoroutine(Explosion());
        }

        Collider2D[] overlapResults = new Collider2D[16];
        IEnumerator Explosion()
        {
            exploded = true;
            GetComponent<Animator>().SetTrigger("CountDown");
            yield return new WaitForSeconds(BlockType.CountDownTime);

            GetComponent<Animator>().SetTrigger("Explode");
            collider.enabled = false;
            var data = BlockInstance.GetData<ExplosionBlock.Data>();
            for (int i = 1; i <= BlockType.ExplosionRange; i++)
            {
                for (int y = -i; y <= i; y++)
                {
                    for (int x = -i; x <= i; x++)
                    {
                        data.Blocks.Get(BlockData.Position + new Vector2Int(x, y))?.TriggerExplosion();
                    }
                }
            }

            yield return new WaitForSeconds(BlockType.RecoverTime);
        RetrySpawn:
            var count = Physics2D.OverlapBoxNonAlloc(transform.position.ToVector2() + collider.offset, collider.size, 0, overlapResults, 1 << 11);
            for (int i = 0; i < count; i++)
            {
                if (overlapResults[i].attachedRigidbody?.gameObject != BlockInstance.gameObject)
                {
                    yield return null;
                    goto RetrySpawn;
                }
            }
            GetComponent<Animator>().SetTrigger("Reset");
            StartCoroutine(WaitForContact());
        }

    }
}


