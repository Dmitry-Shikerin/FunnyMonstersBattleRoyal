using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct HealthComponent
    {
        public int Value;
    }
}