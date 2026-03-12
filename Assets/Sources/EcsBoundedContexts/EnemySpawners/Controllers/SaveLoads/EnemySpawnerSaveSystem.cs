using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.EnemySpawners.Controllers.SaveLoads
{
    [EcsSystem(509)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class EnemySpawnerSaveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                EnemySpawnerTag,
                SaveDataEvent>());        
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                EnemySpawnerTag,
                ClearDataEvent>());
        
        private readonly IDataService _dataService;

        public EnemySpawnerSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                EnemySpawnerDataComponent enemySpawnerComponent = entity.GetEnemySpawnerData();

                EnemySpawnerSaveData data = new EnemySpawnerSaveData
                {
                    Id = IdsConst.EnemySpawner,
                    SpawnedEnemiesInCurrentWave = enemySpawnerComponent.SpawnedEnemies,
                    LastSpawnedEnemyType = enemySpawnerComponent.LastSpawnedEnemyType,
                    SpawnedBossesInCurrentWave = enemySpawnerComponent.SpawnedBosses,
                    SpawnedKamikazeInCurrentWave = enemySpawnerComponent.SpawnedKamikaze,
                    WaveIndex = enemySpawnerComponent.WaweIndex,
                };
                
                _dataService.SaveData(data, IdsConst.EnemySpawner);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.EnemySpawner);
            }
        }
    }
}