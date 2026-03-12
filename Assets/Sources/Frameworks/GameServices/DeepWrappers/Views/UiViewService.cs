using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;

namespace Sources.Frameworks.GameServices.DeepWrappers.Views
{
    public class UiViewService : IUiViewService
    {
        public T Get<T>()
            where T : UiView =>
            DeepUiBrain.ViewManager.Get<T>();

        public void Show(UiViewId id) =>
            DeepUiBrain.SignalBus.Handle(new ShowUiViewSignal(id));
    }
}