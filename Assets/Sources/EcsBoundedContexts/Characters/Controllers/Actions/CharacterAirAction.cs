using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterAirAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            //Todo доработать эту логику
            if (_entity.HasAir() == false)
                return;

            if (_entity.HasJumping())
                return;
            
            CharacterController characterController = _entity.GetCharacterController().Value;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            ProtoEntity inputEntity = _entity.GetInputEntity().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;

            //Gravity
            direction.y -= config.JumpPower * Time.deltaTime;

            //Форвард
            Transform transform = _entity.GetTransform().Value;
            transform.forward = inputEntity.GetDirection().Value.normalized;

            characterController.Move(direction);
        }
    }
}