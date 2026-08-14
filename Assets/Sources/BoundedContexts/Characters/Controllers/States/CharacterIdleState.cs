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

namespace Sources.BoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterIdleState : FSMState
    {
        private CharacterConfig _config;
        private CharacterMovementViewComponent _movement;
        private AnimationViewComponent _animation;

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
        }

        protected override void OnEnter()
        {
            _animation.Play(AnimationName.Idle);
            _animation.Play_Rpc((int)AnimationName.Idle);
            _movement.CharacterSpeed = 0;
            _movement.Gravity = _config.IdleGravity;
        }
    }
}