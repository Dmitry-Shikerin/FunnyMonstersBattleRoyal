using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Hud.Infrastructure.Services.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class DesktopExitUiView : UiView
    {
        [Required] [SerializeField] private UiButton _exitButton;
        
        private IQuitService _quitService;

        [Inject]
        private void Construct(IQuitService quitService)
        {
            _quitService = quitService;
        }
        
        private void OnEnable() =>
            _exitButton.AddOnClickListener(ExitGame);

        private void OnDisable() =>
            _exitButton.RemoveOnClickListener(ExitGame);

        private void ExitGame() =>
            _quitService.QuitApplication();
    }
}