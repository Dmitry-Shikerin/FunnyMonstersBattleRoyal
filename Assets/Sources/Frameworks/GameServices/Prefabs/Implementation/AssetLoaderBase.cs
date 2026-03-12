using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.GameServices.Prefabs.Implementation
{
    public abstract class AssetLoaderBase : IPrefabLoader
    {
        protected readonly IAssetCollector AssetCollector;
        protected readonly List<Object> _objects;

        protected AssetLoaderBase(IAssetCollector assetCollector)
        {
            AssetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
            _objects = new List<Object>();
        }
        
        protected IReadOnlyList<Object> Objects => _objects;
        
        public async UniTask<T> LoadAsset<T>(string address)
            where T : Object
        {
            Object asset = await LoadAssetAsync<T>(address);
            
            if(asset == null)
                throw new InvalidOperationException(typeof(T).Name);
            
            if(asset is not T component)
                throw new InvalidOperationException(typeof(T).Name);
            
            _objects.Add(asset);
            AssetCollector.Add(typeof(T), component);
            
            return component;
        }
        
        protected abstract UniTask<Object> LoadAssetAsync<T>(string address)
            where T : Object;

        public virtual void ReleaseAll()
        {
            _objects.ForEach(AssetCollector.Remove);
            _objects.Clear();
        }
    }
}