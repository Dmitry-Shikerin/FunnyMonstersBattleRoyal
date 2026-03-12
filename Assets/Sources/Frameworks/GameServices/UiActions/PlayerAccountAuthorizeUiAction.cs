using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class PlayerAccountAuthorizeUiAction : UiAction
    {
        private ISdkService _sdkService;
        private IUiViewService _uiViewService;

        public override UiActionId Id => UiActionId.PlayerAccountAuthorize;

        [Inject]
        private void Construct(
            ISdkService sdkService,
            IUiViewService uiViewService)
        {
            _sdkService = sdkService;
            _uiViewService = uiViewService;
        }

        public override void Handle()
        {
            _sdkService.AuthorizePlayerAccount();
        }
    }
}