using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.HealthBoosters.Domain.Components;
using Sources.EcsBoundedContexts.HealthBoosters.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.HealthBoosters.Controllers.Data
{
    [EcsSystem(507)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class HealthBusterSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                HealthBusterComponent,
                SaveDataEvent>());

        public HealthBusterSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                int healthBusters = entity.GetHealthBuster().Value;

                HealthBusterSaveData data = new HealthBusterSaveData
                {
                    Id = IdsConst.HealthBooster,
                    Amount = healthBusters,
                    IsFirstUsedCompleted = entity.HasFirstUsedCompleted(),
                };
                
                _dataService.SaveData(data, IdsConst.HealthBooster);
            }
        }
    }
}