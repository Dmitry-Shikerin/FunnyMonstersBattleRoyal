using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces.Generic;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic;

namespace Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Implementation
{
    public class SignalStream<T> : ISignalStream<T> 
        where T : struct, ISignal
    {
        private readonly List<Action<T>> _handlers = new ();

        public void Handle(T signal) =>
            _handlers.ForEach(handler => handler.Invoke(signal));

        public void Subscribe(Action<T> signalHandler) =>
            _handlers.Add(signalHandler);

        public void Unsubscribe(Action<T> signalHandler)
        {
            if (_handlers.Contains(signalHandler) == false)
                return;
            
            _handlers.Remove(signalHandler);
        }

        public void Release() =>
            _handlers.Clear();
    }
}