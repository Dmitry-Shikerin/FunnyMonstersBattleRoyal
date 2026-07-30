using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Presentation.Network;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterStartJumpState : FSMState
    {
        private ProtoEntity _entity;
        private ISignalBus _signalBus;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(ISignalBus signalBus) =>
            _signalBus = signalBus;
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.StartJump);
            //_entity.PlayAnimation(AnimationName.StartJump).SetCallback(AnimationEventName.End, Finish);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.StartJump);
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            _entity.AddJumping(config.JumpDuration, 0, _entity.GetTransform().Value.position);
            _entity.ReplaceTargetGravity(config.JumpPower);
        }

        protected override void OnUpdate()
        {
            if (_entity.HasJumping() == false)
                return;
            
            ref JumpingComponent jumping = ref _entity.GetJumping();

            jumping.JumpTimer -= Time.deltaTime;

            //Move
            Move(_entity);

            if (jumping.JumpTimer > 0)
                return;

            _entity.DelJumping();
            Finish();
        }
        
        private void Move(ProtoEntity entity)
        {
            ProtoEntity inputEntity = _entity.GetInputEntity().Value;
            CharacterController characterController = entity.GetCharacterController().Value;
            CharacterConfig config = entity.GetCharacterConfig().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;

            //jump
            direction.y = _entity.GetGravity().Value;

            characterController.Move(direction);
        }
    }
}