using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Pauses;
using UnityEngine;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class PauseUiAction : UiAction
    {
        private IPauseService _pauseService;

        public override UiActionId Id => UiActionId.Pause;

        [Inject]
        private void Construct(IPauseService pauseService) =>
            _pauseService = pauseService;

        public override void Handle()
        {
            _pauseService.PauseGame();
        }
    }
}