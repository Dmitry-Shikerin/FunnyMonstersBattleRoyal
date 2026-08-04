using DG.Tweening;
using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Presentation.Network;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterJumpState : FSMState
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.StartJump);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.StartJump);
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            _entity.AddJumping(config.ChangeJumpGravityDuration, 0, _entity.GetTransform().Value.position);
            _entity.ReplaceTargetGravity(config.JumpGravity);
            Vector3 direction = _entity.GetInputEntity().Value.GetDirection().Value;
            _entity.AddJumpImpulseDirection(direction);
            DOVirtual
                .Float(
                    _entity.GetGravity().Value,
                    config.JumpGravity,
                    config.ChangeJumpGravityDuration,
                    value => _entity.ReplaceGravity(value))
                .SetEase(config.ChangeJumpGravityEase);
        }

        protected override void OnUpdate()
        {
            ref JumpingComponent jumping = ref _entity.GetJumping();
            
            jumping.JumpTimer -= Time.deltaTime;
            
            Move(_entity);
            
            if (jumping.JumpTimer > 0)
                return;
            
            _entity.DelJumping();
            Finish();
        }

        protected override void OnExit()
        {
            _entity.DelJumpImpulseDirection();
        }

        private void Move(ProtoEntity entity)
        {
            CharacterController characterController = _entity.GetCharacterController().Value;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            Vector3 direction = _entity.GetJumpImpulseDirection().Value * (config.JumpForwardPower * Time.deltaTime);

            //jump
            direction.y = _entity.GetGravity().Value;

            characterController.Move(direction);
        }
    }
}