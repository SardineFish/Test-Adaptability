using UnityEngine;
using System.Collections;
using System;

namespace Project
{
    public class PhysicsSystem : Singleton<PhysicsSystem>
    {
        public static event Action BeforePhysicsSimulation;
        public static event Action AfterPhysicsSimulation;
        static Collider2D[] overlapPool = new Collider2D[64];
        static RaycastHit2D[] raycastHit2DPool = new RaycastHit2D[64];

        private void FixedUpdate()
        {
            BeforePhysicsSimulation?.Invoke();

            Physics2D.Simulate(Time.fixedDeltaTime);

            AfterPhysicsSimulation?.Invoke();
        }

        public static Collider2D[] GetOverlap2DPool(int size)
        {
            if (size > overlapPool.Length)
                Array.Resize(ref overlapPool, size);
            return overlapPool;
        }

        public static RaycastHit2D[] GetRaycastHit2DPool(int size)
        {
            if (size > raycastHit2DPool.Length)
                Array.Resize(ref raycastHit2DPool, size);
            return raycastHit2DPool;
        }
    }
}
