using UnityEngine;
using System.Collections;

namespace Project
{
    public abstract class EntityBehaviour : MonoBehaviour, IEntityLifeCycle
    {
        public GameEntity Entity { get; private set; }

        protected virtual void Awake()
        {
            Entity = GetComponent<GameEntity>();
            if (!Entity)
                Entity = GetComponentInParent<GameEntity>();
        }


        public virtual void OnActive() { }
        public virtual void OnInactive() { }
    }
}

