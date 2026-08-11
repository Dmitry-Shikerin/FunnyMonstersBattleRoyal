using Reflex.Core;
using Reflex.Enums;
using Sources.BoundedContexts.Hud.Infrastructure.Services;
using Sources.BoundedContexts.Hud.Infrastructure.Services.Interfaces;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services;
using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Common
{
    public class SettingsServicesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(QualityService), new[] { typeof(IQualityService) }, Lifetime.Singleton, Resolution.Lazy);
            builder.RegisterType(typeof(ScreenService), new[] { typeof(IScreenService) }, Lifetime.Singleton, Resolution.Lazy);
            builder.RegisterType(typeof(QuitService), new[] { typeof(IQuitService) }, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}