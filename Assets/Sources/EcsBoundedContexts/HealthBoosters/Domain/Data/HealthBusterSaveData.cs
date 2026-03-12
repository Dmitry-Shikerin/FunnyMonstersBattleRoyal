using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.HealthBoosters.Domain.Data
{
    public struct HealthBusterSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public bool IsFirstUsedCompleted { get; set; }
    }
}