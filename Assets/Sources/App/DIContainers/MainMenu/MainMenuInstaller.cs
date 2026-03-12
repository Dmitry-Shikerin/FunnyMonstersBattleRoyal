using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
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

namespace Sources.App.DIContainers.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [Required] [SerializeField] private MainMenuRootGameObjects _mainRootGameObjects;
        
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ISceneFactory, MainMenuSceneFactory>();
            container.Bind(_mainRootGameObjects);

            //Services
            container.Bind<IPauseService, PauseService>();
            container.Bind<ICameraService, CameraService>();
            container.Bind<ICompositeAssetService, MainMenuAssetService>();
            
            //Systems
            container.Bind<ISystemsCollector, MainMenuSystemsCollector>();
            MainMenuSystemsInstaller.InstallBindings(container);
        }
    }
}