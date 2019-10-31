using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    [CreateAssetMenu(fileName ="LaserBlock",menuName ="Blocks/Laser")]
    public class LaserBlock : Block
    {
        public float ActiveTime = 1;
        public float SleepTime = 1;
        public bool ActiveOnAwake = false;
        public GameObject LaserPrefab;
        public override void ProcessMergedBlocks(MergedBlocks blocks)
        {
            var instance = GameMap.BlocksMap.CreateBlockInstance(new GameMap.BlockInstanceOptions()
            {
                BlockType = this,
                Blocks = blocks,
                GenerateCollider = true,
                GenerateRenderer = true,
            });
            instance.SetData(new LaserData() { Coroutine = instance.StartCoroutine(LaserCoroutine(instance)) });

        }

        IEnumerator LaserCoroutine(GameMap.BlockInstance instance)
        {
            var obj = Instantiate(LaserPrefab);
            obj.name = "Laser";
            var laser = obj.GetComponent<FX.Laser>();
            laser.BlockInstance = instance;
            laser.transform.parent = instance.transform;
            laser.transform.position = instance.transform.position + DirectionVector.ToVector3() * .5f;
            laser.transform.rotation = Quaternion.FromToRotation(Vector3.right, DirectionVector);
            laser.OnTrigger += (collider) =>
            {
                var player = collider.attachedRigidbody?.GetComponent<Player>();
                if (player)
                    player.Kill();
            };

            if(ActiveOnAwake)
            {
                laser.PowerOn(0.05f);
                foreach (var t in Utility.FixedTimer(ActiveTime))
                    yield return new WaitForFixedUpdate();
                laser.ShutDown(0.2f);
            }

            while(true)
            {
                foreach (var t in Utility.FixedTimer(SleepTime))
                    yield return new WaitForFixedUpdate();

                laser.PowerOn(0.05f);

                foreach (var t in Utility.FixedTimer(ActiveTime))
                    yield return new WaitForFixedUpdate();

                laser.ShutDown(0.2f);
            }
        }

        public class LaserData : GameMap.BlockInstanceData
        {
            public Coroutine Coroutine;
        }
    }

}