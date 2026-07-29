using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;

namespace Sources.Frameworks.GameServices.SignalBuses.StreamBuses.Interfaces.Generic
{
    public interface ISignalAction<in T> 
        where T : struct, ISignal
    {
        void Handle(T signal);
    }
}