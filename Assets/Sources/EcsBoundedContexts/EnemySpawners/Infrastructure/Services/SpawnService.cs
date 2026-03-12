using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Enums;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services
{
    public class SpawnService : ISpawnService
    {
        private readonly IAssetCollector _assetCollector;
        private EnemySpawnerConfig _config;
        private readonly Dictionary<SpawnLogic, INextEnemyTypeService> _nextTypeServices;

        public SpawnService(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
            _nextTypeServices = new Dictionary<SpawnLogic, INextEnemyTypeService> 
            {
                { SpawnLogic.Random, new NextEnemyTypeRandomService(this) }, 
                { SpawnLogic.Weighted, new NextEnemyTypeWeightedService(this) }, 
            };
        }

        public EnemySpawnerConfig Config => _config ?? _assetCollector.Get<EnemySpawnerConfig>();
        public List<EnemySpawnStrategy> SpawnStrategies { get; set; }

        public int GetSumEnemiesInCurrentWave(ProtoEntity spawnEntity)
        {
            EnemySpawnerWave currentWave = GetCurrentWave(spawnEntity);
            return currentWave.EnemyCount + currentWave.BossesCount + currentWave.KamikazeEnemyCount;
        }

        public ProtoEntity GetRandomSpawnPoint(ProtoEntity spawnEntity)
        {
            ProtoEntity[,] spawnPoints = spawnEntity.GetEnemySpawnPoints().Value;
            return spawnPoints[Random.Range(0, spawnPoints.GetLength(0)), Random.Range(0, spawnPoints.GetLength(1))];
        }

        public EnemySpawnerWave GetCurrentWave(ProtoEntity spawnEntity)
        {
            int waveIndex = spawnEntity.GetEnemySpawnerData().WaweIndex;
            return Config.Waves[waveIndex];
        }

        public int GetSumSpawnedEnemies(ProtoEntity spawnEntity)
        {
            EnemySpawnerDataComponent data = spawnEntity.GetEnemySpawnerData();
            return data.SpawnedBosses + data.SpawnedEnemies + data.SpawnedKamikaze;
        }

        public void IncreaseSpawnedEnemies(ProtoEntity spawnEntity, EnemyType enemyType)
        {
            if (enemyType == EnemyType.Default)
                throw new InvalidOperationException();
            
            ref EnemySpawnerDataComponent data = ref spawnEntity.GetEnemySpawnerData();
            data.LastSpawnedEnemyType = enemyType;

            switch (enemyType)
            {
                case EnemyType.Enemy:
                    data.SpawnedEnemies++;
                    break;
                case EnemyType.Boss:
                    data.SpawnedBosses++;
                    break;
                case EnemyType.Kamikaze:
                    data.SpawnedKamikaze++;
                    break;
                case EnemyType.Default:
                    throw new InvalidOperationException("Not expected enemy type");
            }
        }

        public void IncreaseCurrentWave(ProtoEntity spawnEntity)
        {
            ref EnemySpawnerDataComponent data = ref spawnEntity.GetEnemySpawnerData();
            data.WaweIndex++;
            ClearSpawnedEnemies(spawnEntity);
        }

        public int GetHealth(ProtoEntity spawnEntity, EnemyType type)
        {
            int currentWaveNumber = spawnEntity.GetEnemySpawnerData().WaweIndex;
            
            if (currentWaveNumber == 0)
                currentWaveNumber = 1;
            
            return type switch
            {
                EnemyType.Enemy => Config.StartEnemyHealth + Config.AddedEnemyHealth * currentWaveNumber,
                EnemyType.Boss => Config.StartBossHealth + Config.AddedBossHealth * currentWaveNumber,
                EnemyType.Kamikaze => Config.StartKamikazeHealth + Config.AddedKamikazeHealth * currentWaveNumber,
                _ => throw new InvalidOperationException("Not expected enemy type"),
            };
        }

        public int GetAttackPower(ProtoEntity spawnEntity, EnemyType type)
        {
            int currentWaveNumber = spawnEntity.GetEnemySpawnerData().WaweIndex;
            
            if (currentWaveNumber == 0)
                currentWaveNumber = 1;

            return type switch
            {
                EnemyType.Enemy => Config.StartEnemyAttackPower + Config.AddedEnemyAttackPower * currentWaveNumber,
                EnemyType.Boss => Config.StartBossAttackPower + Config.AddedBossAttackPower * currentWaveNumber,
                EnemyType.Kamikaze => Config.StartKamikazeHealth + Config.AddedKamikazeHealth * currentWaveNumber,
                _ => throw new InvalidOperationException("Not expected enemy type"),
            };
        }

        public int GetMassAttackPower(ProtoEntity spawnEntity, EnemyType type)
        {
            int currentWaveNumber = spawnEntity.GetEnemySpawnerData().WaweIndex;
            
            if (currentWaveNumber == 0)
                currentWaveNumber = 1;

            return type switch
            {
                EnemyType.Boss => Config.StartBossMassAttackPower + Config.AddedBossMassAttackPower * currentWaveNumber,
                EnemyType.Kamikaze => Config.StartKamikazeMassAttackPower + Config.AddedKamikazeMassAttackPower * currentWaveNumber,
                _ => throw new InvalidOperationException("Not expected enemy type"),
            };
        }

        private void ClearSpawnedEnemies(ProtoEntity spawnEntity)
        {
            ref EnemySpawnerDataComponent data = ref spawnEntity.GetEnemySpawnerData();

            data.SpawnedKamikaze = 0;
            data.SpawnedBosses = 0;
            data.SpawnedEnemies = 0;
        }

        public EnemyType GetNextEnemyType(ProtoEntity spawnEntity) =>
            _nextTypeServices[Config.SpawnLogic].GetNextEnemyType(spawnEntity);
    }
}