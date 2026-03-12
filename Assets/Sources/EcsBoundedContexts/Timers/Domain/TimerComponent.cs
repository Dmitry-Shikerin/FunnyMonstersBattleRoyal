using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Timers.Domain
{
    [Component(group: ComponentGroup.Common)]
    public struct TimerComponent
    {
        public float Value;
    }
}