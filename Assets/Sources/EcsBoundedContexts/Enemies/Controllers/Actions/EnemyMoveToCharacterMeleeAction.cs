using System;
using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [Category(NcCategoriesConst.Enemies)]
    public class EnemyMoveToCharacterMeleeAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnExecute()
        {
            _entity.AddFindCharacters();
            _entity.PlayAnimation(GetAnimName());
            Vector3 position = _entity.GetCharacterMeleePoint().Value.GetTransform().Value.position;
            
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