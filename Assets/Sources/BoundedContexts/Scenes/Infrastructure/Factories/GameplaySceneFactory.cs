using System;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Controllers;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.Frameworks.GameServices.DeepWrappers.Curtains;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.Frameworks.GameServices.UiReflexInjectors;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories
{
    public class GameplaySceneFactory : ISceneFactory
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
        private readonly ICameraService _cameraService;
        private readonly IUpdateService _updateService;

        public GameplaySceneFactory(
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
            ICameraService cameraService,
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
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
            _compositeAssetService = compositeAssetService ?? 
                                     throw new ArgumentNullException(nameof(compositeAssetService));
            _soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            _curtainService = curtainService ?? throw new ArgumentNullException(nameof(curtainService));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public UniTask<IScene> Create(object payload)
        {
            IScene gameplayScene = new GameplayScene(
                _uiViewService,
                _joinManager,
                _sceneService,
                _inputService,
                _uiReflexInjector,
                _sdkService,
                _assetCollector,
                _entityRepository,
                _container,
                _rootGameObject,
                _compositeAssetService,
                _soundService,
                _focusService,
                _localizationService,
                _curtainService,
                _cameraService,
                _updateService);

            return UniTask.FromResult(gameplayScene);
        }
    }
}