using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.EcsBoundedContexts.Cameras.Controllers
{
    [EcsSystem(51)]
    [ComponentGroup(ComponentGroup.Camera)]
    [Aspect(AspectName.Game)]
    public class CameraRotationSystem : IProtoRunSystem, IProtoInitSystem
    {
        public class CameraSettings
        {
            public float distance = 5f;
            public float height = 2f;
            public float mouseSensitivity = 3f;
            public float smoothTime = 0.2f;
            public Vector2 pitchLimits = new Vector2(-40f, 80f);
            public bool invertY = false;
        }

        public class CollisionSettings
        {
            public LayerMask obstacleMask = -1;
            public float radius = 0.5f;
            public float minDistance = 1f;
            public float smoothTime = 0.1f;
        }

        public class ZoomSettings
        {
            public bool enabled = true;
            public float zoomSpeed = 2f;
            public float minDistance = 2f;
            public float maxDistance = 10f;
        }

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                MainCameraTag>());

        public Transform target;
        public CameraSettings cameraSettings = new CameraSettings();
        public CollisionSettings collisionSettings = new CollisionSettings();
        public ZoomSettings zoomSettings = new ZoomSettings();

        // Input Actions
        private Vector2 lookInput;
        private float zoomInput;

        private float currentYaw;
        private float currentPitch;
        private float currentDistance;
        private Vector3 positionSmoothVelocity = Vector3.zero;

        public void Init(IProtoSystems systems)
        {
            // LockCursor();
            // InitializeCamera();
        }

        public void Run()
        {
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void InitializeCamera()
        {
            if (target != null)
            {
                //Vector3 angles = transform.eulerAngles;
                // currentYaw = angles.y;
                // currentPitch = angles.x;
                // currentDistance = cameraSettings.distance;
            }
        }

        void Update()
        {
            if (target == null)
                return;
        
            // Обработка ввода мыши через Input System
            float mouseX = lookInput.x * cameraSettings.mouseSensitivity * Time.deltaTime;
            float mouseY = lookInput.y * cameraSettings.mouseSensitivity * Time.deltaTime;
        
            currentYaw += mouseX;
            currentPitch += cameraSettings.invertY ? mouseY : -mouseY;
            currentPitch = Mathf.Clamp(currentPitch, cameraSettings.pitchLimits.x, cameraSettings.pitchLimits.y);
        
            // Зум через Input System
            if (zoomSettings.enabled && zoomInput != 0)
            {
                cameraSettings.distance -= zoomInput * zoomSettings.zoomSpeed * Time.deltaTime;
                cameraSettings.distance = Mathf.Clamp(cameraSettings.distance,
                    zoomSettings.minDistance, zoomSettings.maxDistance);
            }
        
            // Управление курсором
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        
            if (Mouse.current.leftButton.wasPressedThisFrame && Cursor.visible)
            {
                LockCursor();
            }
        }
        //
        // void LateUpdate()
        // {
        //     if (target == null) return;
        //
        //     Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        //     Vector3 targetPosition = target.position + Vector3.up * cameraSettings.height;
        //
        //     float targetDistance = CheckCollisions(targetPosition, rotation);
        //     currentDistance = Mathf.Lerp(currentDistance, targetDistance, collisionSettings.smoothTime);
        //
        //     Vector3 desiredPosition = targetPosition - (rotation * Vector3.forward * currentDistance);
        //     transform.position = Vector3.SmoothDamp(transform.position, desiredPosition,
        //         ref positionSmoothVelocity, cameraSettings.smoothTime);
        //     transform.rotation = rotation;
        // }
        //
        // float CheckCollisions(Vector3 targetPosition, Quaternion rotation)
        // {
        //     float targetDistance = cameraSettings.distance;
        //     Vector3 direction = -rotation * Vector3.forward;
        //
        //     RaycastHit hit;
        //     if (Physics.SphereCast(targetPosition, collisionSettings.radius,
        //             direction, out hit, cameraSettings.distance, collisionSettings.obstacleMask))
        //     {
        //         targetDistance = Mathf.Max(hit.distance - 0.3f, collisionSettings.minDistance);
        //     }
        //
        //     return targetDistance;
        // }

    }
}