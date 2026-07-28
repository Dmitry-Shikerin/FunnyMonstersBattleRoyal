using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks.Infrastructure.Services;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu.Logic
{
    public class QuickGameView : MonoBehaviour
    {
        [Required] [SerializeField] private UiButton _button;
        
        private NetworkStartGameService _startGameService;
        private ISceneService _sceneService;

        private void OnEnable() =>
            _button.OnClick += StartQuickGame;

        private void OnDisable() =>
            _button.OnClick -= StartQuickGame;

        private async void StartQuickGame()
        {
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