using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class CompleteTutorialUiAction : UiAction
    {
        private IEntityRepository _entityRepository;
        private ProtoEntity _tutorial;

        public override UiActionId Id => UiActionId.CompleteTutorial;

        [Inject]
        private void Construct(IEntityRepository entityRepository) =>
            _entityRepository = entityRepository;

        public override void Handle()
        {
            _tutorial = _entityRepository.GetByName(IdsConst.Tutorial);
            _tutorial.AddComplete();
            _tutorial.AddSaveDataEvent();
        }
    }
}