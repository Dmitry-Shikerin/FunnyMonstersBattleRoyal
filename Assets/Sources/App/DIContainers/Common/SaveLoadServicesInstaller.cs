using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.Frameworks.GameServices.Loads.Services.Implementation;
using Sources.Frameworks.GameServices.Loads.Services.Implementation.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Implementation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.App.DIContainers.Common
{
    public class SaveLoadServicesInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IStorageService, StorageService>();
            container.Bind<IDataService, PlayerPrefsDataService>();
            
            //Assets
            container.Bind<IAssetCollector, AssetCollector>();
            container.Bind<IResourcesAssetLoader, ResourcesAssetLoader>();
            container.Bind<IAddressablesAssetLoader, AddressablesAssetLoader>();
        }
    }
}