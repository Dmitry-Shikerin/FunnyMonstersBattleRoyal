using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsGroundedCondition : ConditionTask
    {
        private CharacterMovementViewComponent _movement;

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            return null;
        }

        protected override bool OnCheck() =>
            _movement.IsGrounded;
    }
}