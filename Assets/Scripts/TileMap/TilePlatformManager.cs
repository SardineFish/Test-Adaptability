using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;

namespace Project.GameMap
{
    [RequireComponent(typeof(Tilemap))]
    public class TilePlatformManager : Singleton<TilePlatformManager>
    {
        public BoundsInt Bound;
        public Transform PlatformContainer;
        public static List<Platform> Platforms { get; private set; }
        
        Tilemap tilemap;
        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }
        // Use this for initialization
        [EditorButton("Load")]
        void Start()
        {
            if (!tilemap)
                tilemap = GetComponent<Tilemap>();
            if (Application.isPlaying)
                PlatformContainer.gameObject.ClearChildren();
            else
                PlatformContainer.gameObject.ClearChildImmediate();
            List<PlatformRange> platforms = new List<PlatformRange>();
            for (var y = Bound.position.y; y < Bound.size.y; y++)
            {
                for (var x = Bound.position.x; x < Bound.size.x; x++)
                {
                    if (tilemap.GetTile(new Vector3Int(x, y, 0)))
                    {
                        var startX = x;
                        while (x < Bound.size.x && tilemap.GetTile(new Vector3Int(x, y, 0)))
                            x++;
                        platforms.Add(new PlatformRange(new Vector2Int(startX, y), new Vector2Int(x, y)));
                    }
                }
            }
            Platforms = platforms.Select(platform =>
            {
                var obj = new GameObject();
                obj.transform.parent = PlatformContainer;
                obj.transform.position = PlatformContainer.position + platform.center.ToVector3();
                obj.layer = 10;
                obj.name = $"Platform{platform.Begin.x}-{platform.Begin.y}";
                var collider = obj.AddComponent<BoxCollider2D>();
                collider.size = platform.size;
                collider.usedByEffector = true;
                var effector = obj.AddComponent<PlatformEffector2D>();
                effector.useColliderMask = false;
                effector.sideArc = 0;
                effector.surfaceArc = 170;
                var rigidBody = obj.AddComponent<Rigidbody2D>();
                rigidBody.bodyType = RigidbodyType2D.Static;
                return obj.AddComponent<Platform>();
            }).ToList();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Bound.center, Bound.size);
        }
    }

    public struct PlatformRange
    {
        public Vector2Int Begin;
        public Vector2Int End;

        public Vector2 center => new Vector2((End.x + Begin.x) / 2.0f, Begin.y + 0.5f);
        public Vector2 size => new Vector2(End.x - Begin.x, 1);

        public PlatformRange(Vector2Int begin, Vector2Int end)
        {
            Begin = begin;
            End = end;
        }
    }


}
