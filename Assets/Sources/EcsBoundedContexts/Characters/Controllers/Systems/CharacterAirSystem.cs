using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Input.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class CharacterAirSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoItExc _it = new(
            It.Inc<
                CharacterTag,
                AirComponent>(),
            It.Exc<JumpingComponent>());
        [DI] private readonly ProtoIt _inputIt = new(
            It.Inc<
                InputTag>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            { 
                //Move
                ProtoEntity inputEntity = _inputIt.First().Entity;
                Move(entity, inputEntity);
            }
        }

        private void Move(ProtoEntity entity, ProtoEntity inputEntity)
        {
            CharacterController characterController = entity.GetCharacterController().Value;
            CharacterConfig config = entity.GetCharacterConfig().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;

            //jump
            direction.y -= config.JumpPower * Time.deltaTime;

            //Форвард
            Transform transform = entity.GetTransform().Value;
            transform.forward = inputEntity.GetDirection().Value.normalized;

            characterController.Move(direction);
        }
    }
}