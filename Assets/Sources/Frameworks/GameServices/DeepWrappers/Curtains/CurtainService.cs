using Cysharp.Threading.Tasks;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;

namespace Sources.Frameworks.GameServices.DeepWrappers.Curtains
{
    public class CurtainService : ICurtainService
    {
        public bool IsInProgress => DeepUiBrain.CurtainView.IsInProgress;
        
        public UniTask ShowAsync() =>
            DeepUiBrain.CurtainView.ShowAsync();
        
        public UniTask HideAsync() =>
            DeepUiBrain.CurtainView.HideAsync();
    }
}