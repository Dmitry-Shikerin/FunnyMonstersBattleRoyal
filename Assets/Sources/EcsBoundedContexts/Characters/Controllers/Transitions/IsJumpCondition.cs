using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsJumpCondition : ConditionTask
    {
        private IEntityRepository _repository;
        private ProtoEntity _input;

        protected override string OnInit()
        {
            _input = _repository.GetByName(IdsConst.Input);
            return null;
        }

        [Inject]
        private void Construct(IEntityRepository repository)
        {
            _repository = repository;
        }

        protected override bool OnCheck() =>
            _input.HasJumpEvent();
    }
}