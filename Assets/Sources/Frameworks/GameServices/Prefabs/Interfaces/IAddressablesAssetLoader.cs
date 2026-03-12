using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sources.Frameworks.GameServices.Prefabs.Interfaces
{
    public interface IAddressablesAssetLoader : IPrefabLoader
    {
        public UniTask<T> LoadAsset<T>(AssetReferenceT<T> assetReference)
            where T : Object;
        public UniTask<T> LoadAsset<T>(AssetReferenceT<GameObject> assetReference)
            where T : Object;
    }
}