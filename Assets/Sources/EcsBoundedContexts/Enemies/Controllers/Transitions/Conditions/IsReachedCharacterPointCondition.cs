using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Transitions.Conditions
{
    [Category(NcCategoriesConst.Enemies)]
    public class IsReachedCharacterPointCondition : ConditionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }

        protected override bool OnCheck()
        {
            Vector3 enemyPosition = _entity.GetTransform().Value.position;
            Vector3 characterMeleePointPosition = _entity.GetCharacterMeleePoint().Value.GetTransform().Value.position;
            float stoppingDistance = _entity.GetNavMesh().Value.stoppingDistance + 0.15f;
            
            return Vector3.Distance(enemyPosition, characterMeleePointPosition) <= stoppingDistance;
        }
    }
}