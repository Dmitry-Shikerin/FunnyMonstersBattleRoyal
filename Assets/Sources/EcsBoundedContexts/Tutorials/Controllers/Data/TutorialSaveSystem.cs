using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Tutorials.Domain.Components;
using Sources.EcsBoundedContexts.Tutorials.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Tutorials.Controllers.Data
{
    [EcsSystem(503)]
    [ComponentGroup(ComponentGroup.Tutorial)]
    [Aspect(AspectName.Game)]
    public class TutorialSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                TutorialTag,
                SaveDataEvent>());

        public TutorialSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                if (entity.HasComplete() == false)
                    continue;

                TutorialSaveData data = new TutorialSaveData
                {
                    Id = IdsConst.Tutorial,
                    IsCompleted = true,
                };
                
                _dataService.SaveData(data, IdsConst.Tutorial);
            }
        }
    }
}