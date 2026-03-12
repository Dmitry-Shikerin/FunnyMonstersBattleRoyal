using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.PlayerWallets.Domain.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct DecreaseCoinsEvent
    {
        public int Value;
    }
}