using System;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation
{
    public class AddressablesAssetLoader : AssetLoaderBase, IAddressablesAssetLoader
    {
        public AddressablesAssetLoader(
            IAssetCollector assetCollector) 
            : base(assetCollector)
        {
        }
        
        protected override async UniTask<Object> LoadAssetAsync<T>(string address) =>
            await Addressables.LoadAssetAsync<T>(address).Task;
        
        public async UniTask<T> LoadAsset<T>(AssetReferenceT<T> assetReference)
            where T : Object
        {
            Object asset = await Addressables.LoadAssetAsync<T>(assetReference).Task;
            
            if(asset == null)
                throw new InvalidOperationException(typeof(T).Name);
            
            if(asset is not T component)
                throw new InvalidOperationException(typeof(T).Name);
            
            _objects.Add(asset);
            AssetCollector.Add(typeof(T), component);
            
            return component;
        }     
        
        public async UniTask<T> LoadAsset<T>(AssetReferenceT<GameObject> assetReference)
            where T : Object
        {
            Object asset = await Addressables.LoadAssetAsync<GameObject>(assetReference).Task;
            
            if(asset == null)
                throw new InvalidOperationException(typeof(T).Name);
            
            T component = asset.GetComponent<T>();
            
            if(component == null)
                throw new InvalidOperationException(typeof(T).Name);
            
            _objects.Add(asset);
            AssetCollector.Add(typeof(T), asset);
            
            return component;
        }

        public override void ReleaseAll()
        {
            Objects.ForEach(Addressables.Release);
            base.ReleaseAll();
        }
    }
}