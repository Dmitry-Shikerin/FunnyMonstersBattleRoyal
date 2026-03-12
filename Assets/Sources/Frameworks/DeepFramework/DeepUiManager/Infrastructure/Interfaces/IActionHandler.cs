using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Interfaces
{
    public interface IActionHandler
    {
        void Initialize();
        void Add(IUiAction uiAction);
        void Handle(UiActionId id);
        void Handle(IEnumerable<UiActionId> actionNames);
        void Destroy();
    }
}