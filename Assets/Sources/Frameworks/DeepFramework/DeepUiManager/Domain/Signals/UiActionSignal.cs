using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct UiActionSignal : ISignal
    {
        public UiActionSignal(IEnumerable<UiActionId> actionIds)
        {
            ActionIds = actionIds;
        }

        public IEnumerable<UiActionId> ActionIds { get; }
    }
}