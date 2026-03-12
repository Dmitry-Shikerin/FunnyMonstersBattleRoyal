using System;
using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyMoveToBunkerAction : ActionTask
    {
        private ProtoEntity _entity;
        private ProtoEntity _bunkerEntity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(IEntityRepository repository) =>
            _bunkerEntity = repository.GetByName(IdsConst.Bunker);

        protected override void OnExecute()
        {
            _entity.AddFindCharacters();
            _entity.PlayAnimation(GetAnimName());
            Vector3 position = _bunkerEntity.GetTransform().Value.position;

            if (_entity.HasTargetPoint())
                _entity.ReplaceTargetPoint(position);
            else
                _entity.AddTargetPoint(position);
        }
        
        private AnimationName GetAnimName()
        {
            return _entity.GetEnemyType().Value switch
            {
                EnemyType.Enemy => AnimationName.EnemyUnarmedRunForward,
                EnemyType.Kamikaze => AnimationName.KamikazeUnarmedRunForward,
                EnemyType.Boss => AnimationName.BossUnarmedRunForward,
                _ => throw new NullReferenceException(),
            };
        }

        protected override void OnStop()
        {
            _entity.DelFindCharacters();
        }
    }
}