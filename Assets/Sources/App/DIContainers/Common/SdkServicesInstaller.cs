using Reflex.Core;
using Reflex.Enums;
using Sources.Frameworks.GameServices.ServerTimes.Services.Implementation;
using Sources.Frameworks.GameServices.ServerTimes.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Focuses.Implementation;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Implementation;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Services.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Common
{
    public class SdkServicesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(FocusService), new [] { typeof(IFocusService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(YandexLeaderboardService), new [] { typeof(ILeaderboardService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(YandexSdkService), new [] { typeof(ISdkService) }, Lifetime.Singleton, Resolution.Lazy);
            // Container.Bind<ITimeService>().To<NetworkTimeService>().AsSingle();
            containerBuilder.RegisterType(typeof(DayTimeService), new [] { typeof(ITimeService) }, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}