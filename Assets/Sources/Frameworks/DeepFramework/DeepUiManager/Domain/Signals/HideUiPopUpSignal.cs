using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct HideUiPopUpSignal : ISignal
    {
        public HideUiPopUpSignal(UiPopUpId viewId)
        {
            ViewId = viewId;
        }

        public UiPopUpId ViewId { get; }
    }
}