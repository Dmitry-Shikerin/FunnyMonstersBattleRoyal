using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Data;
using Sources.EcsBoundedContexts.DailyRewards.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.EcsBoundedContexts.DailyRewards.Controllers.Data
{
    [EcsSystem(15)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class DailyRewardLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISceneService _sceneService;
        private readonly DailyRewardEntityFactory _dailyRewardEntityFactory;
        private readonly IDataService _dataService;

        public DailyRewardLoadSystem(
            IUiViewService uiViewService,
            ISceneService sceneService,
            DailyRewardEntityFactory dailyRewardEntityFactory,
            IDataService dataService)
        {
            _uiViewService = uiViewService;
            _sceneService = sceneService;
            _dailyRewardEntityFactory = dailyRewardEntityFactory;
            _dataService = dataService;
        }

        public void Init(IProtoSystems systems)
        {
            EntityLink link = _sceneService.CurrentSceneName == IdsConst.MainMenu
                ? _uiViewService.Get<MainHudUiView>().DailyReward
                : _uiViewService.Get<GameplayPauseUiView>().DailyReward;
            ProtoEntity entity = _dailyRewardEntityFactory.Create(link);

            if (_dataService.HasKey(IdsConst.DailyReward) == false)
                return;

            DailyRewardSaveData data = _dataService.LoadData<DailyRewardSaveData>(IdsConst.DailyReward);
            entity.ReplaceDailyRewardData(data.LastRewardTime, data.CurrentTime, data.TargetRewardTime, data.ServerTime);
        }
    }
}