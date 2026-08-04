using System;
using Cysharp.Threading.Tasks;
using Fusion.Menu;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.NetworkCore.Services;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainMenuUiView : UiView
    {
        [Required] [SerializeField] private UiButton _quickGameButton;
        [SerializeField] private FusionMenuConfig _config;
        [field: SerializeField] public FusionMenuConnectArgs ConnectionArgs { get; private set; } = new();
        [field: Required] [field: SerializeField] public EntityLink PlayerNameUiLink { get; private set; }

        private NetworkStartGameService _startGameService;
        private ISceneService _sceneService;
        private IUiViewService _uiViewService;
        private IUiPopUpService _uiPopUpService;

        public event Action<FusionMenuConnectArgs> OnBeforeConnect;

        private void OnEnable() =>
            _quickGameButton.OnClick += StartQuickGame;

        private void OnDisable() =>
            _quickGameButton.OnClick -= StartQuickGame;

        private async void StartQuickGame()
        {
            _quickGameButton.Interactable(false);
            
            ConnectionArgs.Session = null;
            ConnectionArgs.Creating = false;
            ConnectionArgs.Region = ConnectionArgs.PreferredRegion;
            ConnectionArgs.MaxPlayerCount = 6;
            
            _uiViewService.Show(UiViewId.Loading);
            ConnectResult result = await _startGameService.ConnectAsync(ConnectionArgs, _config, IdsConst.Lobby);

            await HandleConnectionResult(result);
        }
        
        private async UniTask HandleConnectionResult(ConnectResult result) 
        {
            if (result.CustomResultHandling)
                return;
            
            if (result.Success) 
            {
                await UniTask.WaitUntil(() => NetworkRunnerProvider.Runner.IsRunning);
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                await _sceneService.ChangeSceneAsync(IdsConst.Gameplay);
            } 
            else if (result.FailReason != ConnectFailReason.ApplicationQuit) 
            {
                _uiPopUpService.Get<MainMenuUiPopUp>().SetMassage("Connection Failed");
            }
        }
        
        [Inject]
        private void Construct(
            IUiPopUpService uiPopUpService,
            IUiViewService uiViewService,
            NetworkStartGameService startGameService,
            ISceneService sceneService)
        {
            _uiPopUpService = uiPopUpService;
            _uiViewService = uiViewService;
            _startGameService = startGameService;
            _sceneService = sceneService;
        }
    }
}