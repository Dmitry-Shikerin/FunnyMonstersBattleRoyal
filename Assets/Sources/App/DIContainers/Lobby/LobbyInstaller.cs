using Reflex.Core;
using Reflex.Enums;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Scenes.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Input.Infrastructure.Services;
using Sources.EcsBoundedContexts.NetworkCore.Services;
using Sources.Frameworks.GameServices.InputServices;
using Sources.Frameworks.GameServices.InputServices.InputServices;
using Sources.Frameworks.GameServices.Prefabs.Implementation.Composites;
using Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites;
using Sources.Frameworks.GameServices.Scenes.Infrastructure.Factories.Controllers.Interfaces;
using UnityEngine;

namespace Sources.App.DIContainers.Lobby
{
    public class LobbyInstaller : MonoBehaviour, IInstaller
    {
        [Required] [SerializeField] private RootGameObject _rootGameObject;
        [Required] [SerializeField] private JoinManager _joinManager;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_rootGameObject);
            containerBuilder.RegisterValue(_joinManager);
            
            containerBuilder.RegisterType(typeof(GameplaySceneFactory), new [] { typeof(ISceneFactory) }, Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            containerBuilder.RegisterType(typeof(GameplayAssetService), new [] { typeof(ICompositeAssetService) }, Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            
            containerBuilder.RegisterType(typeof(NewInputService), new [] { typeof(IInputService) }, Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            
            containerBuilder.RegisterType(typeof(CharacterFactory), Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            
            //ECS
            containerBuilder.RegisterType(typeof(GameSystemsCollector), new [] { typeof(ISystemsCollector) }, Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            GameSystemsInstaller.InstallBindings(containerBuilder);
        }
    }
}