using System;
using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.EcsBoundedContexts.GameCompleted.Controllers
{
    [EcsSystem(67)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class GameCompletedSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;
        private readonly IStorageService _storageService;
        private readonly ISdkService _sdkService;
        private ProtoEntity _enemySpawner;
        private ProtoEntity _killEnemyCounter;
        private bool _isCompleted;
        private EnemySpawnerConfig _enemySpawnerConfig;

        public GameCompletedSystem(
            IUiViewService uiViewService,
            IAssetCollector assetCollector,
            IEntityRepository repository,
            IStorageService storageService,
            ISdkService sdkService)
        {
            _uiViewService = uiViewService;
            _assetCollector = assetCollector;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _sdkService = sdkService;
        }

        public void Init(IProtoSystems systems)
        {
            _enemySpawner = _repository.GetByName(IdsConst.EnemySpawner);
            _killEnemyCounter = _repository.GetByName(IdsConst.KillEnemyCounter);
            _enemySpawnerConfig = _assetCollector.Get<EnemySpawnerConfig>();
        }

        public void Run()
        {
            OnCompleted();
        }

        private void OnCompleted()
        {
            if (_isCompleted)
                return;

            int wavesCount = _enemySpawnerConfig.Waves.Count;
            
            if (_enemySpawner.GetEnemySpawnerData().WaweIndex < wavesCount)
                return;
            
            int score = _killEnemyCounter.GetKillEnemyCounter().Value;
            _sdkService.SetPlayerScore(score);
            _storageService.ClearAll();
            //TODO обобщить GameOver и GameCompleted или сделать одну систему
            _uiViewService.Show(UiViewId.GameCompleted);
            _uiViewService.Get<GameCompletedUiView>().ScoreText.text = score.ToString();
            _isCompleted = true;
        }
    }
}