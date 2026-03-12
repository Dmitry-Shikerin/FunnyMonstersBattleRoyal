using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions
{
    public abstract class UiAction : IUiAction
    {
        public abstract UiActionId Id { get;  }
        
        public virtual void Initialize()
        {
        }

        public abstract void Handle();

        public virtual void Destroy()
        {
        }
    }
}