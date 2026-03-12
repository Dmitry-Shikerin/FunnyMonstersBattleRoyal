using System;
using Cysharp.Threading.Tasks;
using MyDependencies.Sources.Containers;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Controllers;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Controllers.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.BoundedContexts.Scenes.Infrastructure.Factories
{
    public class GameplaySceneFactory : ISceneFactory
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

        public GameplaySceneFactory(
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
            _rootGameObject = rootGameObject ?? throw new ArgumentNullException(nameof(rootGameObject));
            _compositeAssetService = compositeAssetService ?? 
                                     throw new ArgumentNullException(nameof(compositeAssetService));
            _soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
            _ecsGameStartUp = ecsGameStartUp ?? throw new ArgumentNullException(nameof(ecsGameStartUp));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public UniTask<IScene> Create(object payload)
        {
            IScene gameplayScene = new GameplayScene(
                _sdkService,
                _assetCollector,
                _entityRepository,
                _container,
                _rootGameObject,
                _compositeAssetService,
                _soundService,
                _ecsGameStartUp,
                _focusService,
                _localizationService,
                _curtainView,
                _cameraService,
                _updateService);

            return UniTask.FromResult(gameplayScene);
        }
    }
}