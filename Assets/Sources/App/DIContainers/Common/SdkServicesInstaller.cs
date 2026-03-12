using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.ServerTimes.Services.Implementation;
using Sources.Frameworks.GameServices.ServerTimes.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Implementation;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Implementation;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.App.DIContainers.Common
{
    public class SdkServicesInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IFocusService, FocusService>();
            container.Bind<ILeaderboardService, YandexLeaderboardService>();
            container.Bind<ISdkService, YandexSdkService>();
            // Container.Bind<ITimeService>().To<NetworkTimeService>().AsSingle();
            container.Bind<ITimeService, DayTimeService>();
        }
    }
}