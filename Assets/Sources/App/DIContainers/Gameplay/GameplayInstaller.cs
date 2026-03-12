using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using UnityEngine;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        
        public override void InstallBindings(DiContainer container)
        {
            container.Bind(_rootGameObject);
            
            container.Bind<ISceneFactory, GameplaySceneFactory>();
            container.Bind<ICompositeAssetService, GameplayAssetService>();
            
            //ECS
            container.Bind<ISystemsCollector, GameSystemsCollector>();
            GameSystemsInstaller.InstallBindings(container);
        }
    }
}