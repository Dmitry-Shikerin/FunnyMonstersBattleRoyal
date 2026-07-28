using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Input.Domain;
using Sources.Frameworks.GameServices.InputServices.Inputs;
using Sources.Frameworks.GameServices.Pauses;
using UnityEngine;

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
        private ProtoEntity _entity;

        public InputSystem(IPauseService pauseService)
        {
            _pauseService = pauseService;
            InputData = new InputData();
        }

        public InputData InputData { get; }

        public void Init(IProtoSystems systems)
        {
            _entity = _it.First().Entity;
        }

        public void Run()
        {
            if (_pauseService == null)
                return;

            if (_pauseService.IsPaused)
                return;

            foreach (ProtoEntity entity in _it)
            {
                UpdateMovement(entity);
            }
        }

        public void Destroy()
        {
        }

        private void UpdateMovement(ProtoEntity entity)
        {
            Vector3 networkInput = entity.GetNetworkInputDirection().Value;
            Vector3 cameraForward = entity.GetNetworkCameraForward().Value;
            cameraForward.y = 0;
            float angle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * networkInput;
            entity.ReplaceDirection(moveDirection);
        }
    }
}