using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Systems
{
    [EcsSystem(62)]
    [ComponentGroup(ComponentGroup.Enemy)]
    [Aspect(AspectName.Game)]
    public class ExplodeSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                EnemyKamikazeTag,
                ExplodeComponent>());
        [DI] private readonly ProtoItExc _charactersIt = new(
            It.Inc<
                CharacterTag>(),
            It.Exc<
                InPoolComponent>());

        private readonly IAssetCollector _assetCollector;
        private EnemyKamikazeConfig _config;

        public ExplodeSystem(
            IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<EnemyKamikazeConfig>();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Vector3 enemyPosition = entity.GetTransform().Value.position;
                int damage = entity.GetMassAttackPower().Value;

                foreach (ProtoEntity characterEntity in _charactersIt)
                {
                    if (entity.HasTargetEnemy())
                        continue;

                    Vector3 position = characterEntity.GetTransform().Value.position;
                    float distance = Vector3.Distance(enemyPosition, position);

                    if (distance > _config.MassAttackFindRange)
                        continue;

                    characterEntity.AddDamageEvent(damage);
                    Debug.Log($"Add Damage {damage}");
                }

                Debug.Log($"Explode");
                entity.DelExplode();
            }
        }
    }
}