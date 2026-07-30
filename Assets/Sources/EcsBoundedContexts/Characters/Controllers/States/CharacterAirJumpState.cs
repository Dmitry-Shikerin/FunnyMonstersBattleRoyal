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
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterAirJumpState : FSMState
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;
        
        protected override void OnEnter()
        {
            //_entity.PlayAnimation(AnimationName.AirJump).SetCallback(AnimationEventName.End, Finish);
            _entity.PlayAnimation(AnimationName.AirJump);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.AirJump);
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            _entity.ReplaceTargetGravity(config.Gravity);
            _entity.AddAir();
        }
        
        protected override void OnUpdate()
        {
            CharacterController characterController = _entity.GetCharacterController().Value;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            ProtoEntity inputEntity = _entity.GetInputEntity().Value;
            Vector3 direction = inputEntity.GetDirection().Value * config.Speed * Time.deltaTime;

            //Gravity
            direction.y = _entity.GetGravity().Value;

            characterController.Move(direction);
        }

        protected override void OnExit()
        {
            _entity.DelAir();
            _entity.ReplaceSpeed(10);
        }
    }
}