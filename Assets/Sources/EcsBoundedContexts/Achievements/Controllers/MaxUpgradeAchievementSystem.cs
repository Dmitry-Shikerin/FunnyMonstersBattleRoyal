using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Achievements.Controllers
{
    [EcsSystem(456)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.Game)]
    public class MaxUpgradeAchievementSystem : AchievementSystem, IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                UpgradeTag,
                ApplyUpgradeEvent>());
        [DI] private readonly ProtoIt _upgradeIt = new(
            It.Inc<
                UpgradeTag>());

        private readonly IUiPopUpService _uiPopUpService;
        private readonly IStorageService _storageService;
        private readonly IEntityRepository _entityRepository;

        public MaxUpgradeAchievementSystem(
            IUiPopUpService uiPopUpService,
            IStorageService storageService,
            IEntityRepository entityRepository,
            AchievementEntityFactory achievementEntityFactory) 
            : base(
                uiPopUpService,
                achievementEntityFactory,
                storageService)
        {
            _uiPopUpService = uiPopUpService;
            _storageService = storageService;
            _entityRepository = entityRepository;
        }

        protected override string AchievementId { get; set; }
        protected override ProtoEntity Achievement { get; set; }
        protected override EntityLink AchievementPopUpLink { get; set; }

        public void Init(IProtoSystems systems)
        {
            AchievementId = IdsConst.MaxUpgradeAchievement;
            Achievement = _entityRepository.GetByName(AchievementId);
            AchievementPopUpLink = _uiPopUpService.Get<AchievementUiPopUpView>().AchievementLink;
        }

        public void Run()
        {
            if (Achievement.HasComplete())
                return;
            
            int index = 0;
            int len = _upgradeIt.Len();
            
            foreach (ProtoEntity entity in _it)
            {
                foreach (ProtoEntity ability in _upgradeIt)
                {
                    index++;

                    UpgradeConfigComponent levelUpgradeConfigComponent = ability.GetUpgradeConfig();
                    int maxLevel = levelUpgradeConfigComponent.Value.Levels.Count;
                    int currentLevel = levelUpgradeConfigComponent.Index;

                    if (currentLevel < maxLevel)
                        return;

                    if (index != len)
                        continue;
                
                    Execute();
                    _storageService.Save(IdsConst.GetIds<UpgradeSaveData>());
                }
            }
        }
    }
}