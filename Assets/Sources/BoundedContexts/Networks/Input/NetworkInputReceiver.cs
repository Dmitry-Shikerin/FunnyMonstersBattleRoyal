using Fusion;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks.Core;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Input
{
    public class NetworkInputReceiver : NetworkBehaviour
    {
        [Required] [SerializeField] private EntityLink _character;
        
        public Vector3 MovementDirection;
        public Vector3 CameraForward;
        private NetworkButtons _previousButtons;
        
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData inputData) == false)
                return;

            Vector2 input = inputData.MovementInput;
            MovementDirection = new Vector3(input.x, 0, input.y);
            CameraForward = inputData.CameraForward;

            if (_character.IsInitialized == false)
                return;

            ProtoEntity characterEntity = _character.Entity;
            
            if (characterEntity.HasInputEntity() == false)
                return;

            if (characterEntity.GetInputEntity().Value.HasNetworkInputDirection() == false)
                return;

            ProtoEntity inputEntity = _character.Entity.GetInputEntity().Value;
            inputEntity.ReplaceNetworkInputDirection(MovementDirection);
            inputEntity.ReplaceNetworkCameraForward(CameraForward);

            if (inputData.InputButtons.WasPressed(_previousButtons, InputButtons.Jump))
                inputEntity.AddJumpEvent();
            //Debug.Log($"Direction {MovementDirection}, IsJump {inputData.InputButtons.WasPressed(_previousButtons, InputButtons.Jump)}");
        }
    }
}