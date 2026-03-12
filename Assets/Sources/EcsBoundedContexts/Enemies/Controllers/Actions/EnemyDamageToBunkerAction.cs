using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyDamageToBunkerAction : ActionTask
    {
        private ProtoEntity _entity;
        private ProtoEntity _bunkerEntity;
        private ExplosionBodyBloodyEntityFactory _explosionBodyBloodyEntityFactory;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(
            IEntityRepository repository,
            ExplosionBodyBloodyEntityFactory explosionBodyBloodyEntityFactory)
        {
            _bunkerEntity = repository.GetByName(IdsConst.Bunker);
            _explosionBodyBloodyEntityFactory = explosionBodyBloodyEntityFactory;
        }

        protected override void OnExecute()
        {
            if (_bunkerEntity.HasDamageEvent())
            {
                ref DamageEvent damage = ref _bunkerEntity.GetDamageEvent();
                damage.Value++;
            }
            else
            {
                _bunkerEntity.AddDamageEvent(1);
            }
            
            if (_entity.HasTargetPoint())
                _entity.DelTargetPoint();
            
            Vector3 spawnPosition = _entity.GetTransform().Value.position + Vector3.up;
            _explosionBodyBloodyEntityFactory.Create(spawnPosition);
            
            _entity.DelInitialized();
            _entity.DelCharacterMeleePoint();
            _entity.DelReachedMeleePoint();
            _entity.GetReturnToPoolAction().ReturnToPool.Invoke();
        }
    }
}