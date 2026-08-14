using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class ToIdleTransition : ConditionTask
    {
        private CharacterMovementViewComponent _movement;

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            return null;
        }

        protected override bool OnCheck()
        {
            if (_movement.IsGrounded == false)
                return false;
            
            if (_movement.CharacterDirection == Vector3.zero)
                return true;

            return _movement.CharacterSpeed == 0;
        }
    }
}