using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Components;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data
{
    [EcsSystem(506)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class KillEnemyCounterSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                KillEnemyCounterComponent,
                SaveDataEvent>());        
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                KillEnemyCounterComponent,
                ClearDataEvent>());
        
        public KillEnemyCounterSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                int killedEnemies = entity.GetKillEnemyCounter().Value;

                KillEnemyCounterSaveData data = new KillEnemyCounterSaveData
                {
                    Id = IdsConst.KillEnemyCounter,
                    KilledEnemiesCount = killedEnemies,
                };
                
                _dataService.SaveData(data, IdsConst.KillEnemyCounter);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.KillEnemyCounter);
            }
        }
    }
}