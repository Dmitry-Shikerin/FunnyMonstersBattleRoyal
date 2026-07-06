using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class ConnectUiView : UiView
    {
        [SerializeField] private UiButton _button;
        
        private IUiViewService _uiViewService;

        [Inject]
        private void Construct(IUiViewService uiViewService) =>
            _uiViewService = uiViewService;

        private void OnEnable() =>
            _button.OnClick += Connect;

        private void OnDisable() =>
            _button.OnClick -= Connect;

        private void Connect() =>
            _uiViewService.Get<MainHudUiView>().LobbyView.Connect();
    }
}