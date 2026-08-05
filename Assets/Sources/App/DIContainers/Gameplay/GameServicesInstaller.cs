using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.Frameworks.GameServices.Linecasts.Implementation;
using Sources.Frameworks.GameServices.Linecasts.Interfaces;
using Sources.Frameworks.GameServices.Overlaps.Implementation;
using Sources.Frameworks.GameServices.Overlaps.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.GameServices.Pauses.Impl;
using Sources.Frameworks.GameServices.UpdateServices.Implementation;
using Sources.Frameworks.GameServices.UpdateServices.Interfaces;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameServicesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(OverlapService), new [] { typeof(IOverlapService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LinecastService), new [] { typeof(ILinecastService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(PauseService), new [] { typeof(IPauseService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(UpdateService), new [] { typeof(IUpdateService), typeof(IUpdateRegister) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Camera
            containerBuilder.RegisterType(typeof(CameraService), new [] { typeof(ICameraService) }, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}