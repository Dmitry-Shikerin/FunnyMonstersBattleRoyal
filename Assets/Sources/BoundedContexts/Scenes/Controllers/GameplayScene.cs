using System;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UiActions;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;

namespace Sources.BoundedContexts.Scenes.Controllers
{
    public class GameplayScene : IScene
    {
        private readonly ISdkService _sdkService;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _entityRepository;
        private readonly DiContainer _container;
        private readonly RootGameObject _rootGameObject;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISoundService _soundService;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly IFocusService _focusService;
        private readonly ILocalizationService _localizationService;
        private readonly ICurtainView _curtainView;
        private readonly ICameraService _cameraService;
        private readonly IUpdateService _updateService;

        public GameplayScene(
            ISdkService sdkService,
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            DiContainer container,
            RootGameObject rootGameObject,
            ICompositeAssetService compositeAssetService,
            ISoundService soundService,
            IEcsGameStartUp ecsGameStartUp,
            IFocusService focusService,
            ILocalizationService localizationService,
            ICurtainView curtainView,
            ICameraService cameraService,
            IUpdateService updateService)
        {
            _sdkService = sdkService;
            _assetCollector = assetCollector;
            _entityRepository = entityRepository;
            _container = container;
            _rootGameObject = rootGameObject;
            _compositeAssetService = compositeAssetService ?? throw new ArgumentNullException(nameof(compositeAssetService));
            _soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
            _ecsGameStartUp = ecsGameStartUp ?? throw new ArgumentNullException(nameof(ecsGameStartUp));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _localizationService = localizationService ?? 
                                   throw new ArgumentNullException(nameof(localizationService));
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public async void Enter(object payload = null)
        {
            _focusService.Initialize();
            await _compositeAssetService.LoadAsync(ResourcesPrefabPath.ResourcesAssetsConfig, AddressablesPrefabPath.AddressablesAssetConfig);
            ColliderExt.Construct(_entityRepository);
            AnimancerExtension.Construct(_assetCollector);
            InitUiActions();
            InitDeepUiBrain();
            _localizationService.Translate();
            _ecsGameStartUp.Initialize();
            _sdkService.Initialize();
            _soundService.Initialize();
            _soundService.Play(SoundDatabaseName.Music, SoundName.GameplayBackgroundMusic);
            await _curtainView.HideAsync();
        }

        public void Exit()
        {
            _soundService.Stop(SoundName.GameplayBackgroundMusic);
            _soundService.Destroy();
            _ecsGameStartUp.Destroy();
            _sdkService.Destroy();
            _compositeAssetService.Release();
            _focusService.Destroy();
            DeepUiBrain.Instance?.Destroy();
        }

        public void Update(float deltaTime)
        {
            _updateService.Update(deltaTime);
            _ecsGameStartUp.Update(deltaTime);
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
            Camera mainCamera = _rootGameObject.MainCamera;
            DeepUiBrain.Instance.Initialize(hudConfig.GameUiConfig, mainCamera, _container);
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
        }
    }
}