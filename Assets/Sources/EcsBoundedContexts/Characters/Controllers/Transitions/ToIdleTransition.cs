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
    public class ToIdleTransition : ConditionTask
    {
        private ProtoEntity _entity;
        
        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override bool OnCheck()
        {
            if (_entity.HasGrounded() == false)
                return false;
            
            if (_entity.GetDirection().Value == Vector3.zero)
                return true;

            //Debug.Log($"{_entity.GetSpeed().Value}");
            return _entity.GetSpeed().Value == 0;
        }
    }
}