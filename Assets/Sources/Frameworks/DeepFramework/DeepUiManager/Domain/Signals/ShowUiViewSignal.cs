using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct ShowUiViewSignal : ISignal
    {
        public ShowUiViewSignal(UiViewId viewId)
        {
            ViewId = viewId;
        }

        public UiViewId ViewId { get; }
    }
}