using UnityEngine;
using System.Collections;
using Unity.Mathematics;

namespace Project.Controller
{

    public class EditorCameraController : MonoBehaviour
    {
        public float MoveSpeed = 5;
        [Range(0, 1)]
        public float Damping = 0.5f;
        Vector2 velocity;

        Input.GameInput input;
        private void Awake()
        {
            input = new Input.GameInput();
        }
        private void OnEnable()
        {
            input.Enable();
        }
        private void OnDisable()
        {
            input.Disable();
        }
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            var movement = input.EditorMode.CameraMovement.ReadValue<Vector2>();
            var v = movement * MoveSpeed;

            var damping = (1 - Mathf.Sqrt(Damping)) * 60;
            velocity = math.lerp(velocity, v, Time.fixedDeltaTime * damping);
            transform.position = CameraManager.Instance.CinemachineBrain.transform.position + velocity.ToVector3() * Time.fixedDeltaTime;
        }
    }
}
