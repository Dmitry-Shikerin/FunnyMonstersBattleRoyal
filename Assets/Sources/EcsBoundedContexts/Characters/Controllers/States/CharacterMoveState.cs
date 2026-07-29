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
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterMoveState : FSMState
    {
        private ProtoEntity _entity;
        private IEntityRepository _entityRepository;
        private ISignalBus _signalBus;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(ISignalBus signalBus) =>
            _signalBus = signalBus;

        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.Walk);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.Walk);
        }

        protected override void OnUpdate()
        {
            CharacterController characterController = _entity.GetCharacterController().Value;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            float speed = _entity.GetSpeed().Value;
            Vector3 direction = _entity.GetDirection().Value.normalized * speed * Time.deltaTime;
            //гравитация
            direction.y = config.GroundedGravity;
            
            characterController.Move(direction);
        }

        private void ChangeSpeed(CharacterConfig config)
        {
            ref SpeedComponent speed = ref _entity.GetSpeed();
            Vector3 input = _entity.GetInputEntity().Value.GetDirection().Value;
            
            if (input == Vector3.zero)
            {
                if (speed.Value > 0)
                {
                    speed.Value -= config.SpeedChangeDelta;
                }
            }
            else
            {
                if (speed.Value < config.Speed)
                {
                    speed.Value += config.SpeedChangeDelta;
                }
            }
        }
    }
}