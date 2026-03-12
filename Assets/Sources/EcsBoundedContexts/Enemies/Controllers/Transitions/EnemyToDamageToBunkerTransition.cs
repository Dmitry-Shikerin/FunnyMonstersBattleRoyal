using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Transitions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyToDamageToBunkerTransition : ConditionTask
    {
        private ProtoEntity _entity;
        private ProtoEntity _bunkerEntity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(IEntityRepository repository) =>
            _bunkerEntity = repository.GetByName(IdsConst.Bunker);

        protected override bool OnCheck()
        {
            Vector3 bunkerPosition = _bunkerEntity.GetTransform().Value.position;
            Vector3 enemyPosition = _entity.GetTransform().Value.position;
            float distance = Vector3.Distance(bunkerPosition, enemyPosition);
            float stoppingDistance = _entity.GetNavMesh().Value.stoppingDistance + 0.15f;
            
            return distance <= stoppingDistance;
        }
    }
}