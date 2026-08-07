using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.BoundedContexts.Hud.Controllers.Conditions
{
    [Category(NcCategoriesConst.Ui)]
    public class HasChangedSettings : ConditionTask
    {
        private IEntityRepository _repository;
        private ProtoEntity _entity;

        protected override string OnInit()
        {
            _entity = _repository.GetByName(IdsConst.Settings); 
            return null;
        }

        [Inject]
        private void Construct(IEntityRepository repository) =>
            _repository = repository;

        protected override bool OnCheck() =>
            _entity.HasChangedSettings();
    }
}