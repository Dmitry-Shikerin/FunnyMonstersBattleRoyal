using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.SaveAfterWaves.Controllers
{
    [EcsSystem(86)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class SaveAfterWaveSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                EnemySpawnerTag,
                WaveCompletedEvent>());

        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;
        private readonly IUiPopUpService _uiPopUpService;
        private readonly IStorageService _storageService;
        private ProtoEntity _enemySpawner;
        private EnemySpawnerConfig _enemySpawnerConfig;
        private ProtoEntity _bunker;

        public SaveAfterWaveSystem(
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            IUiPopUpService uiPopUpService,  
            IStorageService storageService)
        {
            _assetCollector = assetCollector;
            _repository = entityRepository;
            _uiPopUpService = uiPopUpService;
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public void Init(IProtoSystems systems)
        {
            _enemySpawner = _repository.GetByName(IdsConst.EnemySpawner);
            _enemySpawnerConfig = _assetCollector.Get<EnemySpawnerConfig>();
            _bunker = _repository.GetByName(IdsConst.Bunker);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                //TODO сделать гейм ентити и дабовить на нее состояние и использовать для проверки ее
                int wavesCount = _enemySpawnerConfig.Waves.Count;
            
                if (_enemySpawner.GetEnemySpawnerData().WaweIndex >= wavesCount)
                    return;
                
                if (_bunker.HasHealth() == false)
                    return;
                
                _storageService.SaveAll();
                _uiPopUpService.Show(UiPopUpId.SaveGame);
            }
        }
    }
}