using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Transitions.Conditions
{
    [Category(NcCategoriesConst.Enemies)]
    public class HasTargetPointCondition : ConditionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override bool OnCheck() =>
            _entity.HasTargetPoint();
    }
}