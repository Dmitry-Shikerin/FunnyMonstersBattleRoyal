using DG.Tweening;
using Fusion;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterFallState : FSMState
    {
        private CharacterMovementViewComponent _movement;
        private AnimationViewComponent _animation;
        private CharacterConfig _config;
        private NetworkRunner _runner;
        private InputReceiverViewComponent _inputReceiver;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<CharacterConfig>();
        }
        
        protected override void OnInit()
        {
            ViewComponentsLink link = graphBlackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            _animation = link.Get<AnimationViewComponent>();
            _inputReceiver = link.Get<InputReceiverViewComponent>();
            _runner = _movement.Runner;
        }
        
        protected override void OnEnter()
        {
            _animation.Play(AnimationName.AirJump);
            _animation.Play_Rpc((int)AnimationName.AirJump);
            
            _movement.IsAir = true;
            
            DOVirtual
                .Float(
                    _movement.Gravity,
                    _config.FallGravity,
                    _config.ChangeFallGravityDuration,
                    value => _movement.Gravity = value)
                .SetEase(_config.ChangeFallGravityEase);
        }
        
        protected override void OnUpdate()
        {
            Vector3 direction = _inputReceiver.MovementDirection * (_config.Speed * _runner.DeltaTime);
            //Gravity
            direction.y = _movement.Gravity;

            _movement.CharacterController.Move(direction);
        }

        protected override void OnExit()
        {
            _movement.IsAir = false;
            //TODO обратить вниманиее
            _movement.CharacterSpeed = 10;
        }
    }
}