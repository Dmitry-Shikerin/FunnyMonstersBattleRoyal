using Fusion;
using Sources.BoundedContexts.Networks.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.BoundedContexts.Networks
{
    public class NetworkInputReceiver : NetworkBehaviour
    {
        public Vector3 MovementDirection;
        private NetworkButtons _previousButtons;
        
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData inputData) == false)
                return;

            Vector2 input = inputData.MovementInput;
            MovementDirection = new Vector3(input.x, 0, input.y);
            Debug.Log($"Direction {MovementDirection}, IsJump {inputData.InputButtons.WasPressed(_previousButtons, InputButtons.Jump)}");
        }
    }
}