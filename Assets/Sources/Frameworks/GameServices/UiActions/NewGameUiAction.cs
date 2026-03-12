using MyDependencies.Sources.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class NewGameUiAction : UiAction
    {
        private IStorageService _storageService;
        private ISceneService _sceneService;
        private IUiViewService _uiViewService;

        public override UiActionId Id => UiActionId.NewGame;

        [Inject]
        private void Construct(
            IUiViewService uiViewService,
            IStorageService storageService,
            ISceneService sceneService)
        {
            _uiViewService = uiViewService;
            _storageService = storageService;
            _sceneService = sceneService;
        }

        public override void Handle()
        {
            if (_storageService.HasKey(IdsConst.PlayerWallet))
            {
                _uiViewService.Show(UiViewId.WarningNewGame);
                return;
            }
            
            _sceneService.ChangeSceneAsync(IdsConst.Gameplay);
        }
    }
}