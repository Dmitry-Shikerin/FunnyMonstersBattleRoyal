using Fusion;
using Sources.EcsBoundedContexts.Input.Domain.Data;
using Sources.EcsBoundedContexts.Input.Domain.Data.Network;
using Sources.EcsBoundedContexts.NetworkCore;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.Frameworks.GameServices.InputServices
{
    public class NewInputService : IInputService
    {
        private NetworkCallbacksReceiver _callbacksReceiver;
        private readonly InputSystem_Actions _inputActions;
        private bool _jumpPerformed;

        public NewInputService()
        {
            _inputActions = new InputSystem_Actions();
        }

        public Vector2 MovementInput => _inputActions == null 
            ? Vector2.zero 
            : _inputActions.Player.Move.ReadValue<Vector2>();
        public bool IsJumpPerformed => GetJumpPerformed();

        public void Initialize()
        {
            _inputActions.Player.Jump.performed += JumpPerformed;
            _callbacksReceiver = NetworkRunnerProvider.NetworkCallbacksReceiver;
            _callbacksReceiver.OnPopulateInput += PopulateInput;
            _inputActions.Enable();
        }

        public void Destroy()
        {
            _inputActions.Player.Jump.performed -= JumpPerformed;
            _callbacksReceiver.OnPopulateInput -= PopulateInput;
            _inputActions.Disable();
        }

        private bool GetJumpPerformed()
        {
            bool accumulatedJump = _jumpPerformed;
            _jumpPerformed = false;
            return accumulatedJump;
        }

        private void JumpPerformed(InputAction.CallbackContext obj) =>
            _jumpPerformed = true;

        private void PopulateInput(NetworkRunner runner, NetworkInput input)
        {
            NetworkInputData data = new NetworkInputData
            {
                MovementInput = MovementInput,
                CameraForward = Camera.main.transform.forward,
            };
            data.InputButtons.Set(InputButtons.Jump, IsJumpPerformed);
            input.Set(data);
        }
    }
}