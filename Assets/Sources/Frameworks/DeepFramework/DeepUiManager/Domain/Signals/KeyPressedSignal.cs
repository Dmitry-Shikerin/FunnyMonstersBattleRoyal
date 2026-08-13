using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Signals
{
    public struct KeyPressedSignal : ISignal
    {
        public KeyPressedSignal(KeyCode key)
        {
            Key = key;
        }

        public KeyCode Key { get; }
    }
}