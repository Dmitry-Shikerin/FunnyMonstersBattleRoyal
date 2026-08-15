using System;
using Fusion;
using Sources.EcsBoundedContexts.Input.Domain.Data.Network;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class InputReceiverViewComponent : NetworkBehaviour, IViewComponent
    {
        [field: SerializeField] public Vector3 MovementDirection { get; private set; }
        [field: SerializeField] public Vector3 CameraForward { get; private set; }
        
        private NetworkButtons _previousButtons;
        
        public PlayerRef PlayerRef { get; private set; }

        public event Action OnJump;
        
        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData inputData) == false)
                return;

            Vector2 input = inputData.MovementInput;
            Vector3 cameraForward = inputData.CameraForward;
            cameraForward.y = 0;
            float angle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
            MovementDirection = Quaternion.Euler(0, angle, 0) * new Vector3(input.x, 0, input.y);
            CameraForward = inputData.CameraForward;

            if (inputData.InputButtons.WasPressed(_previousButtons, InputButtons.Jump))
                OnJump?.Invoke();
        }
    }
}