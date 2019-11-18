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
        public override void ProcessMergedBlocks(BlocksCollection blocks)
        {
            var instance = BlocksMap.CreateBlockInstance(new BlockInstanceOptions()
            {
                BlockType=this,
                Blocks=blocks,
                GenerateRenderer=true,
                GenerateCollider=true,
                Data= new MotionData()
                {
                    velocity = Direction == MoveDirection.Horizontal
                    ? Vector2.right * Speed
                    : Vector2.up * Speed
                }
            });
            var rigidbody = instance.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.useFullKinematicContacts = true;
        }

        public override void PostBlockProcess(BlockData data)
        {
            BlocksMap.RemoveBlock(data.Position);
        }
        RaycastHit2D[] hits = new RaycastHit2D[16];
        bool HitForward(BlockInstance instance)
        {
            var data = instance.GetData<MotionData>();
            var dir = data.velocity.normalized * .5f;
            if (Mathf.Approximately(data.velocity.magnitude, 0))
                dir = this.Direction == MoveDirection.Horizontal
                    ? Vector2.right
                    : Vector2.up;
            dir += dir.normalized * 0.01f;
            for (var blockIdx = 0; blockIdx < instance.Blocks.BlocksList.Count; blockIdx++)
            {
                var block = instance.Blocks.BlocksList[blockIdx];
                var pos = block.Position.ToVector3() - instance.Blocks.Bound.center + instance.transform.position + new Vector3(.5f, .5f, 0);
                var count = Physics2D.RaycastNonAlloc(pos, dir.normalized, hits, dir.magnitude, 1 << 11);
                Debug.DrawLine(pos, pos + dir.ToVector3(), Color.red);
                for (int i = 0; i < count; i++)
                {
                    if (hits[i].collider.isTrigger)
                        continue;
                    if (hits[i].rigidbody == instance.GetComponent<Rigidbody2D>())
                        continue;
                    var hitBlock = hits[i].rigidbody?.GetComponent<IBlockInstance>()?.GetContactedBlock(hits[i].point, hits[i].normal);
                    if (hitBlock)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool HitBackward(BlockInstance instance)
        {
            var data = instance.GetData<MotionData>();
            var dir = -data.velocity.normalized * .5f;
            if (Mathf.Approximately(data.velocity.magnitude, 0))
                dir = this.Direction == MoveDirection.Horizontal
                    ? Vector2.left
                    : Vector2.down;
            for (var blockIdx = 0; blockIdx < instance.Blocks.BlocksList.Count; blockIdx++)
            {
                var block = instance.Blocks.BlocksList[blockIdx];
                var pos = block.Position.ToVector3() - instance.Blocks.Bound.center + instance.transform.position + new Vector3(.5f, .5f, 0);
                var count = Physics2D.RaycastNonAlloc(pos, dir.normalized, hits, dir.magnitude, 1 << 11);
                Debug.DrawLine(pos, pos + dir.ToVector3(), Color.blue);
                for (int i = 0; i < count; i++)
                {
                    if (hits[i].collider.isTrigger)
                        continue;
                    if (hits[i].rigidbody == instance.GetComponent<Rigidbody2D>())
                        continue;
                    var hitBlock = hits[i].rigidbody?.GetComponent<IBlockInstance>()?.GetContactedBlock(hits[i].point, hits[i].normal);
                    if (hitBlock)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void UpdateInstance(BlockInstance instance)
        {
            var data = instance.GetData<MotionData>();

            if(HitForward(instance))
            {
                if (HitBackward(instance))
                    data.velocity = Vector2.zero;
                else if (Mathf.Approximately(data.velocity.magnitude, 0))
                    data.velocity = (Direction == MoveDirection.Horizontal ? Vector2.left : Vector2.down) * Speed;
                else
                    data.velocity = -data.velocity;
            }
            else if (Mathf.Approximately(data.velocity.magnitude, 0))
                data.velocity = (Direction == MoveDirection.Horizontal ? Vector2.left : Vector2.down) * Speed;

            instance.GetComponent<Rigidbody2D>().velocity = data.velocity;
            /*
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
                    if (hits[i].collider.isTrigger)
                        continue;
                    if (hits[i].rigidbody == instance.GetComponent<Rigidbody2D>())
                        continue;
                    var block = hits[i].rigidbody?.GetComponent<IBlockInstance>()?.GetContactedBlock(hits[i].point, hits[i].normal);
                    if (block)
                    {
                        data.velocity = -data.velocity;
                        break;
                    }
                }

            }*/
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