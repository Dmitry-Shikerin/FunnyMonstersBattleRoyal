using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Bunker.Domain.Components;
using Sources.EcsBoundedContexts.Bunker.Domain.Data;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Bunker.Controllers.SaveDatas
{
    [EcsSystem(512)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class BunkerSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                BunkerTag,
                SaveDataEvent>());        
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                BunkerTag,
                ClearDataEvent>());

        public BunkerSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                int health = entity.GetHealth().Value;

                BunkerSaveData data = new BunkerSaveData()
                {
                    Id = IdsConst.Bunker,
                    Health = health,
                };
                _dataService.SaveData(data, IdsConst.Bunker);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.Bunker);
            }
        }
    }
}