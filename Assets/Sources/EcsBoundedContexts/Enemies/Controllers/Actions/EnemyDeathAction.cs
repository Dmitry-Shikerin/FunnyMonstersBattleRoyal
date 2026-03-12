using System;
using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Constants;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Components;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyDeathAction : ActionTask
    {
        private ProtoEntity _playerWallet;
        private ProtoEntity _killEnemyCounter;
        private ProtoEntity _entity;
        private ExplosionBodyBloodyEntityFactory _explosionBodyBloodyEntityFactory;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(
            IEntityRepository entityRepository,
            ExplosionBodyBloodyEntityFactory explosionBodyBloodyEntityFactory)
        {
            _playerWallet = entityRepository.GetByName(IdsConst.PlayerWallet);
            _killEnemyCounter = entityRepository.GetByName(IdsConst.KillEnemyCounter);
            _explosionBodyBloodyEntityFactory = explosionBodyBloodyEntityFactory;
        }

        protected override void OnExecute()
        {
            Vector3 spawnPosition = _entity.GetTransform().Value.position + Vector3.up;
            _explosionBodyBloodyEntityFactory.Create(spawnPosition);
            ref KillEnemyCounterComponent killEnemyCounter = ref _killEnemyCounter.GetKillEnemyCounter();
            killEnemyCounter.Value++;
            int reward = _entity.GetEnemyType().Value switch
            {
                EnemyType.Enemy => EnemyConst.EnemyReward,
                EnemyType.Kamikaze => EnemyConst.KamikazeReward,
                EnemyType.Boss => EnemyConst.BossReward,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (_playerWallet.HasIncreaseCoinsEvent())
            {
                ref var addCoins = ref _playerWallet.GetIncreaseCoinsEvent();
                addCoins.Value += reward;
            }
            else
            {
                _playerWallet.AddIncreaseCoinsEvent(reward);
            }

            if (_entity.HasTargetPoint())
                _entity.DelTargetPoint();
            
            _entity.DelInitialized();
            _entity.DelReachedMeleePoint();
            _entity.DelCharacterMeleePoint();
            _entity.GetReturnToPoolAction().ReturnToPool.Invoke();
        }
    }
}