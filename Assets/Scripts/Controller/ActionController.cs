using UnityEngine;
using System.Collections;


namespace Project.Controller
{
    public enum FaceMethod
    {
        FlipRoot,
        FlipRenderer,
        RotateRenderer,
        RotateRoot,
    }
    public class ActionController : EntityBehaviour
    {
        public FaceMethod FaceMethod;
        float direction = 1;
        [DisplayInInspector]
        public float Direction
        {
            get => direction;
            set
            {
                if (value == 0)
                    return;
                var dir = MathUtility.SignInt(value);
                if(dir != direction)
                {
                    direction = dir;
                    Flip();
                }
            }
        }

        public float SetDirection(float dir)
        {
            Direction = dir;
            return Direction;
        }

        void Flip()
        {
            if(FaceMethod == FaceMethod.FlipRoot)
            {
                var scale = Vector3.Scale(Entity.transform.localScale, new Vector3(-1, 1, 1));
                Entity.transform.localScale = scale;
            }
            else if (FaceMethod == FaceMethod.FlipRenderer)
            {
                var renderer = Entity.GetComponentInChildren<SpriteRenderer>();
                var scale = Vector3.Scale(renderer.transform.localScale, new Vector3(-1, 1, 1));
                renderer.transform.localScale = scale;
            }
        }
    }

}