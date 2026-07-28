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
    public class JumpSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                JumpingComponent>());
        [DI] private readonly ProtoIt _inputIt = new(
            It.Inc<
                InputTag>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref JumpingComponent jumping = ref entity.GetJumping();
                CharacterConfig config = entity.GetCharacterConfig().Value;
                Transform transform = entity.GetTransform().Value;

                jumping.JumpTimer -= Time.deltaTime;
                //transform.position += new Vector3(0, 5f * Time.deltaTime, 0);

                //Move
                ProtoEntity inputEntity = _inputIt.First().Entity;
                Move(entity, inputEntity);
                
                // Завершение прыжка
                float currentHeight = transform.position.y;
                float targetHeight = jumping.StartPos.y + config.JumpHeight;

                if (currentHeight < targetHeight)
                    continue;

                entity.DelJumping();
            }
        }

        private void Move(ProtoEntity entity, ProtoEntity inputEntity)
        {
            CharacterController characterController = entity.GetCharacterController().Value;
            CharacterConfig config = entity.GetCharacterConfig().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;
            
            //jump
            direction.y += config.JumpPower * Time.deltaTime;
            
            //Форвард
            Transform transform = entity.GetTransform().Value;
            Vector3 forwardDirection = inputEntity.GetDirection().Value.normalized;
            transform.forward = forwardDirection == Vector3.zero 
                ? transform.forward
                : forwardDirection;
            
            characterController.Move(direction);
        }
    }
}