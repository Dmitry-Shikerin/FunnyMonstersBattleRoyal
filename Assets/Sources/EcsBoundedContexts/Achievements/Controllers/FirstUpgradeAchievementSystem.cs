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
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Achievements.Controllers
{
    [EcsSystem(454)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.Game)]
    public class FirstUpgradeAchievementSystem : AchievementSystem, IProtoRunSystem, IProtoInitSystem
    {
        private readonly IUiPopUpService _uiPopUpService;
        private readonly IEntityRepository _entityRepository;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                UpgradeTag,
                ApplyUpgradeEvent>());

        public FirstUpgradeAchievementSystem(
            IUiPopUpService uiPopUpService,
            IStorageService storageService,
            AchievementEntityFactory achievementEntityFactory,
            IEntityRepository entityRepository) 
            : base(
                uiPopUpService,
                achievementEntityFactory,
                storageService)
        {
            _uiPopUpService = uiPopUpService;
            _entityRepository = entityRepository;
        }

        protected override ProtoEntity Achievement { get; set; }
        protected override EntityLink AchievementPopUpLink { get; set; }
        protected override string AchievementId { get; set; }

        public void Init(IProtoSystems systems)
        {
            AchievementId = IdsConst.FirstUpgradeAchievement;
            Achievement = _entityRepository.GetByName(AchievementId);
            AchievementPopUpLink = _uiPopUpService.Get<AchievementUiPopUpView>().AchievementLink;
        }

        public void Run()
        {
            if (Achievement.HasComplete())
                return;
            
            foreach (ProtoEntity entity in _it)
            {
                Execute();
            }
        }
    }
}