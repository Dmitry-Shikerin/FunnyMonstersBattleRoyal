using Reflex.Core;
using Reflex.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.UiReflexInjectors;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Common
{
    public class UiFrameworkInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(LocalizationService), new [] { typeof(ILocalizationService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Soundy
            containerBuilder.RegisterType(typeof(SoundService), new [] { typeof(ISoundService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //UI
            containerBuilder.RegisterType(typeof(UiPopUpService), new [] { typeof(IUiPopUpService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(UiViewService), new [] { typeof(IUiViewService) }, Lifetime.Singleton, Resolution.Lazy);            
            
            //Injector
            containerBuilder.RegisterType(typeof(UiReflexInjector), Lifetime.Singleton, Resolution.Lazy);
        }
    }
}