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
        public Vector2 DeadZoneRange = new Vector2(.5f, .5f);
        Vector2 velocity;

        public Transform Follow;

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
            Vector2 movement = Vector2.zero;
            if(Follow)
            {
                var screenPos = math.float2(CameraManager.Instance.MainCamera.WorldToViewportPoint(Follow.position).ToVector2());
                var centerPos = screenPos - math.float2(.5f, .5f);
                var overflow = math.sign(centerPos) * math.clamp(math.abs(centerPos) - math.float2(DeadZoneRange) * .5f, 0, 1);
                var follow = overflow / ((1 - math.float2(DeadZoneRange)) * .5f);
                movement = follow;
                
            }
            var inputMovement = input.EditorMode.CameraMovement.ReadValue<Vector2>();
            if (inputMovement.magnitude > 0)
                movement = input.EditorMode.CameraMovement.ReadValue<Vector2>();

            var v = movement * MoveSpeed;

            var damping = (1 - Mathf.Sqrt(Damping)) * 60;
            velocity = math.lerp(velocity, v, Time.fixedDeltaTime * damping);
            transform.position = CameraManager.Instance.CinemachineBrain.transform.position + velocity.ToVector3() * Time.fixedDeltaTime;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, CameraManager.Instance.ScreenWorldSize * DeadZoneRange);
        }
    }
}
