using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces
{
    public interface ICurtainView
    {
        bool IsInProgress { get; }
        
        public UniTask ShowAsync();
        public UniTask HideAsync();
    }
}