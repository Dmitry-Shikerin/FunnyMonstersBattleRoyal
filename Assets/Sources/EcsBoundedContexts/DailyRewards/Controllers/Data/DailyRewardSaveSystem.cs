using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.DailyRewards.Controllers.Data
{
    [EcsSystem(510)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class DailyRewardSaveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                DailyRewardTag,
                SaveDataEvent>());

        private readonly IDataService _dataService;

        public DailyRewardSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                string id = IdsConst.DailyReward;
                DailyRewardDataComponent dailyRewardData = entity.GetDailyRewardData();

                DailyRewardSaveData data = new DailyRewardSaveData
                {
                    Id = id,
                    LastRewardTime = dailyRewardData.LastRewardTime,
                    CurrentTime = dailyRewardData.CurrentTime,
                    TargetRewardTime = dailyRewardData.TargetRewardTime,
                    ServerTime = dailyRewardData.ServerTime,
                };

                _dataService.SaveData(data, id);
            }
        }
    }
}