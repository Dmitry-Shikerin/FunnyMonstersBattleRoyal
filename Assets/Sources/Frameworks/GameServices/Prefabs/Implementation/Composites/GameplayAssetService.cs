using System;
using Cysharp.Threading.Tasks;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.Prefabs.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

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

            //ResourcesAssetsConfig config = await _resourcesAssetLoader.LoadAsset<ResourcesAssetsConfig>(assetCollectorPath);

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
                //_resourcesAssetLoader.LoadAsset<CharacterModule>(ResourcesPrefabPath.Character)
            );
        }
        
        protected override async UniTask LoadByAddressableConfigAsync(string addressablesCollectorPath)
        {
            AddressablesAssetConfig config = await _addressablesAssetLoader.LoadAsset<AddressablesAssetConfig>(addressablesCollectorPath);

            //Configs
            Debug.Log($"Load characterConfig");
            await AddressablesLoad(config.CharacterConfig);
            Debug.Log($"Load UiConfig");
            await AddressablesLoad(config.UiConfig);
            Debug.Log($"Load AnimationConfig");
            await AddressablesLoad(config.AnimationConfig);
            Debug.Log($"Load AdvertisingAfterWaveConfig");
            await AddressablesLoad(config.AdvertisingAfterWaveConfig);
            Debug.Log($"Load DailyRewardConfig");
            await AddressablesLoad(config.DailyRewardConfig);
            Debug.Log($"Load AnimatorLodConfig");
            await AddressablesLoad(config.AnimatorLodConfig);
            
            //Prefabs
            //await AddressalesPrefabLoad<CharacterModule>(config.CharacterModule);
        }
    }
}