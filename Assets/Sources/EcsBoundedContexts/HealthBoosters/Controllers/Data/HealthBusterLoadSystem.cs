using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.HealthBoosters.Domain.Data;
using Sources.EcsBoundedContexts.HealthBoosters.Infrastructure;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.EcsBoundedContexts.HealthBoosters.Controllers.Data
{
    [EcsSystem(13)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class HealthBusterLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISceneService _sceneService;
        private readonly IDataService _dataService;
        private readonly HealthBusterEntityFactory _healthBusterEntityFactory;

        public HealthBusterLoadSystem(
            IUiViewService uiViewService,
            ISceneService sceneService,
            IDataService dataService,
            HealthBusterEntityFactory healthBusterEntityFactory)
        {
            _uiViewService = uiViewService;
            _sceneService = sceneService;
            _dataService = dataService;
            _healthBusterEntityFactory = healthBusterEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            EntityLink link = _sceneService.CurrentSceneName == IdsConst.Gameplay
                ? _uiViewService.Get<GameplayUiView>().HealthBuster
                : _uiViewService.Get<MainHudUiView>().HealthBuster;
            
            //Create
            ProtoEntity entity = _healthBusterEntityFactory.Create(link);
            entity.AddInitializeEvent();

            if (_dataService.HasKey(IdsConst.HealthBooster) == false)
                return;
            
            //Load
            HealthBusterSaveData healthBusterSaveData = _dataService.LoadData<HealthBusterSaveData>(IdsConst.HealthBooster);
            entity.ReplaceHealthBuster(healthBusterSaveData.Amount);

            if (healthBusterSaveData.IsFirstUsedCompleted)
                entity.AddFirstUsedCompleted();
        }
    }
}