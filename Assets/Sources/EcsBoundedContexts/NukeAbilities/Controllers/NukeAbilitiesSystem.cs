using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.NukeAbilities.Domain;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Controllers
{
    [EcsSystem(59)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class NukeAbilitiesSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                NukeAbilityTag,
                ApplyAbilityEvent>());
        [DI] private readonly ProtoIt _nukeBombCompletePathIt = new(
            It.Inc<
                NukeBombTag,
                CompleteMoveAlongPathEvent>());
        [DI] private readonly ProtoItExc _enemyIt = new(
            It.Inc<
                EnemyTypeComponent>(),
            It.Exc<
                InPoolComponent>());

        private readonly IEntityRepository _entityRepository;

        public NukeAbilitiesSystem(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ProtoEntity nukeBomb = entity.GetNukeBombLink().Value;
                Vector3 targetPoint = nukeBomb.GetPointPath().Points[1];
                Vector3 startPoint = nukeBomb.GetPointPath().Points[0];
                nukeBomb.GetTransform().Value.position = startPoint;
                nukeBomb.AddTargetPoint(targetPoint);
                nukeBomb.ReplaceTargetPointIndex(0);
                nukeBomb.AddEnableGameObjectEvent();
            }

            foreach (ProtoEntity entity in _nukeBombCompletePathIt)
            {
                ProtoEntity nukeEntity = _entityRepository.GetByName(IdsConst.NukeAbility);
                nukeEntity.GetNukeParticle().Value.Play();
                entity.AddDisableGameObjectEvent();
                DealDamage();
            }
        }

        private void DealDamage()
        {
            ProtoEntity nukeEntity = _entityRepository.GetByName(IdsConst.NukeAbility);
            
            foreach (ProtoEntity enemyEntity in _enemyIt)
            {
                if (IsInBounds(enemyEntity, nukeEntity) == false)
                    continue;

                //TODO доработать
                if (enemyEntity.HasDamageEvent())
                {
                    enemyEntity.ReplaceDamageEvent(1000);
                    continue;
                }
                
                enemyEntity.AddDamageEvent(1000);
            }
        }

        private bool IsInBounds(ProtoEntity enemyEntity, ProtoEntity nukeEntity)
        {
            Vector3 enemyPosition = enemyEntity.GetTransform().Value.position;
            BoxCollider boxCollider = nukeEntity.GetNukeDamageCollider().Value;
            
            return boxCollider.bounds.Contains(enemyPosition);
        }
    }
}