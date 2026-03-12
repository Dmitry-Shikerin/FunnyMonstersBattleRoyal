using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class ToCharacterIdleTransition : ConditionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override bool OnCheck()
        {
            Vector3 characterPosition = _entity.GetTransform().Value.position;
            float range = _entity.GetEnemiesFindRange().Value;
            Vector3 enemyPosition = default;

            if (_entity.HasTargetEnemy())
                enemyPosition = _entity.GetTargetEnemy().Value.GetTransform().Value.position;
            
            return _entity.HasTargetEnemy() == false || Vector3.Distance(
                characterPosition, enemyPosition) > range;
        }
    }
}