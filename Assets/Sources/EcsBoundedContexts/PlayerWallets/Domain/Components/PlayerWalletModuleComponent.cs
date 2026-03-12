using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.PlayerWallets.Presentation;

namespace Sources.EcsBoundedContexts.PlayerWallets.Domain.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct PlayerWalletModuleComponent
    {
        public PlayerWalletModule Value;
    }
}