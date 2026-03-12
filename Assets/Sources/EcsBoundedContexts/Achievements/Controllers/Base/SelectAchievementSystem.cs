using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.EcsBoundedContexts.Achievements.Domain.Components;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.Achievements.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Achievements.Controllers.Base
{
    [EcsSystem(18)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class SelectAchievementSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly AchievementEntityFactory _factory;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                AchievementTag,
                SelectAchievementEvent>());
        [DI] private readonly ProtoIt _allIt = new(
            It.Inc<
                AchievementTag,
                SelectAchievementEvent>());

        private EntityLink _infoView;

        public SelectAchievementSystem(
            IUiViewService uiViewService,
            AchievementEntityFactory factory)
        {
            _uiViewService = uiViewService;
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            _infoView = _uiViewService.Get<AchievementsUiView>().AchievementInfo;
        }

        public void Run()
        {
            foreach (ProtoEntity selectedEntity in _it)
            {
                AchievementModuleComponent selectedModule = selectedEntity.GetAchievementModule();

                foreach (GameObject gameObject in selectedModule.Value.SelectedObjects)
                    gameObject.SetActive(true);
                
                _factory.InitAchievementInfoView(_infoView, selectedEntity);
                
                foreach (ProtoEntity entity in _allIt)
                {
                    if (entity.Equals(selectedEntity))
                        continue;
                    
                    AchievementModuleComponent module = entity.GetAchievementModule();
                    
                    foreach (GameObject gameObject in module.Value.SelectedObjects)
                        gameObject.SetActive(false);
                }
            }
        }
    }
}