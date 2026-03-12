using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Dictionaries;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services
{
    public class NextEnemyTypeWeightedService : INextEnemyTypeService
    {
        private readonly ISpawnService _spawnService;
        private readonly List<EnemyType> _enemyTypes = new();
        private readonly Dictionary<EnemyType, float> _weights = new();

        public NextEnemyTypeWeightedService(ISpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public EnemyType GetNextEnemyType(ProtoEntity spawnEntity)
        {
            List<EnemyType> availableTypes = GetAvailableEnemyTypes(spawnEntity);

            if (availableTypes.Count == 0)
                throw new InvalidOperationException("No available enemy types");

            if (availableTypes.Count == 1)
                return availableTypes[0];

            return GetByWeights(availableTypes, spawnEntity);
        }

        private List<EnemyType> GetAvailableEnemyTypes(ProtoEntity spawnEntity)
        {
            _enemyTypes.Clear();
            EnemySpawnerDataComponent data = spawnEntity.GetEnemySpawnerData();
            EnemySpawnerWave currentWave = _spawnService.GetCurrentWave(spawnEntity);
            
            if (data.SpawnedEnemies < currentWave.EnemyCount)
                _enemyTypes.Add(EnemyType.Enemy);

            if (data.SpawnedBosses < currentWave.BossesCount)
                _enemyTypes.Add(EnemyType.Boss);

            if (data.SpawnedKamikaze < currentWave.KamikazeEnemyCount)
                _enemyTypes.Add(EnemyType.Kamikaze);

            return _enemyTypes;
        }

        private EnemyType GetByWeights(List<EnemyType> availableTypes, ProtoEntity spawnEntity)
        {
            Dictionary<EnemyType, float> weights = CalculateWeights(availableTypes, spawnEntity);
            //TODO заменить эту линку
            float totalWeight = weights.Values.Sum();

            if (totalWeight <= 0)
            {
                return availableTypes.Count > 0 ? availableTypes[0] : EnemyType.Default;;
            }

            float randomValue = Random.Range(0, totalWeight);
            float currentWeight = 0f;

            foreach (EnemyType type in availableTypes)
            {
                currentWeight += weights[type];
                
                if (randomValue < currentWeight)
                    return type;
            }

            return availableTypes[0]; // fallback
        }

        private Dictionary<EnemyType, float> CalculateWeights(List<EnemyType> availableTypes, ProtoEntity spawnEntity)
        {
            _weights.Clear();
            EnemySpawnerDataComponent data = spawnEntity.GetEnemySpawnerData();
            EnemySpawnerWave currentWave = _spawnService.GetCurrentWave(spawnEntity);

            // Базовые веса (можно вынести в конфиг)
            WeightsDictionary baseWeights = _spawnService.Config.Weights;

            foreach (EnemyType type in availableTypes)
            {
                float weight = baseWeights[type];

                // Модификаторы весов в зависимости от ситуации
                switch (type)
                {
                    case EnemyType.Enemy:
                        weight = GetEnemyWeight(weight, data, currentWave);
                        break;
                    case EnemyType.Boss:
                        weight = GetBossWeight(weight, data, currentWave, spawnEntity);
                        break;
                    case EnemyType.Kamikaze:
                        weight = GetKamikazeWeight(weight, data, currentWave, spawnEntity);
                        break;
                }

                _weights[type] = weight;
            }

            return _weights;
        }

        private float GetEnemyWeight(float baseWeight, EnemySpawnerDataComponent data, EnemySpawnerWave currentWave)
        {
            float weight = baseWeight;

            // Увеличиваем вес обычных врагов, если другие типы почти закончились
            int remainingBosses = currentWave.BossesCount - data.SpawnedBosses;
            int remainingKamikaze = currentWave.KamikazeEnemyCount - data.SpawnedKamikaze;

            if (remainingBosses <= 0 && remainingKamikaze <= 0)
                weight += 50f; // Большой бонус, если боссы и камикадзе закончились

            return weight;
        }

        private float GetBossWeight(float baseWeight, EnemySpawnerDataComponent data, EnemySpawnerWave currentWave, ProtoEntity spawnEntity)
        {
            float weight = baseWeight;

            // Боссы чаще появляются в конце волны
            int totalEnemiesInWave = currentWave.EnemyCount + currentWave.BossesCount + currentWave.KamikazeEnemyCount;
            int remainingTotal = totalEnemiesInWave - _spawnService.GetSumSpawnedEnemies(spawnEntity);

            if (remainingTotal <= currentWave.BossesCount)
                weight += 30f; // Бонус в конце волны

            // Уменьшаем вес, если уже заспавнили много боссов
            float bossSpawnRatio = (float)data.SpawnedBosses / currentWave.BossesCount;
            weight *= (1f - bossSpawnRatio * 0.3f); // Уменьшаем до 30%

            return weight;
        }

        private float GetKamikazeWeight(float baseWeight, EnemySpawnerDataComponent data, EnemySpawnerWave currentWave, ProtoEntity spawnEntity)
        {
            float weight = baseWeight;

            // Камикадзе чаще в середине волны
            int totalEnemiesInWave = currentWave.EnemyCount + currentWave.BossesCount + currentWave.KamikazeEnemyCount;
            float waveProgress = (float)_spawnService.GetSumSpawnedEnemies(spawnEntity) / totalEnemiesInWave;

            // Пик веса в середине волны (30%-70% прогресса)
            if (waveProgress >= 0.3f && waveProgress <= 0.7f)
                weight += 15f;

            // Уменьшаем вес, если почти достигли лимита камикадзе
            float kamikazeSpawnRatio = (float)data.SpawnedKamikaze / currentWave.KamikazeEnemyCount;
            
            if (kamikazeSpawnRatio > 0.7f)
                weight *= 0.5f; // Сильно уменьшаем вес при接近лимита

            return weight;
        }
    }
}