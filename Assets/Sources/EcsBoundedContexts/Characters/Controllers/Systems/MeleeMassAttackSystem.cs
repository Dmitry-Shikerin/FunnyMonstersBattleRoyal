using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(54)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class MeleeMassAttackSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                MassAttackEvent>());
        [DI] private readonly ProtoIt _enemyIt = new(
            It.Inc<
                EnemyTypeComponent>());

        private readonly IAssetCollector _assetCollector;
        private CharacterMeleeConfig _config;

        public MeleeMassAttackSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<CharacterMeleeConfig>();
        }

        public void Run()                                                       
        {
            foreach (ProtoEntity entity in _it)
            {
                foreach (ProtoEntity enemy in _enemyIt)
                {
                    if (HasDistance(entity, enemy) == false)
                        continue;
                    
                    int attack = entity.GetAttackPower().Value;
                    
                    if (enemy.HasDamageEvent())
                    {
                        ref DamageEvent damageEvent = ref enemy.GetDamageEvent();
                        damageEvent.Value += attack;
                        
                        continue;
                    }

                    enemy.AddDamageEvent(attack);
                }
            }
        }

        private bool HasDistance(ProtoEntity character, ProtoEntity enemy)
        {
            Vector3 characterPosition = character.GetTransform().Value.position;
            Vector3 enemyPosition = enemy.GetTransform().Value.position;
            float distance = Vector3.Distance(characterPosition, enemyPosition);
            
            return distance < _config.MassAttackRange;
        }
    }
}