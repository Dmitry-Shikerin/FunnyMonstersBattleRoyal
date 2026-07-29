using System;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces.Generic;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic;

namespace Sources.EcsBoundedContexts.SignalBuses
{
    public class SignalBus : ISignalBus
    {
        public void Handle<T>(T signal) 
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.Handle<T>(signal);

        public ISignalStream<T> GetStream<T>() 
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.GetStream<T>();

        public void Subscribe<T>(ISignalAction<T> signalAction)
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.Subscribe<T>(signalAction);

        public void Unsubscribe<T>(ISignalAction<T> signalAction) 
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.Unsubscribe<T>(signalAction);

        public void Subscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.Subscribe<T>(signalHandler);

        public void Unsubscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal =>
            DeepUiBrain.SignalBus.Unsubscribe<T>(signalHandler);

        public void Release() =>
            DeepUiBrain.SignalBus.Release();
    }
}