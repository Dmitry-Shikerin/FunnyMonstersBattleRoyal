using System;
using Fusion.Menu;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.BoundedContexts.Players.Presentation.Ui;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainMenuUiView : UiView
    {
        [Required] [SerializeField] private UiButton _quickGameButton;
        [field: Required] [field: SerializeField] public GameplayPlayerNameUiView PlayerNameUiView { get; private set; }
        [field: Required] [field: SerializeField] public FusionMenuConnectArgs ConnectArgs { get; private set; }

        private NetworkStartGameService _startGameService;

        public event Action<FusionMenuConnectArgs> OnBeforeConnect;

        [Inject]
        private void Construct(NetworkStartGameService startGameService) =>
            _startGameService = startGameService;

        private void OnEnable() =>
            _quickGameButton.OnClick += StartQuickGame;

        private void OnDisable() =>
            _quickGameButton.OnClick -= StartQuickGame;

        public void EnableQuickGameButton() =>
            _quickGameButton.Interactable(true);

        private async void StartQuickGame()
        {
            _quickGameButton.Interactable(false);
            await _startGameService.StartGameAsync(ConnectArgs, null, false);
        }
    }
}