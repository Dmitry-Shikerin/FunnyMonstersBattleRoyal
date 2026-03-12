using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class ShowDailyRewardViewUiAction : UiAction
    {
        private IUiViewService _uiViewService;
        
        public override UiActionId Id => UiActionId.ShowDailyRewardView;

        [Inject]
        private void Construct(IUiViewService uiViewService)
        {
            _uiViewService = uiViewService;
        }

        public override void Handle() =>
            _uiViewService.Show(UiViewId.DailyRewards);
    }
}