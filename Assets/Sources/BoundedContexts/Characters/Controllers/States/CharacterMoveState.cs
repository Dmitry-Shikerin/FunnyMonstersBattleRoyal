using System;
using Animancer;
using Fusion;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterMoveState : FSMState
    {
        private CharacterMovementViewComponent _movement;
        private CharacterConfig _config;
        private AnimationViewComponent _animation;
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
            _runner = _movement.Runner;
        }

        protected override void OnEnter()
        {
            _movement.Gravity = _config.IdleGravity;
            _animation.PlayAnim(AnimationName.Walk);
        }

        protected override void OnUpdate()
        {
            _animation.AnimationSpeed = _movement.CharacterSpeed.Normalize(0, _config.Speed);
            Vector3 direction = _movement.CharacterDirection.normalized * (_movement.CharacterSpeed * _runner.DeltaTime);
            //гравитация
            direction.y = _movement.Gravity;
            
            _movement.CharacterController.Move(direction);
        }
    }
}