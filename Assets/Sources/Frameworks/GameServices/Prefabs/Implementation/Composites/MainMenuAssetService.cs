using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Prefabs.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public class MainMenuAssetService : CompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;

        public MainMenuAssetService(
            IAddressablesAssetLoader addressablesAssetLoader, 
            IResourcesAssetLoader resourcesAssetLoader) 
            : base(
                addressablesAssetLoader, 
                resourcesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader;
        }

        protected override UniTask LoadByResourcesConfigAsync(string assetCollectorPath)
        {
            return UniTask.CompletedTask;
        }

        protected override UniTask LoadByResourcesFoldersAsync()
        {
            return UniTask.CompletedTask;
        }

        protected override async UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath)
        {
            AddressablesAssetConfig config = await _addressablesAssetLoader.LoadAsset<AddressablesAssetConfig>(addressablesCollectorPath);

            //Configs
            await AddressalesLoad(config.UiConfig);
            await AddressalesLoad(config.DailyRewardConfig);
        }
    }
}