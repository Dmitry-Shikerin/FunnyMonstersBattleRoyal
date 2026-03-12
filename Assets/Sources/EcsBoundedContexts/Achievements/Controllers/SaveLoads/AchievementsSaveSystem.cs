using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Achievements.Domain.Components;
using Sources.EcsBoundedContexts.Achievements.Domain.Data;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads
{
    [EcsSystem(513)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class AchievementsSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                AchievementTag,
                SaveDataEvent>());

        public AchievementsSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                string key = entity.GetStringId().Value;
                AchievementSaveData achievementSaveData = new AchievementSaveData()
                {
                    Id = key,
                    IsCompleted = entity.HasComplete(),
                };
                _dataService.SaveData(achievementSaveData, key);
            }
        }
    }
}