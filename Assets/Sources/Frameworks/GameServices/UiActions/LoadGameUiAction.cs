using MyDependencies.Sources.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Scenes.Domain.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class LoadGameUiAction : UiAction
    {
        private ISceneService _sceneService;

        public override UiActionId Id => UiActionId.LoadGame;

        [Inject]
        private void Construct(ISceneService sceneService) =>
            _sceneService = sceneService;

        public override void Handle() => 
            _sceneService.ChangeSceneAsync(
                IdsConst.Gameplay, new ScenePayload(IdsConst.Gameplay, true, false));
    }
}