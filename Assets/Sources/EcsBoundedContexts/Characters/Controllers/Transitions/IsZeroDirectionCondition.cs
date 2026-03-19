using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Transitions
{
    [Category(NcCategoriesConst.Characters)]
    public class IsZeroDirectionCondition : ConditionTask
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
            _input.GetDirection().Value == Vector3.zero;
    }
}