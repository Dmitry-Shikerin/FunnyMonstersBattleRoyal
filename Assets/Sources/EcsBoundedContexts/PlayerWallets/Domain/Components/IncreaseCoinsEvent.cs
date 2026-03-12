using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.PlayerWallets.Domain.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct IncreaseCoinsEvent
    {
        public int Value;
    }
}