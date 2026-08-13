using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct OnClickSignal : ISignal
    {
        public OnClickSignal(ButtonId buttonId)
        {
            ButtonId = buttonId;
        }

        public ButtonId ButtonId { get; }
    }
}