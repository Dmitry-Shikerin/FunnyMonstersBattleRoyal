using DG.Tweening;
using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterFallState : FSMState
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.AirJump);
            _entity.GetCharacterModule().Value.EcsNetworkAnimationView
                .PlayAnimation_Rpc((int)AnimationName.AirJump);
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            _entity.ReplaceTargetGravity(config.FallGravity);
            _entity.AddAir();
            DOVirtual.Float(
                    _entity.GetGravity().Value,
                    config.FallGravity,
                    config.ChangeFallGravityDuration,
                    value => _entity.ReplaceGravity(value))
                .SetEase(config.ChangeFallGravityEase);
        }
        
        protected override void OnUpdate()
        {
            CharacterController characterController = _entity.GetCharacterController().Value;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            ProtoEntity inputEntity = _entity.GetInputEntity().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;

            //Gravity
            direction.y = _entity.GetGravity().Value;
            //Debug.Log($"Direction {direction}");

            //characterController.Move(direction);
            _entity.GetTransform().Value.Translate(direction);
        }

        protected override void OnExit()
        {
            _entity.DelAir();
            _entity.ReplaceSpeed(10);
        }
    }
}