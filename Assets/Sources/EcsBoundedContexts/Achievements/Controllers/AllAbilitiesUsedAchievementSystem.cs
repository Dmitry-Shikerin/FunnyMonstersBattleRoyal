using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.ApplyAbility.Domain.Data;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Achievements.Controllers
{
    [EcsSystem(450)]
    [ComponentGroup(ComponentGroup.Achievements)]
    [Aspect(AspectName.Game)]
    public class AllAbilitiesUsedAchievementSystem : AchievementSystem, IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                AbilityTag,
                ApplyAbilityEvent>());
        [DI] private readonly ProtoIt _abilityIt = new(
            It.Inc<
                AbilityTag>());

        private readonly IStorageService _storageService;
        private readonly IUiPopUpService _uiPopUpService;
        private readonly IEntityRepository _entityRepository;

        public AllAbilitiesUsedAchievementSystem(
            IUiPopUpService uiPopUpService,
            IEntityRepository entityRepository,
            AchievementEntityFactory achievementEntityFactory,
            IStorageService storageService)
            : base(
                uiPopUpService,
                achievementEntityFactory,
                storageService)
        {
            _uiPopUpService = uiPopUpService;
            _entityRepository = entityRepository;
            _storageService = storageService;
        }

        protected override ProtoEntity Achievement { get; set; }
        protected override EntityLink AchievementPopUpLink { get; set; }
        protected override string AchievementId { get; set; }

        public void Init(IProtoSystems systems)
        {
            AchievementId = IdsConst.AllAbilitiesUsedAchievement;
            Achievement = _entityRepository.GetByName(AchievementId);
            AchievementPopUpLink = _uiPopUpService.Get<AchievementUiPopUpView>().AchievementLink;
        }

        public void Run()
        {
            if (Achievement.HasComplete())
                return;

            int index = 0;
            int len = _abilityIt.Len();

            foreach (ProtoEntity entity in _it)
            {
                foreach (ProtoEntity ability in _abilityIt)
                {
                    index++;

                    if (ability.HasFirstUsedCompleted() == false)
                        return;

                    if (index != len)
                        continue;

                    Execute();
                    _storageService.Save(IdsConst.GetIds<AbilitySaveData>());
                }
            }
        }
    }
}