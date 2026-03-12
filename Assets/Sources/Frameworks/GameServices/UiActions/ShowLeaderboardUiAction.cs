using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class ShowLeaderboardUiAction : UiAction
    {
        private ILeaderboardService _leaderboardService;
        private ISdkService _sdkService;
        private IUiViewService _uiViewService;

        public override UiActionId Id => UiActionId.ShowLeaderboard;

        [Inject]
        public void Construct(
            IUiViewService uiViewService,
            ISdkService sdkService,
            ILeaderboardService leaderboardService)
        {
            _uiViewService = uiViewService;
            _sdkService = sdkService;
            _leaderboardService = leaderboardService;
        }

        public override void Handle()
        {
            if (_sdkService.IsAccountAuthorized() == false)
            {
                _uiViewService.Show(UiViewId.Authorization);
                return;
            }

            _leaderboardService.Fill();
            _uiViewService.Show(UiViewId.LeaderBoard);
        }
    }
}