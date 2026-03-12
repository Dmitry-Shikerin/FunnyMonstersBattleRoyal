using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.Frameworks.GameServices.Linecasts.Implementation;
using Sources.Frameworks.GameServices.Linecasts.Interfaces;
using Sources.Frameworks.GameServices.Overlaps.Implementation;
using Sources.Frameworks.GameServices.Overlaps.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.GameServices.Pauses.Impl;
using Sources.Frameworks.GameServices.UpdateServices.Implementation;

namespace Sources.App.DIContainers.Gameplay
{
    public class GameServicesInstaller : MonoInstaller
    {
        // [Required] [SerializeField] private CameraView _cameraView;
        // [Required] [SerializeField] private SkyAndWeatherView _skyAndWeatherView;
        
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IOverlapService, OverlapService>();
            container.Bind<ILinecastService, LinecastService>();
            container.Bind<IPauseService, PauseService>();
            container.BindInterfaces<UpdateService>();
            
            //Camera
            //container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
            container.Bind<ICameraService, CameraService>();
            
            //SkyAndWeather
            //container.Bind<SkyAndWeatherView>().FromInstance(_skyAndWeatherView).AsSingle();
            //container.Bind<ISkyAndWeatherService>().To<SkyAndWeatherService>().AsSingle();
        }
    }
}