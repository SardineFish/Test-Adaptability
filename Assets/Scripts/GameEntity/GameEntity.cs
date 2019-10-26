using UnityEngine;
using System.Collections;

namespace Project
{
    public class GameEntity : MonoBehaviour
    {
        public GameEntity()
        {
            EntityManager.RegisterEntity(this);
        }

        protected virtual void OnDestroy()
        {
            EntityManager.RemoveEntity(this);
        }

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}