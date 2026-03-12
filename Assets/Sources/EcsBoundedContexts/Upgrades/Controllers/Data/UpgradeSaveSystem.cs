using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Upgrades.Controllers.Data
{
    [EcsSystem(502)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class UpgradeSaveSystem : IProtoRunSystem 
    {
        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                UpgradeTag,
                SaveDataEvent>());
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                UpgradeTag,
                ClearDataEvent>());

        private readonly IDataService _dataService;

        public UpgradeSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                int index = entity.GetUpgradeConfig().Index;
                string id = entity.GetStringId().Value;

                UpgradeSaveData upgradeSaveData = new()
                {
                    Id = id,
                    UpgradeIndex = index,
                };
                
                _dataService.SaveData(upgradeSaveData, id);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                string id = entity.GetStringId().Value;
                _dataService.Clear(id);
            }
        }
    }
}