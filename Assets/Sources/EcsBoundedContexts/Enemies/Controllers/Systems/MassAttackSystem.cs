using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Systems
{
    [EcsSystem(65)]
    [ComponentGroup(ComponentGroup.Enemy)]
    [Aspect(AspectName.Game)]
    public class MassAttackSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;
        private EnemyBossConfig _config;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                MassAttackTimerComponent,
                MassAttackParticleComponent>());
        [DI] private readonly ProtoItExc _charactersIt = new(
            It.Inc<
                CharacterTag>(),
            It.Exc<InPoolComponent>());

        public MassAttackSystem(
            IAssetCollector assetCollector,
            IEntityRepository repository)
        {
            _assetCollector = assetCollector;
            _repository = repository;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<EnemyBossConfig>();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref MassAttackTimerComponent massAttackTimer = ref entity.GetMassAttackTimer();
                massAttackTimer.Value -= Time.deltaTime;

                if (massAttackTimer.Value > 0)
                    continue;

                float delay = _config.MassAttackDelay;
                entity.ReplaceMassAttackTimer(delay);
                entity.GetMassAttackParticle().Value.Play();
                PlayMassAttack(entity);
            }
        }

        private void PlayMassAttack(ProtoEntity entity)
        {
            int attack = entity.GetMassAttackPower().Value;

            foreach (ProtoEntity character in _charactersIt)
            {
                if (HasDistance(entity, character) == false)
                    continue;
                
                if (character.HasDamageEvent())
                {
                    ref DamageEvent damageEvent = ref character.GetDamageEvent();
                    damageEvent.Value += attack;

                    continue;
                }
                
                character.AddDamageEvent(attack);
            }
        }

        private bool HasDistance(ProtoEntity enemy, ProtoEntity character)
        {
            Vector3 characterPosition = character.GetTransform().Value.position;
            Vector3 enemyPosition = enemy.GetTransform().Value.position;
            float distance = Vector3.Distance(characterPosition, enemyPosition);

            return distance < _config.MassAttackFindRange;
        }
    }
}