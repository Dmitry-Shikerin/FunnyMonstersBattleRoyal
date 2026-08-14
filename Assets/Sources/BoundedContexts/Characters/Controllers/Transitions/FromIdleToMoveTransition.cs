using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class FromIdleToMoveTransition : ConditionTask
    {
        private CharacterMovementViewComponent _movement;
        private InputReceiverViewComponent _input;

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            _input = link.Get<InputReceiverViewComponent>();
            return null;
        }

        protected override bool OnCheck()
        {
            if (_movement.IsGrounded == false)
                return false;
            
            if (_input.MovementDirection == Vector3.zero)
                return false;

            return _movement.CharacterSpeed > 0;
        }
    }
}