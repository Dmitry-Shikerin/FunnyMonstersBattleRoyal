using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Interfaces
{
    public interface ICurtainView
    {
        bool IsInProgress { get; }
        
        public UniTask ShowAsync();
        public UniTask HideAsync();
    }
}