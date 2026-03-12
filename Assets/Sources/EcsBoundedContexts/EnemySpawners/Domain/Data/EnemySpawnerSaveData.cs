using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Data
{
    public struct EnemySpawnerSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public int SpawnedAllEnemiesInWave { get; set; }
        public int SpawnedEnemiesInCurrentWave { get; set; }
        public int SpawnedBossesInCurrentWave { get; set; }
        public int SpawnedKamikazeInCurrentWave { get; set; }
        public int WaveIndex { get; set; }
        public EnemyType LastSpawnedEnemyType { get; set; }
    }
}