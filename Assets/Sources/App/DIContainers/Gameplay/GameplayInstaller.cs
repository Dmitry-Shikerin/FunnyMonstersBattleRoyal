using Reflex.Core;
using Reflex.Enums;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks;
using Sources.BoundedContexts.Networks.Core;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories;
using Sources.BoundedContexts.TestGameplays;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        [Required] [SerializeField] private EcsGameStartUp _ecsGameStartUp;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_rootGameObject);
            containerBuilder.RegisterValue(_ecsGameStartUp, new []{ typeof(IEcsGameStartUp) });
            
            containerBuilder.RegisterType(typeof(GameplaySceneFactory), new [] { typeof(ISceneFactory) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(GameplayAssetService), new [] { typeof(ICompositeAssetService) }, Lifetime.Singleton, Resolution.Lazy);
            
            containerBuilder.RegisterType(typeof(NewInputService), new [] { typeof(IInputService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //ECS
            containerBuilder.RegisterType(typeof(GameSystemsCollector), new [] { typeof(ISystemsCollector) }, Lifetime.Singleton, Resolution.Lazy);
            GameSystemsInstaller.InstallBindings(containerBuilder);
        }
    }
}