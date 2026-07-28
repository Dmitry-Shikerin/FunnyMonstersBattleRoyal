using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Input.Domain;
using Sources.Frameworks.GameServices.InputServices.Inputs;
using Sources.Frameworks.GameServices.Pauses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.EcsBoundedContexts.Input.Controllers
{
    [EcsSystem(50)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class InputSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                InputTag,
                DirectionComponent>());

        private readonly IPauseService _pauseService;
        //private InputSystem_Actions _inputActions;
        private ProtoEntity _entity;

        public InputSystem(IPauseService pauseService)
        {
            _pauseService = pauseService;
            InputData = new InputData();
        }

        public InputData InputData { get; }

        public void Init(IProtoSystems systems)
        {
            // _inputActions.Enable();
            // _inputActions.Player.Look.performed += OnLookPerformed;
            // _inputActions.Player.Look.canceled += OnLookCanceled;
            // _inputActions.Player.Zoom.performed += OnZoomPerformed;
            // _inputActions.Player.Jump.performed += OnJumpPerformed;
            _entity = _it.First().Entity;
        }

        public void Run()
        {
            if (_pauseService == null)
                return;

            if (_pauseService.IsPaused)
                return;

            UpdateMovement();
        }
        
        // Обработчики Input System
        private void OnLookPerformed(InputAction.CallbackContext context)
        {
             Vector2 look = context.ReadValue<Vector2>();
             _entity.ReplaceCameraLook(look);
        }

        private void OnLookCanceled(InputAction.CallbackContext context)
        {
            _entity.ReplaceCameraLook(Vector2.zero);
        }

        private void OnZoomPerformed(InputAction.CallbackContext context)
        {
            // float zoomInput = context.ReadValue<float>();
            // _entity.ReplaceCameraZoom(zoomInput);
        }     
        
        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _entity.AddJumpEvent();
        }

        public void Destroy()
        {
            // _inputActions.Disable();
            // _inputActions.Player.Look.performed -= OnLookPerformed;
            // _inputActions.Player.Look.canceled -= OnLookCanceled;
            // _inputActions.Player.Zoom.performed -= OnZoomPerformed;
            // _inputActions.Player.Jump.performed -= OnJumpPerformed;
        }

        private void UpdateMovement()
        {
            // Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
            // Vector3 cameraForward = Camera.main.transform.forward;
            // cameraForward.y = 0;
            // float angle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
            // Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * new Vector3(input.x, 0, input.y);
            // _entity.ReplaceDirection(moveDirection);
        }
    }
}