using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct ShowUiPopUpSignal : ISignal
    {
        public ShowUiPopUpSignal(UiPopUpId viewId)
        {
            ViewId = viewId;
        }

        public UiPopUpId ViewId { get; }
    }
}