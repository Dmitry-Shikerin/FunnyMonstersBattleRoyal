using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Tutorials.Domain.Data;
using Sources.EcsBoundedContexts.Tutorials.Infrastructure;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Tutorials.Controllers.Data
{
    [EcsSystem(9)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class TutorialLoadSystem : IProtoInitSystem
    {
        private readonly IDataService _dataService;
        private readonly TutorialEntityFactory _tutorialEntityFactory;

        public TutorialLoadSystem(
            IDataService dataService,
            TutorialEntityFactory tutorialEntityFactory)
        {
            _dataService = dataService;
            _tutorialEntityFactory = tutorialEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            //Create
            ProtoEntity entity = _tutorialEntityFactory.Create(null);

            if (_dataService.HasKey(IdsConst.Tutorial) == false)
                return;

            //Tutorial
            TutorialSaveData tutorialSaveData = _dataService.LoadData<TutorialSaveData>(IdsConst.Tutorial);

            if (tutorialSaveData.IsCompleted)
                entity.AddComplete();
        }
    }
}