using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces
{
    public interface IUiAction
    {
        UiActionId Id { get; }
        
        public void Initialize();
        public void Handle();
        public void Destroy();
    }
}