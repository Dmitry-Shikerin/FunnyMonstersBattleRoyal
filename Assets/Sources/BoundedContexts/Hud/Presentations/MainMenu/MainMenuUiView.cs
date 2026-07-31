using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.NetworkCore.Services;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainMenuUiView : UiView
    {
        [Required] [SerializeField] private UiButton _quickGameButton;
        
        [field: Required] [field: SerializeField] public EntityLink PlayerNameUiLink { get; private set; }
        
        private NetworkStartGameService _startGameService;
        private ISceneService _sceneService;

        private void OnEnable() =>
            _quickGameButton.OnClick += StartQuickGame;

        private void OnDisable() =>
            _quickGameButton.OnClick -= StartQuickGame;

        private async void StartQuickGame()
        {
            _quickGameButton.Interactable(false);
            //Доработать логику назвавния катки
            await _startGameService.StartSimulationAsync(GameMode.AutoHostOrClient, "SampleSession");
            //Иначе загружает мейн меню сцену
            await UniTask.WaitUntil(() => NetworkRunnerProvider.Runner.IsRunning);
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await _sceneService.ChangeSceneAsync(IdsConst.Gameplay);
        }

        [Inject]
        private void Construct(
            NetworkStartGameService startGameService,
            ISceneService sceneService)
        {
            _startGameService = startGameService;
            _sceneService = sceneService;
        }
    }
}