using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services
{
    public interface ISpawnService
    {
        EnemySpawnerConfig Config { get; }

        ProtoEntity GetRandomSpawnPoint(ProtoEntity spawnEntity);
        EnemySpawnerWave GetCurrentWave(ProtoEntity spawnEntity);
        EnemyType GetNextEnemyType(ProtoEntity spawnEntity);
        void IncreaseSpawnedEnemies(ProtoEntity spawnEntity, EnemyType enemyType);
        int GetAttackPower(ProtoEntity spawnEntity, EnemyType type);
        int GetMassAttackPower(ProtoEntity spawnEntity, EnemyType type);
        int GetHealth(ProtoEntity spawnEntity, EnemyType type);
        void IncreaseCurrentWave(ProtoEntity spawnEntity);
        int GetSumSpawnedEnemies(ProtoEntity spawnEntity);
        int GetSumEnemiesInCurrentWave(ProtoEntity spawnEntity);
    }
}