using Fusion;
using Reflex.Attributes;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.EcsBoundedContexts.Input.Domain.Data.Network;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class InputSenderViewComponent : MonoBehaviour, IViewComponent
    {
        private ISignalBus _signalBus;
        private NetworkCallbacksReceiver _callbacksReceiver;
        private InputSystem_Actions _inputActions;
        private bool _jumpPerformed;

        [Inject]
        public void Construct(ISignalBus signalBus)
        {
            _signalBus = signalBus;
            _inputActions = new InputSystem_Actions();
            Initialize();
        }

        public Vector2 MovementInput => _inputActions == null 
            ? Vector2.zero 
            : _inputActions.Player.Move.ReadValue<Vector2>();
        public bool IsJumpPerformed => GetJumpPerformed();
        public PlayerRef PlayerRef { get; private set; }

        private void Initialize()
        {
            _inputActions.Player.Jump.performed += JumpPerformed;
            _inputActions.UiKeys.Pause.performed += EscapePerformed;
            _callbacksReceiver = NetworkRunnerProvider.NetworkCallbacksReceiver;
            _callbacksReceiver.OnPopulateInput += PopulateInput;
            _inputActions.Enable();
        }

        public void OnDestroy()
        {
            _inputActions.Player.Jump.performed -= JumpPerformed;
            _inputActions.UiKeys.Pause.performed -= EscapePerformed;
            _callbacksReceiver.OnPopulateInput -= PopulateInput;
            _inputActions.Disable();
        }

        public void Init(PlayerRef playerRef) =>
            PlayerRef = playerRef;

        private void EscapePerformed(InputAction.CallbackContext obj) =>
            _signalBus.Handle(new KeyPressedSignal(KeyCode.Escape));

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
                CameraForward = UnityEngine.Camera.main.transform.forward,
            };
            data.InputButtons.Set(InputButtons.Jump, IsJumpPerformed);
            input.Set(data);
        }
    }
}