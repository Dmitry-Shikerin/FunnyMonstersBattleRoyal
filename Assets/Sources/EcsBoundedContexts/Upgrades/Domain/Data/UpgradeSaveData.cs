using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Upgrades.Domain.Data
{
    public struct UpgradeSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int UpgradeIndex { get; set; }
    }
}