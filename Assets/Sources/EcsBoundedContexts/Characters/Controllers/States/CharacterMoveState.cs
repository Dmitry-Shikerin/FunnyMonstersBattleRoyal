using System;
using Animancer;
using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Presentation.Network;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterMoveState : FSMState
    {
        private ProtoEntity _entity;
        private IEntityRepository _entityRepository;
        private LinearMixerState _state;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnEnter()
        {
            var config = _entity.GetCharacterConfig().Value;
            _entity.ReplaceGravity(config.IdleGravity);
            AnimancerState state = _entity.PlayAnimation(AnimationName.Walk);

            if (state is not LinearMixerState linearMixerState)
                throw new InvalidOperationException();

            _state = linearMixerState;
            
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.Walk);
        }

        protected override void OnUpdate()
        {
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            float currentSpeed = _entity.GetSpeed().Value;
            _state.Parameter = Normalize(currentSpeed, 0, config.Speed);
            
            CharacterController characterController = _entity.GetCharacterController().Value;
            float speed = _entity.GetSpeed().Value;
            Vector3 direction = _entity.GetDirection().Value.normalized * speed * Time.deltaTime;
            //гравитация
            direction.y = _entity.GetGravity().Value;
            
            characterController.Move(direction);
        }

        private float Normalize(float value, float min, float max)
        {
            float result = (value - min) / (max - min);
            return Mathf.Clamp01(result);
        }
    }
}