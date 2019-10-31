using UnityEngine;
using System.Collections;
using System.Linq;
using System;

namespace Project.FX
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Laser : MonoBehaviour
    {
        public float ColliderWidth = .5f;
        public float Length = 0;
        public event Action<Collider2D> OnTrigger;
        public GameMap.BlockInstance BlockInstance;
        public bool Active;
        new BoxCollider2D collider;
        new SpriteRenderer renderer;
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.offset = new Vector2(.5f, 0);
            collider.size = new Vector2(1, ColliderWidth);
        }
        // Use this for initialization
        void Start()
        {

        }

        public float GetLength()
        {
            var count = Physics2D.RaycastNonAlloc(transform.position, transform.right, hits, 100, 1 << 11);
            var minDist = 100f;
            for (var i = 0; i < count; i++)
            {
                var instance = hits[i].rigidbody?.GetComponent<GameMap.IBlockInstance>();
                if (instance as GameMap.BlockInstance == BlockInstance)
                    continue;
                var block = instance?.GetContactedBlock(hits[i].point, hits[i].normal);
                if (block is null || block is Blocks.Platform)
                    continue;
                if (hits[i].distance < minDist)
                    minDist = hits[i].distance;
            }
            return minDist;
        }

        RaycastHit2D[] hits = new RaycastHit2D[32];
        public void PowerOn(float time)
        {
            StartCoroutine(On(time));
        }
        IEnumerator On(float time)
        {
            var targetLength = GetLength();
            foreach(var t in Utility.FixedTimerNormalized(time))
            {
                var actualLength = GetLength();
                var length = targetLength * t;
                if (length >= actualLength)
                    break;
                Length = length;
                yield return new WaitForFixedUpdate();
            }
            Active = true;
        }

        public void ShutDown(float time)
        {
            StopAllCoroutines();
            StartCoroutine(Off(time));
        }
        IEnumerator Off(float time)
        {
            foreach(var t in Utility.FixedTimerNormalized(time))
            {
                transform.localScale = new Vector3(transform.localScale.x, 1 - t, 1);
                yield return new WaitForFixedUpdate();
            }
            transform.localScale = new Vector3(0, 1, 1);
            Length = 0;
            Active = false;
        }

        private void OnWillRenderObject()
        {
            if (Active)
                Length = GetLength();
            transform.localScale = new Vector3(Length, transform.localScale.y, 1);
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetVector("_Scale", transform.localScale);
            renderer.SetPropertyBlock(propertyBlock);
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrigger?.Invoke(collision);
        }
    }

}