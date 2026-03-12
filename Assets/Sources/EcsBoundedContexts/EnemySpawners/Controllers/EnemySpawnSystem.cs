using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.Enemies.Infrastructure.Factories;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services;
using Sources.EcsBoundedContexts.EnemySpawners.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.EnemySpawners.Controllers
{
    [EcsSystem(2)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class EnemySpawnSystem : IProtoInitSystem, IProtoDestroySystem, IProtoRunSystem
    {
        [DI] private readonly ProtoItExc _it = new(
            It.Inc<
                EnemyTypeComponent>(),
            It.Exc<InPoolComponent>());

        private readonly IAssetCollector _assetCollector;
        private readonly ISpawnService _spawnService;
        private readonly IEntityRepository _repository;
        private readonly EnemyKamikazeEntityFactory _enemyKamikazeEntityFactory;
        private readonly EnemyEntityFactory _enemyEntityFactory;
        private readonly EnemyBossEntityFactory _enemyBossEntityFactory;
        private CancellationTokenSource _token;
        private ProtoEntity _enemySpawner;
        private ProtoEntity _bunker;
        private EnemySpawnerConfig _enemySpawnerConfig;

        public EnemySpawnSystem(
            IAssetCollector assetCollector,
            ISpawnService spawnService,
            IEntityRepository repository,
            EnemyKamikazeEntityFactory enemyKamikazeEntityFactory,
            EnemyEntityFactory enemyEntityFactory,
            EnemyBossEntityFactory enemyBossEntityFactory)
        {
            _assetCollector = assetCollector;
            _spawnService = spawnService;
            _repository = repository;
            _enemyKamikazeEntityFactory = enemyKamikazeEntityFactory;
            _enemyEntityFactory = enemyEntityFactory;
            _enemyBossEntityFactory = enemyBossEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _enemySpawner = _repository.GetByName(IdsConst.EnemySpawner);
            _enemySpawnerConfig = _assetCollector.Get<EnemySpawnerConfig>();
            _token = new CancellationTokenSource();
            Spawn(_token.Token);
        }

        public void Destroy()
        {
            _token?.Cancel();
        }

        public void Run()
        {
            int wavesCount = _enemySpawnerConfig.Waves.Count;

            if (_token.IsCancellationRequested)
                return;

            if (_enemySpawner.GetEnemySpawnerData().WaweIndex >= wavesCount)
            {
                StopSpawn();
                return;
            }

            //TODO потом доработать
            _bunker = _repository.GetByName(IdsConst.Bunker);
            
            if (_bunker.HasHealth() == false)
            {
                StopSpawn();
            }
        }

        private async void Spawn(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);//сделано для того что бы успели инициализироваться энтити

                EnemySpawnerDataComponent data = _enemySpawner.GetEnemySpawnerData();
                EnemySpawnerUiModule module = _enemySpawner.GetEnemySpawnerUiModule().Value;
                //UI
                Slider slider = module.SpawnerProgressSlider;
                slider.value = 0;

                int startWave = data.WaweIndex;

                for (int i = startWave; i < _spawnService.Config.Waves.Count; i++)
                {
                    //UI
                    int waveNumber = _enemySpawner.GetEnemySpawnerData().WaweIndex + 1;
                    module.CurrentWaveText.text = waveNumber.ToString();
                    slider.value = 0;
                    int sumEnemies = _spawnService.GetSumEnemiesInCurrentWave(_enemySpawner);
                    int sumSpawnedEnemies = _spawnService.GetSumSpawnedEnemies(_enemySpawner);

                    for (int j = sumSpawnedEnemies; j < sumEnemies; j++)
                    {
                        await SpawnEnemyByType(cancellationToken);
                        await Wait(cancellationToken);

                        //UI
                        slider.value = _spawnService
                            .GetSumSpawnedEnemies(_enemySpawner)
                            .IntToPercent(sumEnemies)
                            .IntPercentToUnitPercent();
                    }

                    _spawnService.IncreaseCurrentWave(_enemySpawner);

                    if (_enemySpawner.HasWaveCompletedEvent())
                        continue;
                    
                    _enemySpawner.AddWaveCompletedEvent();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void StopSpawn()
        {
            _token?.Cancel();

            //TODO подумать над этим
            foreach (ProtoEntity entity in _it)
            {
                if (entity.HasDamageEvent())
                {
                    entity.ReplaceDamageEvent(1000);
                    return;
                }

                entity.AddDamageEvent(1000);
            }
        }

        private async UniTask SpawnEnemyByType(CancellationToken cancellationToken)
        {
            ProtoEntity spawnPoint = _spawnService.GetRandomSpawnPoint(_enemySpawner);
            EnemyType nextType = _spawnService.GetNextEnemyType(_enemySpawner);

            switch (nextType)
            {
                case EnemyType.Boss:
                    SpawnBoss(spawnPoint);
                    break;
                case EnemyType.Kamikaze:
                    SpawnEnemyKamikaze(spawnPoint);
                    break;
                case EnemyType.Enemy:
                    SpawnEnemy(spawnPoint);
                    break;
                case EnemyType.Default:
                    throw new InvalidOperationException("Not expected type");
            }
            
            _spawnService.IncreaseSpawnedEnemies(_enemySpawner, nextType);

            await UniTask.Yield(cancellationToken);
        }

        private async UniTask Wait(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                _spawnService.GetCurrentWave(_enemySpawner).SpawnDelay), cancellationToken: cancellationToken);
        }

        //TODO обобщить эти методы

        private void SpawnEnemy(ProtoEntity spawnPoint)
        {
            Vector3 position = spawnPoint.GetTransform().Value.position;
            ProtoEntity entity = _enemyEntityFactory.Create(position);
            int health = _spawnService.GetHealth(_enemySpawner, EnemyType.Enemy);

            if (entity.HasHealth())
                entity.ReplaceHealth(health);
            else
                entity.AddHealth(health);

            int attackPower = _spawnService.GetAttackPower(_enemySpawner, EnemyType.Enemy);
            entity.ReplaceAttackPower(attackPower);
            entity.ReplaceMaxHealth(health);
            entity.GetBehaviourTreeOwner().Value.StartBehaviour();
            entity.GetHealthBar().Value.fillAmount = 1f;
            ProtoEntity characterMeleePoint = spawnPoint.GetTargetCharactersPoints().Melee;
            entity.AddCharacterMeleePoint(characterMeleePoint);
        }

        private void SpawnEnemyKamikaze(ProtoEntity spawnPoint)
        {
            Vector3 position = spawnPoint.GetTransform().Value.position;
            ProtoEntity entity = _enemyKamikazeEntityFactory.Create(position);
            int health = _spawnService.GetHealth(_enemySpawner, EnemyType.Kamikaze);

            if (entity.HasHealth())
                entity.ReplaceHealth(health);
            else
                entity.AddHealth(health);

            int massAttackPower = _spawnService.GetMassAttackPower(_enemySpawner, EnemyType.Kamikaze);
            entity.ReplaceMassAttackPower(massAttackPower);
            //Debug.Log($"Health {health}");
            entity.ReplaceMaxHealth(health);
            entity.GetBehaviourTreeOwner().Value.StartBehaviour();
            entity.GetHealthBar().Value.fillAmount = 1f;
            ProtoEntity characterMeleePoint = spawnPoint.GetTargetCharactersPoints().Melee;
            entity.AddCharacterMeleePoint(characterMeleePoint);
        }

        private void SpawnBoss(ProtoEntity spawnPoint)
        {
            Vector3 position = spawnPoint.GetTransform().Value.position;
            ProtoEntity entity = _enemyBossEntityFactory.Create(position);
            int health = _spawnService.GetHealth(_enemySpawner, EnemyType.Boss);

            if (entity.HasHealth())
                entity.ReplaceHealth(health);
            else
                entity.AddHealth(health);

            int attackPower = _spawnService.GetAttackPower(_enemySpawner, EnemyType.Boss);
            entity.ReplaceAttackPower(attackPower);
            int massAttackPower = _spawnService.GetMassAttackPower(_enemySpawner, EnemyType.Boss);
            entity.ReplaceMassAttackPower(massAttackPower);
            entity.ReplaceMaxHealth(health);
            entity.GetBehaviourTreeOwner().Value.StartBehaviour();
            entity.GetHealthBar().Value.fillAmount = 1f;
            ProtoEntity characterMeleePoint = spawnPoint.GetTargetCharactersPoints().Melee;
            entity.AddCharacterMeleePoint(characterMeleePoint);
        }
    }
}