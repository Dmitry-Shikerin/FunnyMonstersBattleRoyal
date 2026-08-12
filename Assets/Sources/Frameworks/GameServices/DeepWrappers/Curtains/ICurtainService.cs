using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.GameServices.DeepWrappers.Curtains
{
    public interface ICurtainService
    {
        bool IsInProgress { get; }
        
        public UniTask ShowAsync();
        public UniTask HideAsync();
    }
}