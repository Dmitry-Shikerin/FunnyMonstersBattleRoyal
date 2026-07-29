using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterJumpAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            if (_entity.HasJumping() == false)
                return;
            
            ref JumpingComponent jumping = ref _entity.GetJumping();
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            Transform transform = _entity.GetTransform().Value;

            jumping.JumpTimer -= Time.deltaTime;

            //Move
            ProtoEntity inputEntity = _entity.GetInputEntity().Value;
            Move(_entity, inputEntity);

            // Завершение прыжка
            float currentHeight = transform.position.y;
            float targetHeight = jumping.StartPos.y + config.JumpHeight;

            if (currentHeight < targetHeight)
                return;

            _entity.DelJumping();
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