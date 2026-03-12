using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;

namespace Sources.App.DIContainers.Common
{
    public class UiFrameworkInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ILocalizationService, LocalizationService>();
            
            //Soundy
            container.Bind<ISoundService, SoundService>();
            
            //UI
            container.Bind<IUiPopUpService, UiPopUpService>();
            container.Bind<IUiViewService, UiViewService>();
        }
    }
}