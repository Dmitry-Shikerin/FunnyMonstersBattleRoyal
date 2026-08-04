using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsGroundedCondition : ConditionTask
    {
        private IEntityRepository _repository;
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }

        protected override bool OnCheck() =>
            _entity.HasGrounded();
    }
}