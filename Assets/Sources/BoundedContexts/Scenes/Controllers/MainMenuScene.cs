using Cysharp.Threading.Tasks;
using NodeCanvas.StateMachines;
using Reflex.Core;
using Reflex.Injectors;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Curtains;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Domain.Interfaces;
using Sources.Frameworks.GameServices.UiActions;
using Sources.Frameworks.GameServices.UiReflexInjectors;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;

namespace Sources.BoundedContexts.Scenes.Controllers
{
    public class MainMenuScene : IScene
    {
        private readonly UiReflexInjector _uiReflexInjector;
        private readonly IUiViewService _uiViewService;
        private readonly ISdkService _sdkService;
        private readonly IStorageService _storageService;
        private readonly MainMenuRootGameObjects _mainMenuRootGameObjects;
        private readonly IAssetCollector _assetCollector;
        private readonly Container _container;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ILeaderboardService _leaderboardService;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISoundService _soundService;
        private readonly IFocusService _focusService;
        private readonly ILocalizationService _localizationService;
        private readonly ICurtainService _curtainService;

        public MainMenuScene(
            UiReflexInjector uiReflexInjector,
            IUiViewService uiViewService,
            ISdkService sdkService,
            IStorageService storageService,
            MainMenuRootGameObjects mainMenuRootGameObjects,
            IAssetCollector assetCollector,
            Container container,
            IEcsGameStartUp ecsGameStartUp,
            ILeaderboardService leaderboardService,
            ICompositeAssetService compositeAssetService,
            ISoundService soundService,
            IFocusService focusService,
            ILocalizationService localizationService,
            ICurtainService curtainService)
        {
            _uiReflexInjector = uiReflexInjector;
            _uiViewService = uiViewService;
            _sdkService = sdkService;
            _storageService = storageService;
            _mainMenuRootGameObjects = mainMenuRootGameObjects;
            _assetCollector = assetCollector;
            _container = container;
            _ecsGameStartUp = ecsGameStartUp;
            _leaderboardService = leaderboardService;
            _compositeAssetService = compositeAssetService;
            _soundService = soundService;
            _focusService = focusService;
            _localizationService = localizationService;
            _curtainService = curtainService;
        }

        public async void Enter(object payload = null)
        {
            //await InitializeAsync((IScenePayload)payload);
            await _compositeAssetService.LoadAsync(ResourcesPrefabPath.ResourcesAssetsConfig, AddressablesPrefabPath.AddressablesAssetConfig);
            _focusService.Initialize();
            InitUiActions();
            InitDeepUiBrain();
            _uiReflexInjector.InjectUiViews();
            ActivateLoadGameButton();
            _localizationService.Translate();
            AttributeInjector.Inject(_ecsGameStartUp, _container);
            await _ecsGameStartUp.Initialize();
            _sdkService.Initialize();
            //_leaderboardService.Initialize();
            //_soundService.Initialize();
            HideBlackScreen();
            await _curtainService.HideAsync();
            //_soundService.Play(SoundDatabaseName.Music, SoundName.MainMenuBackgroundMusic);
            // await GameReady((IScenePayload)payload);
        }

        public void Exit()
        {
            //_soundService.Stop(SoundName.MainMenuBackgroundMusic);
            _ecsGameStartUp.Destroy();
            _leaderboardService.Destroy();
            _sdkService.Destroy();
            _soundService.Destroy();
            _focusService.Destroy();
            DeepUiBrain.Instance?.Destroy();
        }

        public void Update(float deltaTime)
        {
            _ecsGameStartUp.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }
        
        private void ActivateLoadGameButton()
        {
            // MainHudUiView view = _uiViewService.Get<MainHudUiView>();
            //
            // if (_storageService.HasKey(IdsConst.PlayerWallet))
            // {
            //     view.LoadGameButton.gameObject.SetActive(true);
            //     return;
            // }
            //
            // view.LoadGameButton.gameObject.SetActive(false);
        }
        
        private void HideBlackScreen()
        {
            _mainMenuRootGameObjects.BlackScreen.SetActive(false);
        }

        // private async UniTask InitializeAsync(IScenePayload payload)
        // {
        //     if (payload == null)
        //         return;
        //
        //     if (payload.CanFromGameplay)
        //         return;
        //     
        //     _sdkService.EnableCallbackLogging();
        //     await _sdkService.InitializeSdk();
        // }

        private UniTask GameReady(IScenePayload payload)
        {
            if (payload == null)
                return UniTask.CompletedTask;
            
            if (payload.CanFromGameplay)
                return UniTask.CompletedTask;

            _sdkService.ShowSticky();
            _sdkService.GameReady();

            return UniTask.CompletedTask;
        }

        private void InitDeepUiBrain()
        {
            UiConfig hudConfig = _assetCollector.Get<UiConfig>();
            Camera mainCamera = _mainMenuRootGameObjects.MainCamera;
            DeepUiBrain.Instance.Initialize(hudConfig.MainMenuUiConfig, mainCamera, _container);
            FSMOwner fsmOwner = DeepUiBrain.Hud.MainMenuFsmOwner;
            FSM behaviour = fsmOwner.behaviour;
            behaviour.Initialize(behaviour.agent, behaviour.blackboard, true, false);
            fsmOwner.InjectOwner(_container);
            fsmOwner.StartBehaviour();
        }

        private void InitUiActions()
        {
            UiActionHandler actionHandler = DeepUiBrain.ActionHandler;
            //UiActions
            actionHandler.AddAction<UnPauseUiAction>();
            actionHandler.AddAction<ShowRewardedAdvertisingUiAction>();
            actionHandler.AddAction<ClearSavesUiAction>();
            actionHandler.AddAction<NewGameUiAction>();
            actionHandler.AddAction<ShowLeaderboardUiAction>();
            actionHandler.AddAction<SaveVolumeUiAction>();
            actionHandler.AddAction<ShowDailyRewardViewUiAction>();
            actionHandler.AddAction<PlayerAccountAuthorizeUiAction>();
            actionHandler.AddAction<LoadGameUiAction>();
            actionHandler.AddAction<GetDailyRewardUiAction>();
        }
    }
}