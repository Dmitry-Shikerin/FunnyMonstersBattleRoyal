using System;
using System.Collections.Generic;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces.Generic;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces;
using Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic;

namespace Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Implementation
{
    public class StreamSignalBus : ISignalBus
    {
        private readonly Dictionary<Type, ISignalStream> _streams = new ();
        
        public ISignalStream<T> GetStream<T>()
            where T : struct, ISignal
        {
            if (_streams.ContainsKey(typeof(T)) == false)
                _streams[typeof(T)] = new SignalStream<T>();

            if (_streams[typeof(T)] is not SignalStream<T> concrete)
                throw new InvalidCastException(nameof(SignalStream<T>));
            
            return concrete;
        }

        public void Subscribe<T>(ISignalAction<T> signalAction)
            where T : struct, ISignal =>
            Subscribe<T>(signalAction.Handle);

        public void Unsubscribe<T>(ISignalAction<T> signalAction)
            where T : struct, ISignal =>
            Unsubscribe<T>(signalAction.Handle);

        public void Subscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal =>
            GetStream<T>().Subscribe(signalHandler);

        public void Unsubscribe<T>(Action<T> signalHandler)
            where T : struct, ISignal =>
            GetStream<T>().Unsubscribe(signalHandler);

        public void Handle<T>(T signal)
            where T : struct, ISignal =>
            GetStream<T>().Handle(signal);

        public void Release()
        {
            foreach (ISignalStream stream in _streams.Values)
                stream.Release();
            
            _streams.Clear();
        }
    }
}