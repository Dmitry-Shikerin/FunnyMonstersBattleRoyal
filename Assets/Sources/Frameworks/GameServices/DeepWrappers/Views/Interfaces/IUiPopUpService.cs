using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;

namespace Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces
{
    public interface IUiPopUpService : IUiViewServiceBase<UiPopUpId, UiPopUpView>
    {
        void Hide(UiPopUpId id);
    }
}