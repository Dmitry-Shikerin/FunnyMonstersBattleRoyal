using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Common;
using Sources.EcsBoundedContexts.Achievements.Domain.Data;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads
{
    [EcsSystem(17)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class AchievementsLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly AchievementEntityFactory _achievementEntityFactory;
        private readonly IDataService _dataService;
        private readonly IEntityRepository _entityRepository;

        public AchievementsLoadSystem(
            IUiViewService uiViewService,
            AchievementEntityFactory achievementEntityFactory,
            IDataService dataService,
            IEntityRepository entityRepository)
        {
            _uiViewService = uiViewService;
            _achievementEntityFactory = achievementEntityFactory;
            _dataService = dataService;
            _entityRepository = entityRepository;
        }

        public void Init(IProtoSystems systems)
        {
            AchievementsUiView achievementsUiView = _uiViewService.Get<AchievementsUiView>();
            IReadOnlyList<string> achievementIds = IdsConst.GetIds<AchievementSaveData>();

            //Create
            for (int i = 0; i < achievementIds.Count; i++)
            {
                string achievementId = achievementIds[i];
                EntityLink achievementLink = achievementsUiView.Achievements[i];

                ProtoEntity entity = _achievementEntityFactory.Create(achievementLink, achievementId);
                entity.AddSaveDataEvent();
            }

            if (_dataService.HasKey(IdsConst.FirstEnemyKillAchievement) == false)
                return;

            //Load
            foreach (string achievementId in achievementIds)
            {
                AchievementSaveData achievementSaveData = _dataService.LoadData<AchievementSaveData>(achievementId);
                ProtoEntity achievementEntity = _entityRepository.GetByName(achievementId);
                
                if (achievementSaveData.IsCompleted == false)
                    continue;
            
                achievementEntity.AddComplete();
                achievementEntity.GetAchievementModule().Value.UncompletedImage.gameObject.SetActive(false);
            }
        }
    }
}