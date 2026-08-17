using System;
using NodeCanvas.StateMachines;
using Reflex.Core;
using Reflex.Injectors;
using Sources.BoundedContexts.Hud.Presentations.Lobby;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Curtains;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.Frameworks.GameServices.UiActions;
using Sources.Frameworks.GameServices.UiReflexInjectors;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.BoundedContexts.Scenes.Controllers
{
    public class GameplayScene : IScene
    {
        private readonly IUiViewService _uiViewService;
        private readonly JoinManager _joinManager;
        private readonly ISceneService _sceneService;
        private readonly IInputService _inputService;
        private readonly UiReflexInjector _uiReflexInjector;
        private readonly ISdkService _sdkService;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _entityRepository;
        private readonly Container _container;
        private readonly RootGameObject _rootGameObject;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISoundService _soundService;
        private readonly IFocusService _focusService;
        private readonly ILocalizationService _localizationService;
        private readonly ICurtainService _curtainService;
        private readonly IUpdateService _updateService;
        private IEcsGameStartUp _ecsGameStartUp;
        private bool _isLoaded;

        public GameplayScene(
            IUiViewService uiViewService,
            JoinManager joinManager,
            ISceneService sceneService,
            IInputService inputService,
            UiReflexInjector uiReflexInjector,
            ISdkService sdkService,
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            Container container,
            RootGameObject rootGameObject,
            ICompositeAssetService compositeAssetService,
            ISoundService soundService,
            IFocusService focusService,
            ILocalizationService localizationService,
            ICurtainService curtainService,
            IUpdateService updateService)
        {
            _uiViewService = uiViewService;
            _joinManager = joinManager;
            _sceneService = sceneService;
            _inputService = inputService;
            _uiReflexInjector = uiReflexInjector;
            _sdkService = sdkService;
            _assetCollector = assetCollector;
            _entityRepository = entityRepository;
            _container = container;
            _rootGameObject = rootGameObject;
            _compositeAssetService = compositeAssetService ?? throw new ArgumentNullException(nameof(compositeAssetService));
            _soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _localizationService = localizationService ?? 
                                   throw new ArgumentNullException(nameof(localizationService));
            _curtainService = curtainService ?? throw new ArgumentNullException(nameof(curtainService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public async void Enter(object payload = null)
        {
            //_inputService.Initialize();
            _focusService.Initialize();
            await _compositeAssetService.LoadAsync(ResourcesPrefabPath.ResourcesAssetsConfig, AddressablesPrefabPath.AddressablesAssetConfig);
            ColliderExt.Construct(_entityRepository);
            AnimancerExtension.Construct(_assetCollector);
            InitUiActions();
            InitDeepUiBrain();
            _uiReflexInjector.InjectUiViews();
            _localizationService.Translate();
            _ecsGameStartUp = NetworkRunnerProvider.LeoGameStartUp;
            AttributeInjector.Inject(_ecsGameStartUp, _container);
            await _ecsGameStartUp.Initialize();
            _sdkService.Initialize();
            _soundService.Initialize();
            _isLoaded = true;
            //_soundService.Play(SoundDatabaseName.Music, SoundName.GameplayBackgroundMusic);
            _joinManager.Initialize();

            if (_sceneService.CurrentSceneName == IdsConst.Lobby)
                _uiViewService.Get<LobbyUiView>().PlayersReadyInitializerUiView.Initialize();
            //await _curtainView.HideAsync();
        }

        public void Exit()
        {
            //_inputService.Destroy();
            //_soundService.Stop(SoundName.GameplayBackgroundMusic);
            _soundService.Destroy();
            _ecsGameStartUp.Destroy();
            _sdkService.Destroy();
            _compositeAssetService.Release();
            _focusService.Destroy();
            DeepUiBrain.Instance?.Destroy();
        }

        public void Update(float deltaTime)
        {
            if (_isLoaded == false)
                return;
            
            _updateService.Update(deltaTime);
            //_ecsGameStartUp.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void InitDeepUiBrain()
        {
            UiConfig hudConfig = _assetCollector.Get<UiConfig>();
            //что бы камера не была пустой
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;
            //TODO возможно разделить на отдельную сцену
            UiManagerConfig managerConfig = _sceneService.CurrentSceneName == IdsConst.Gameplay
                ? hudConfig.GameUiConfig
                : hudConfig.LobbyUiConfig;
            //Camera mainCamera = _rootGameObject.MainCamera.GetModule<MainCameraModule>().Camera;
            DeepUiBrain.Instance.Initialize(managerConfig, mainCamera, _container);
            FSMOwner fsmOwner = _sceneService.CurrentSceneName == IdsConst.Gameplay ?
                DeepUiBrain.Hud.GameplayFsmOwner :
                DeepUiBrain.Hud.LobbyFsmOwner;
            FSM behaviour = fsmOwner.behaviour;
            behaviour.Initialize(behaviour.agent, behaviour.blackboard, true, false);
            fsmOwner.InjectOwner(_container);
            fsmOwner.StartBehaviour();
        }

        private void InitUiActions()
        {
            UiActionHandler actionHandler = DeepUiBrain.ActionHandler;
            //Buttons
            actionHandler.AddAction<CompleteTutorialUiAction>();
            actionHandler.AddAction<LoadMainMenuSceneUiAction>();
            actionHandler.AddAction<UnPauseUiAction>();
            actionHandler.AddAction<PauseUiAction>();
            actionHandler.AddAction<ShowRewardedAdvertisingUiAction>();
            actionHandler.AddAction<ShowDailyRewardViewUiAction>();
            actionHandler.AddAction<ClearSavesUiAction>();
            actionHandler.AddAction<SaveVolumeUiAction>();
            actionHandler.AddAction<NewGameUiAction>();
            actionHandler.AddAction<GetDailyRewardUiAction>();
            actionHandler.AddAction<LockCursorUiAction>();
            actionHandler.AddAction<UnlockCursorUiAction>();
        }
    }
}