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
    public class CharacterJumpState : FSMState
    {
        private CharacterConfig _config;
        private CharacterMovementViewComponent _movement;
        private AnimationViewComponent _animation;
        private InputReceiverViewComponent _input;
        private NetworkRunner _runner;

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
            _input = link.Get<InputReceiverViewComponent>();
            _runner = _movement.Runner;
        }
        
        protected override void OnEnter()
        {
            _animation.Play(AnimationName.StartJump);
            _animation.Play_Rpc((int)AnimationName.StartJump);
            _movement.IsJumping = true;
            _movement.JumpTimer = _config.ChangeJumpGravityDuration;
            _movement.JumpImpulseDirection = _input.MovementDirection;
            DOVirtual
                .Float(
                    _movement.Gravity,
                    _config.JumpGravity,
                    _config.ChangeJumpGravityDuration,
                    value => _movement.Gravity = value)
                .SetEase(_config.ChangeJumpGravityEase);
        }

        protected override void OnUpdate()
        {
            _movement.JumpTimer -= _runner.DeltaTime;
            
            Move();
            
            if (_movement.JumpTimer > 0)
                return;

            _movement.IsJumping = false;
            Finish();
        }

        protected override void OnExit()
        {
            _movement.JumpImpulseDirection = Vector3.zero;
        }

        private void Move()
        {
            Vector3 direction = _movement.JumpImpulseDirection * (_config.JumpForwardPower * _runner.DeltaTime);
            //jump
            direction.y = _movement.Gravity;

            _movement.CharacterController.Move(direction);
        }
    }
}