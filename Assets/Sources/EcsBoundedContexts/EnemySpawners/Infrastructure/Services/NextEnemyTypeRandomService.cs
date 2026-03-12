using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services
{
    public class NextEnemyTypeRandomService : INextEnemyTypeService
    {
        private readonly ISpawnService _spawnService;

        public NextEnemyTypeRandomService(ISpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public EnemyType GetNextEnemyType(ProtoEntity spawnEntity)
        {
            int random = Random.Range(0, 100);

            if (TrySpawnBoss(spawnEntity, random))
            {
                return EnemyType.Boss;
            }

            if (TrySpawnKamikaze(spawnEntity, random))
            {
                return EnemyType.Kamikaze;
            }

            if (TrySpawnEnemy(spawnEntity, random))
            {
                return EnemyType.Enemy;
            }
            
            return EnemyType.Default;
        }
        
        private bool TrySpawnEnemy(ProtoEntity spawnEntity, int random)
        {
            if (random <= 66)
                return false;
            
            int spawnedEnemiesInCurrentWave = spawnEntity.GetEnemySpawnerData().SpawnedEnemies;
            return spawnedEnemiesInCurrentWave < _spawnService.GetSumEnemiesInCurrentWave(spawnEntity);
        }

        private bool TrySpawnBoss(ProtoEntity spawnEntity, int random)
        {
            if (random > 33)
                return false;
            
            int spawnedBossesInCurrentWave = spawnEntity.GetEnemySpawnerData().SpawnedBosses;
            EnemySpawnerWave currentWave = _spawnService.GetCurrentWave(spawnEntity);
            
            if (spawnedBossesInCurrentWave >= currentWave.BossesCount)
                return false;
            
            EnemySpawnerDataComponent data = spawnEntity.GetEnemySpawnerData();
            int nextSum = _spawnService.GetSumSpawnedEnemies(spawnEntity) + 1;

            if (nextSum == _spawnService.GetSumEnemiesInCurrentWave(spawnEntity))
                return true;

            // if (_enemySpawner.LastSpawnedEnemyType != typeof(EnemyBossView))
            //     return true;

            if (data.SpawnedEnemies == currentWave.EnemyCount)
                return true;

            return false;
        }

        private bool TrySpawnKamikaze(ProtoEntity spawnEntity, int random)
        {
            if (random is < 33 or > 66)
                return false;
            
            int spawnedKamikazeInCurrentWave = spawnEntity.GetEnemySpawnerData().SpawnedKamikaze;
            var currentWave = _spawnService.GetCurrentWave(spawnEntity);
            
            if (spawnedKamikazeInCurrentWave >= currentWave.KamikazeEnemyCount)
                return false;
            
            var data = spawnEntity.GetEnemySpawnerData();
            var nextSum = _spawnService.GetSumSpawnedEnemies(spawnEntity) + 1;

            if (nextSum == _spawnService.GetSumEnemiesInCurrentWave(spawnEntity))
                return true;

            // if (_enemySpawner.LastSpawnedEnemyType != typeof(EnemyKamikazeView))
            //     return true;

            if (data.SpawnedEnemies == currentWave.EnemyCount)
                return true;

            return false;
        }
    }
}