using System;
using Cysharp.Threading.Tasks;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Enemies.Presentation;
using Sources.EcsBoundedContexts.ExplosionBodies.Presentation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation.Composites
{
    public class GameplayAssetService : CompositeAssetService
    {
        private readonly IAddressablesAssetLoader _addressablesAssetLoader;
        private readonly IResourcesAssetLoader _resourcesAssetLoader;
        private readonly IAddressablesAssetLoader[] _assetServices;

        public GameplayAssetService(
            IAddressablesAssetLoader addressablesAssetLoader,
            IResourcesAssetLoader resourcesAssetLoader)
            : base(
                addressablesAssetLoader,
                resourcesAssetLoader)
        {
            _addressablesAssetLoader = addressablesAssetLoader ??
                                       throw new ArgumentNullException(nameof(addressablesAssetLoader));
            _resourcesAssetLoader = resourcesAssetLoader ??
                                    throw new ArgumentNullException(nameof(resourcesAssetLoader));
        }
        
        protected override async UniTask LoadByResourcesConfigAsync(string assetCollectorPath)
        {
            if (string.IsNullOrEmpty(assetCollectorPath))
                return;

            if (string.IsNullOrWhiteSpace(assetCollectorPath))
                return;

            ResourcesAssetsConfig config = await _resourcesAssetLoader.LoadAsset<ResourcesAssetsConfig>(assetCollectorPath);

            // foreach (var asset in config.Assets)
            // {
            //     //await _resourcesAssetLoader.LoadAsset(asset.FolderPath);
            // }
        }

        protected override async UniTask LoadByResourcesFoldersAsync()
        {
            await UniTask.WhenAll
            (
                _resourcesAssetLoader.LoadAsset<PoolManagerCollector>(ResourcesPrefabPath.PoolManagerCollector)
            );
        }
        
        protected override async UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath)
        {
            AddressablesAssetConfig config = await _addressablesAssetLoader.LoadAsset<AddressablesAssetConfig>(addressablesCollectorPath);

            //Configs
            await AddressalesLoad(config.SkyAndWeatherCollector);
            await AddressalesLoad(config.AchievementConfigCollector);
            await AddressalesLoad(config.AnimatorLodSettingsCollector);
            await AddressalesLoad(config.ShadowManagerConfigCollector);
            await AddressalesLoad(config.UpgradeConfigContainer);
            await AddressalesLoad(config.CharacterRangeConfig);
            await AddressalesLoad(config.CharacterMeleeConfig);
            await AddressalesLoad(config.EnemyConfig);
            await AddressalesLoad(config.EnemyKamikazeConfig);
            await AddressalesLoad(config.EnemyBossConfig);
            await AddressalesLoad(config.UiConfig);
            await AddressalesLoad(config.AnimationConfig);
            await AddressalesLoad(config.AdvertisingAfterWaveConfig);
            await AddressalesLoad(config.BurnConfig);
            await AddressalesLoad(config.DailyRewardConfig);
            
            //Prefabs
            await AddressalesPrefabLoad<CharacterModule>(config.CharacterMeleeModule);
            await AddressalesPrefabLoad<EnemyModule>(config.EnemyModule);
            await AddressalesPrefabLoad<EnemyKamikazeModule>(config.EnemyKamikazeModule);
            await AddressalesPrefabLoad<EnemyBossModule>(config.EnemyBossModule);
            await AddressalesPrefabLoad<ExplosionBodyBloodyModule>(config.ExplosionBodyBloodyModule);
            await AddressalesPrefabLoad<ExplosionBodyModule>(config.ExplosionBodyModule);
        }
    }
}