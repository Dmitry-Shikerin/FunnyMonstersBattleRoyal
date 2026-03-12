using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Pauses;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class UnPauseUiAction : UiAction
    {
        private IPauseService _pauseService;

        public override UiActionId Id => UiActionId.UnPause;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public override void Handle()
        {
            if (_pauseService.IsPaused == false)
                return;

            _pauseService.ContinueGame();
        }
    }
}