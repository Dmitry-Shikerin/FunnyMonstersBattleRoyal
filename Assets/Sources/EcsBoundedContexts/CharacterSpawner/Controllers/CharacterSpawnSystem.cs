using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.CharacterSpawner.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Controllers
{
    [EcsSystem(57)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class CharacterSpawnSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IEntityRepository _repository;
        private readonly CharacterMeleeEntityFactory _characterMeleeEntityFactory;
        private readonly CharacterRangeEntityFactory _characterRangeEntityFactory;

        [DI] private readonly ProtoIt _applyAbilityIt = new(
            It.Inc<
                CharacterSpawnerTag,
                ApplyAbilityEvent>());
        [DI] private readonly ProtoItExc _rangeIt = new(
            It.Inc<
                CharacterRangeTag>(),
            It.Exc<
                InPoolComponent>());
        [DI] private readonly ProtoItExc _meleeIt = new(
            It.Inc<
                CharacterMeleeTag>(),
            It.Exc<
                InPoolComponent>());

        private ProtoEntity _spawner;

        public CharacterSpawnSystem(
            IEntityRepository repository,
            CharacterMeleeEntityFactory characterMeleeEntityFactory,
            CharacterRangeEntityFactory characterRangeEntityFactory)
        {
            _repository = repository;
            _characterMeleeEntityFactory = characterMeleeEntityFactory;
            _characterRangeEntityFactory = characterRangeEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            _spawner = _repository.GetByName(IdsConst.CharacterSpawnerAbility);
            Spawn(_spawner);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _applyAbilityIt)
                Spawn(entity);
        }

        private async void Spawn(ProtoEntity entity)
        {
            DespawnMelee();
            DespawnRange();
            await UniTask.Yield();
            await UniTask.Yield(); //сделано для того чтобы объект успел выключится с помощью системы
            await UniTask.Yield();
            SpawnMelee(entity);
            SpawnRange(entity);
        }

        private void DespawnMelee()
        {
            foreach (ProtoEntity entity in _meleeIt)
            {
                if (entity.HasDamageEvent())
                {
                    entity.ReplaceDamageEvent(1000);
                    continue;
                }

                entity.AddDamageEvent(1000); // костыль
            }
        }

        private void DespawnRange()
        {
            foreach (ProtoEntity entity in _rangeIt)
            {
                if (entity.HasDamageEvent())
                {
                    entity.ReplaceDamageEvent(1000);
                    continue;
                }

                entity.AddDamageEvent(1000);
            }
        }

        private void SpawnMelee(ProtoEntity spawner)
        {
            List<ProtoEntity> points = spawner.GetCharactersSpawnPoints().MeleeSpawnPoints;
            ProtoEntity healthUpgradeEntity = _repository.GetByName(IdsConst.HealthUpgrade);
            UpgradeConfigComponent config = healthUpgradeEntity.GetUpgradeConfig();
            int health = (int)config.Value.Levels[config.Index].CurrentAmount;
            UpgradeConfigComponent attackConfig = _repository.GetByName(IdsConst.AttackUpgrade).GetUpgradeConfig();
            int attack = (int)attackConfig.Value.Levels[config.Index].CurrentAmount;

            foreach (ProtoEntity spawnPoint in points)
            {
                Vector3 position = spawnPoint.GetTransform().Value.position;
                ProtoEntity entity = _characterMeleeEntityFactory.Create(position);

                if (entity.HasHealth())
                    entity.ReplaceHealth(health);
                else
                    entity.AddHealth(health);

                entity.ReplaceAttackPower(attack);
                entity.ReplaceMaxHealth(health);
                entity.GetHealthBar().Value.fillAmount = 1;
                entity.GetBehaviourTreeOwner().Value.StartBehaviour();
            }
        }

        private void SpawnRange(ProtoEntity spawner)
        {
            List<ProtoEntity> points = spawner.GetCharactersSpawnPoints().RangeSpawnPoints;
            ProtoEntity healthUpgradeEntity = _repository.GetByName(IdsConst.HealthUpgrade);
            UpgradeConfigComponent config = healthUpgradeEntity.GetUpgradeConfig();
            int health = (int)config.Value.Levels[config.Index].CurrentAmount;
            UpgradeConfigComponent attackConfig = _repository.GetByName(IdsConst.AttackUpgrade).GetUpgradeConfig();
            int attack = (int)attackConfig.Value.Levels[config.Index].CurrentAmount;

            foreach (ProtoEntity spawnPoint in points)
            {
                Vector3 position = spawnPoint.GetTransform().Value.position;
                ProtoEntity entity = _characterRangeEntityFactory.Create(position);

                if (entity.HasHealth())
                    entity.ReplaceHealth(health);
                else
                    entity.AddHealth(health);

                entity.ReplaceAttackPower(attack);
                entity.ReplaceMaxHealth(health);
                entity.GetHealthBar().Value.fillAmount = 1;
                entity.GetBehaviourTreeOwner().Value.StartBehaviour();
            }
        }
    }
}