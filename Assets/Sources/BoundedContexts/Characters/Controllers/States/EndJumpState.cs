using DG.Tweening;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class EndJumpState : FSMState
    {
        private CharacterMovementViewComponent _movement;
        private AnimationViewComponent _animation;

        protected override void OnInit()
        {
            ViewComponentsLink link = graphBlackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            _animation = link.Get<AnimationViewComponent>();
        }
        
        protected override void OnEnter()
        {
            _animation.Play(AnimationName.EndJump);
            _animation.Play_Rpc((int)AnimationName.EndJump);
            DOVirtual.DelayedCall(0.25f, Finish);
        }

        protected override void OnExit()
        {
            _movement.IsAir = false;
            _movement.IsJumping = false;
        }
    }
}