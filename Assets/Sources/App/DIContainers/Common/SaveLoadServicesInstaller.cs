using Reflex.Core;
using Reflex.Enums;
using Sources.Frameworks.GameServices.Loads.Services.Implementation;
using Sources.Frameworks.GameServices.Loads.Services.Implementation.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Prefabs.Implementation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Common
{
    public class SaveLoadServicesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(StorageService), new [] { typeof(IStorageService) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(PlayerPrefsDataService), new [] { typeof(IDataService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Assets
            containerBuilder.RegisterType(typeof(AssetCollector), new [] { typeof(IAssetCollector) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(ResourcesAssetLoader), new [] { typeof(IResourcesAssetLoader) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(AddressablesAssetLoader), new [] { typeof(IAddressablesAssetLoader) }, Lifetime.Singleton, Resolution.Lazy);
        }
    }
}