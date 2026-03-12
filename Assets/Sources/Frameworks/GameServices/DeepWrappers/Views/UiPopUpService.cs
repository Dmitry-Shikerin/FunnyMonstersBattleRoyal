using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;

namespace Sources.Frameworks.GameServices.DeepWrappers.Views
{
    public class UiPopUpService : IUiPopUpService
    {
        public T Get<T>()
            where T : UiPopUpView =>
            DeepUiBrain.PopUpViewManager.Get<T>();

        public void Show(UiPopUpId id) =>
            DeepUiBrain.SignalBus.Handle(new ShowUiPopUpSignal(id));

        public void Hide(UiPopUpId id) =>
            DeepUiBrain.SignalBus.Handle(new HideUiPopUpSignal(id));
    }
}