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

        RaycastHit2D[] hits = new RaycastHit2D[32];
        public void PowerOn()
        {
            var count = Physics2D.RaycastNonAlloc(transform.position, transform.right, hits, 100, 1 << 11);
            for (var i = 0; i < count; i++)
            {
                var block = hits[i].rigidbody?.GetComponent<GameMap.IBlockInstance>()?.GetContactedBlock(hits[i].point, hits[i].normal);
                if(block!=null && block.Static)
                {
                    StartCoroutine(On(hits[i].distance));
                    return;
                }
            }
        }
        IEnumerator On(float length)
        {
            foreach(var t in Utility.TimerNormalized(0.05f))
            {
                Length = length * t;
                yield return null;
            }
        }

        public void ShutDown()
        {
            StopAllCoroutines();
            StartCoroutine(Off());
        }
        IEnumerator Off()
        {
            foreach(var t in Utility.TimerNormalized(.2f))
            {
                transform.localScale = new Vector3(transform.localScale.x, 1 - t, 1);
                yield return null;
            }
            transform.localScale = new Vector3(0, 1, 1);
            Length = 0;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localScale = new Vector3(Length, transform.localScale.y, 1);
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetVector("_Scale", transform.localScale);
            renderer.SetPropertyBlock(propertyBlock);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTrigger?.Invoke(collision);
        }
    }

}