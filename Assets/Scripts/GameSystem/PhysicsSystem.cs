using UnityEngine;
using System.Collections;
using System;

namespace Project
{
    public class PhysicsSystem : Singleton<PhysicsSystem>
    {
        public static event Action BeforePhysicsSimulation;
        public static event Action AfterPhysicsSimulation;

        private void FixedUpdate()
        {
            BeforePhysicsSimulation?.Invoke();

            Physics2D.Simulate(Time.fixedDeltaTime);

            AfterPhysicsSimulation?.Invoke();
        }
    }
}
