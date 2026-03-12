using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Data;
using Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Factories;
using Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.EnemySpawners.Controllers.SaveLoads
{
    [EcsSystem(1)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class EnemySpawnerLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISpawnService _spawnService;
        private readonly IEntityRepository _entityRepository;
        private readonly IDataService _dataService;
        private readonly RootGameObject _rootGameObject;
        private readonly EnemySpawnerEntityFactory _enemySpawnerEntityFactory;

        public EnemySpawnerLoadSystem(
            IUiViewService uiViewService,
            ISpawnService spawnService,
            IEntityRepository entityRepository,
            IDataService dataService,
            RootGameObject rootGameObject,
            EnemySpawnerEntityFactory enemySpawnerEntityFactory)
        {
            _uiViewService = uiViewService;
            _spawnService = spawnService;
            _entityRepository = entityRepository;
            _dataService = dataService;
            _rootGameObject = rootGameObject;
            _enemySpawnerEntityFactory = enemySpawnerEntityFactory;
        }
        
        public void Init(IProtoSystems systems)
        {
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();
            
            //Create
            ProtoEntity enemySpawnerEntity = _enemySpawnerEntityFactory.Create(_rootGameObject.EnemySpawner);
            _enemySpawnerEntityFactory.InitUiLink(gameplayUiView.EnemySpawner, enemySpawnerEntity);

            if (_dataService.HasKey(IdsConst.EnemySpawner) == false)
                return;
            
            //Load
            EnemySpawnerSaveData enemySpawnerSaveData = _dataService.LoadData<EnemySpawnerSaveData>(IdsConst.EnemySpawner);
            ProtoEntity enemySpawner = _entityRepository.GetByName(IdsConst.EnemySpawner);
            ref EnemySpawnerDataComponent enemySpawnerComponent = ref enemySpawner.GetEnemySpawnerData();
            enemySpawnerComponent.SpawnedEnemies = enemySpawnerSaveData.SpawnedEnemiesInCurrentWave;
            enemySpawnerComponent.LastSpawnedEnemyType = enemySpawnerSaveData.LastSpawnedEnemyType;
            enemySpawnerComponent.SpawnedBosses = enemySpawnerSaveData.SpawnedBossesInCurrentWave;
            enemySpawnerComponent.SpawnedKamikaze = enemySpawnerSaveData.SpawnedKamikazeInCurrentWave;
            enemySpawnerComponent.WaweIndex = enemySpawnerSaveData.WaveIndex;
            
            enemySpawner.GetEnemySpawnerUiModule().Value.SpawnerProgressSlider.value = _spawnService
                .GetSumEnemiesInCurrentWave(enemySpawner)
                .IntToPercent(_spawnService.GetSumEnemiesInCurrentWave(enemySpawner))
                .IntPercentToUnitPercent();
        }
    }
}