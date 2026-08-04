using Fusion;
using Reflex.Core;
using Reflex.Enums;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.GameServices.Pauses.Impl;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.MainMenu
{
    public class MainMenuInstaller : MonoBehaviour, IInstaller
    {
        [Required] [SerializeField] private MainMenuRootGameObjects _mainRootGameObjects;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(MainMenuSceneFactory), new [] { typeof(ISceneFactory) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterValue(_mainRootGameObjects);

            //Ecs
            containerBuilder.RegisterType(typeof(LeoEcsGameStartUp), new [] { typeof(IEcsGameStartUp) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Services
            containerBuilder.RegisterType(typeof(PauseService), new [] { typeof(IPauseService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(CameraService), new [] { typeof(ICameraService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(MainMenuAssetService), new [] { typeof(ICompositeAssetService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Systems
            containerBuilder.RegisterType(typeof(MainMenuSystemsCollector), new [] { typeof(ISystemsCollector) }, Lifetime.Singleton, Resolution.Lazy);
            MainMenuSystemsInstaller.InstallBindings(containerBuilder);
        }
    }
}