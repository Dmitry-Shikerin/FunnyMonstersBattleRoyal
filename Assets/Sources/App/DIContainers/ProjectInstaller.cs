using Reflex.Core;
using Reflex.Enums;
using Sources.Frameworks.GameServices.SceneLoaderServices.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;
using CurtainService = Sources.Frameworks.GameServices.DeepWrappers.Curtains.CurtainService;
using ICurtainService = Sources.Frameworks.GameServices.DeepWrappers.Curtains.ICurtainService;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(PhotonSceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            //containerBuilder.RegisterType(typeof(SceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            //containerBuilder.RegisterType(typeof(AddressableSceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(SceneService), new []{typeof(ISceneService)}, Lifetime.Singleton, Resolution.Lazy);

            //Curtain
            containerBuilder.RegisterType(typeof(CurtainService), new []{typeof(ICurtainService)}, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}