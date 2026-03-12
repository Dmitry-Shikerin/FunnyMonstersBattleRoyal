using System;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces.Generic;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic;

namespace Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces
{
    public interface ISignalBus
    {
        void Handle<T>(T signal) 
            where T : struct, ISignal;
        ISignalStream<T> GetStream<T>()
            where T : struct, ISignal;
        void Subscribe<T>(ISignalAction<T> signalAction)
            where T : struct, ISignal;
        void Unsubscribe<T>(ISignalAction<T> signalAction)
            where T : struct, ISignal;
        void Subscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal;
        void Unsubscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal;
        void Release();
    }
}