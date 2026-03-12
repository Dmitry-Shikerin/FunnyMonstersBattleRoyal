using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.EcsBoundedContexts.Achievements.Controllers.Base
{
    public abstract class AchievementSystem
    {
        private readonly IUiPopUpService _uiPopUpService;
        private readonly AchievementEntityFactory _achievementEntityFactory;
        private readonly IStorageService _storageService;
        
        protected abstract string AchievementId { get; set; }
        protected abstract ProtoEntity Achievement { get; set; }
        protected abstract EntityLink AchievementPopUpLink { get; set; }

        protected AchievementSystem(
            IUiPopUpService uiPopUpService,
            AchievementEntityFactory achievementEntityFactory,
            IStorageService storageService)
        {
            _uiPopUpService = uiPopUpService;
            _achievementEntityFactory = achievementEntityFactory;
            _storageService = storageService;
        }

        protected void Execute()
        {
            Achievement.AddComplete();
            _achievementEntityFactory.Create(Achievement);
            _uiPopUpService.Show(UiPopUpId.Achievement);
            _achievementEntityFactory.InitPopUpView(AchievementPopUpLink, Achievement);
            _storageService.Save(AchievementId);
        }
    }
}