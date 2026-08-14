using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsJumpCondition : ConditionTask
    {
        private InputReceiverViewComponent _input;

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _input = link.Get<InputReceiverViewComponent>();
            return null;
        }
        
        protected override void OnEnable() =>
            _input.OnJump += OnJump;

        protected override void OnDisable() =>
            _input.OnJump -= OnJump;

        private void OnJump() =>
            YieldReturn(true);

        protected override bool OnCheck() =>
            false;
    }
}