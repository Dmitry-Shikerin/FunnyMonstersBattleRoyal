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
        private InputSystem_Actions _inputActions;
        private ProtoEntity _entity;

        public InputSystem(IPauseService pauseService)
        {
            _pauseService = pauseService;
            InputData = new InputData();
        }

        public InputData InputData { get; }

        public void Init(IProtoSystems systems)
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Enable();
            _inputActions.Player.Look.performed += OnLookPerformed;
            _inputActions.Player.Look.canceled += OnLookCanceled;
            _inputActions.Player.Zoom.performed += OnZoomPerformed;
            _inputActions.Player.Jump.performed += OnJumpPerformed;
            _entity = _it.First().Entity;
        }

        public void Run()
        {
            if (_pauseService == null)
                return;

            if (_pauseService.IsPaused)
                return;

            //Debug.Log("Update input");
            UpdateMovement();
            //UpdateAttack();
            UpdatePointerClick();
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
            Debug.Log($"AddJumpEvent");
        }

        private void UpdateSelectable(InputAction.CallbackContext obj)
        {
            //Debug.Log($"UpdateSelectable");
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // if (Physics.Raycast(
            //         ray, out RaycastHit raycastHit, float.MaxValue, GraphView.Layer.Selectable) == false)
            //     return;

            //Debug.Log($"RaycastHit: {raycastHit.collider.name}");
        }

        public void Destroy()
        {
            _inputActions.Disable();
            _inputActions.Player.Look.performed -= OnLookPerformed;
            _inputActions.Player.Look.canceled -= OnLookCanceled;
            _inputActions.Player.Zoom.performed -= OnZoomPerformed;
            _inputActions.Player.Jump.performed -= OnJumpPerformed;
        }

        private void UpdatePointerClick()
        {
            // InputData.PointerPosition = Vector3.zero;
            //
            // if (Input.GetMouseButtonDown(0) == false)
            //     return;

            if (TryGetLook(out Vector3 lookDirection) == false)
                return;

            InputData.PointerPosition = lookDirection;
        }

        private void UpdateStandState(InputAction.CallbackContext context) =>
            InputData.InvokeStand();

        // private void UpdateAttack() =>
        //     InputData.IsAttacking = _inputManager.Gameplay.Attack.IsPressed();

        private void UpdateMovement()
        {
            Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
            //float speed = _inputActions.Player.Run.ReadValue<float>();

            Vector3 lookDirection = Vector3.zero;

            // if (TryGetLook(out Vector3 look))
            //     lookDirection = look;

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;

            float angle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * new Vector3(input.x, 0, input.y);
            _entity.ReplaceDirection(moveDirection);
            // InputData.LookPosition = lookDirection;
            // InputData.Speed = speed;
        }

        private bool TryGetLook(out Vector3 lookDirection)
        {
            lookDirection = Vector3.zero;
            // Ray cameraPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            // if (Physics.Raycast(
            //         cameraPosition, out RaycastHit raycastHit, float.MaxValue, GraphView.Layer.Plane) == false)
            //     return false;

            //lookDirection = raycastHit.point;

            return true;
        }
    }
}