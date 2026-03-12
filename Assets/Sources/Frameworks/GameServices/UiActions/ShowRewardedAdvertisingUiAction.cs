using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class ShowRewardedAdvertisingUiAction : UiAction
    {
        private ISdkService _sdkService;
        public override UiActionId Id => UiActionId.ShowRewardedAdvertising;

        [Inject]
        private void Construct(ISdkService sdkService) =>
            _sdkService = sdkService;

        public override void Handle() => 
            _sdkService.ShowRewardedAdv();

    }
}