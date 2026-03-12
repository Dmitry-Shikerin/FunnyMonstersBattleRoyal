using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(53)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class FindEnemiesSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                FindEnemiesComponent,
                EnemiesFindRangeComponent>());
        [DI] private readonly ProtoItExc _enemiesIt = new(
            It.Inc<
                EnemyTypeComponent>(),
            It.Exc<
                InPoolComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                foreach (ProtoEntity enemyEntity in _enemiesIt)
                {
                    if (entity.HasTargetEnemy())
                        continue;

                    if (HasPosition(entity, enemyEntity) == false)
                        continue;

                    entity.DelFindEnemies();
                    entity.AddTargetEnemy(enemyEntity);
                }
            }
        }

        private bool HasPosition(ProtoEntity character, ProtoEntity enemy)
        {
            Vector3 characterPosition = character.GetTransform().Value.position;
            Vector3 enemyPosition = enemy.GetTransform().Value.position;
            float distance = Vector3.Distance(characterPosition, enemyPosition);
            float range = character.GetEnemiesFindRange().Value;

            return distance < range;
        }
    }
}