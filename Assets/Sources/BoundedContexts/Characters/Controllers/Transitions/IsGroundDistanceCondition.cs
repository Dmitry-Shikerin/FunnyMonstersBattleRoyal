using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsGroundDistanceCondition : ConditionTask
    {
        private CharacterConfig _config;
        private CharacterMovementViewComponent _movement;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<CharacterConfig>();
        }

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            return null;
        }

        protected override bool OnCheck()
        {
            return _movement.GroundDistance <= _config.EndAirDistance;
        }
    }
}