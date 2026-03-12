using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Data
{
    public struct KillEnemyCounterSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int KilledEnemiesCount { get; set; }
    }
}