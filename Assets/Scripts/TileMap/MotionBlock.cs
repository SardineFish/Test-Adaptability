using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Project.GameMap;

namespace Project.Blocks
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical,
    }
    [CreateAssetMenu(fileName ="MotionBlock",menuName ="Blocks/MotionBlock")]
    public class MotionBlock : Block
    {
        public MoveDirection Direction;
        public float Speed = 5;
        public override void ProcessMergedBlocks(MergedBlocks blocks)
        {
            var instance = BlocksMap.CreateBlockInstance(this, new MotionData()
            {
                velocity = Direction == MoveDirection.Horizontal
                    ? Vector2.right * Speed
                    : Vector2.up * Speed
            });
            instance.name = $"MotionBlocks{blocks.Bound.center.x},{blocks.Bound.center.y}";
            instance.MergedBlocks = blocks;
            instance.EnableRenderer = true;
            instance.EnableCollider = true;
            var rigidbody = instance.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.useFullKinematicContacts = true;
        }

        public override void PostBlockProcess(BlockData data)
        {
            BlocksMap.RemoveBlock(data.Position);
        }
        RaycastHit2D[] hits = new RaycastHit2D[16];
        public override void UpdateInstance(BlockInstance instance)
        {
            var data = instance.GetData<MotionData>();
            if (MergeMode == BlockMergeMode.Both)
            {

            }
            else
            {
                var pos = instance.transform.position.ToVector2() + instance.BoxCollider.offset;
                var dir = instance.BoxCollider.size / 2 * data.velocity.normalized;
                var count = Physics2D.RaycastNonAlloc(pos, dir.normalized, hits, dir.magnitude, 1 << 11);
                Debug.DrawLine(pos, pos + dir, Color.red);
                for (int i = 0; i < count; i++)
                {
                    if (hits[i].rigidbody == instance.GetComponent<Rigidbody2D>())
                        continue;
                    var block = hits[i].rigidbody?.GetComponent<IBlockInstance>()?.GetContactedBlock(hits[i].point, hits[i].normal);
                    if (block)
                    {
                        data.velocity = -data.velocity;
                        break;
                    }
                }

            }
            instance.GetComponent<Rigidbody2D>().velocity = data.velocity;
        }

        public override void OnCollision(BlockInstance instance, Collision2D collision)
        {
            /*
            if (collision.rigidbody.GetComponent<GameEntity>())
                return;
            var data = instance.GetData<MotionData>();
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contacts = new ContactPoint2D[10];
                instance.GetComponent<CompositeCollider2D>().co
                var contact = collision.GetContact(i);
                if (Vector2.Dot(contact.normal, data.velocity.normalized) < -0.9f)
                {
                    Debug.DrawLine(contact.point, contact.point + contact.normal, Color.blue);
                    data.velocity = -data.velocity;
                    instance.GetComponent<Rigidbody2D>().velocity = data.velocity;
                }
            }*/
        }

        public class MotionData : BlockInstanceData
        {
            public Vector2 velocity;
        }

    }

}