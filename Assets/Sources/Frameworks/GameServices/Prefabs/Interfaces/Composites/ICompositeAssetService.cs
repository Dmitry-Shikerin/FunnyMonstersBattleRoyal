using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.GameServices.Prefabs.Interfaces.Composites
{
    public interface ICompositeAssetService
    {
        UniTask LoadAsync(string assetCollectorPath, string addressablesCollectorPath);
        void Release();
    }
}