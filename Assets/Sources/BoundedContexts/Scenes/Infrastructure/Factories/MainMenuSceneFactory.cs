using System;
using Cysharp.Threading.Tasks;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Controllers;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISdkService _sdkService;
        private readonly IStorageService _storageService;
        private readonly MainMenuRootGameObjects _mainMenuRootGameObjects;
        private readonly IAssetCollector _assetCollector;
        private readonly DiContainer _container;
        private readonly IEcsGameStartUp _ecsGameStartUp;
        private readonly ILeaderboardService _leaderboardService;
        private readonly ICompositeAssetService _compositeAssetService;
        private readonly ISoundService _soundService;
        private readonly IFocusService _focusService;
        private readonly ILocalizationService _localizationService;
        private readonly ICurtainView _curtainView;

        public MainMenuSceneFactory(
            IUiViewService uiViewService,
            ISdkService sdkService,
            IStorageService storageService,
            MainMenuRootGameObjects mainMenuRootGameObjects,
            IAssetCollector assetCollector,
            DiContainer container,
            IEcsGameStartUp ecsGameStartUp,
            ILeaderboardService leaderboardService,
            ICompositeAssetService compositeAssetService,
            ISoundService soundService,
            IFocusService focusService,
            ILocalizationService localizationService,
            ICurtainView curtainView)
        {
            _uiViewService = uiViewService;
            _sdkService = sdkService;
            _storageService = storageService;
            _mainMenuRootGameObjects = mainMenuRootGameObjects;
            _assetCollector = assetCollector;
            _container = container;
            _ecsGameStartUp = ecsGameStartUp;
            _leaderboardService = leaderboardService ?? 
                                            throw new ArgumentNullException(nameof(leaderboardService));
            _compositeAssetService = compositeAssetService ?? throw new ArgumentNullException(nameof(compositeAssetService));
            _soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _localizationService = localizationService ?? 
                                   throw new ArgumentNullException(nameof(localizationService));
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
        }
        
        public UniTask<IScene> Create(object payload)
        {
            IScene mainMenuScene = new MainMenuScene(
                _uiViewService,
                _sdkService,
                _storageService,
                _mainMenuRootGameObjects,
                _assetCollector,
                _container,
                _ecsGameStartUp,
                _leaderboardService,
                _compositeAssetService,
                _soundService,
                _focusService,
                _localizationService,
                _curtainView);
            
            return UniTask.FromResult(mainMenuScene);
        }
    }
}