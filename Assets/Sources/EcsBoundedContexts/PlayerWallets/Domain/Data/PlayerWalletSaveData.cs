using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.PlayerWallets.Domain.Data
{
    public struct PlayerWalletSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int Coins { get; set; }
    }
}