using System;
using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Achievements.Domain.Configs;
using Sources.EcsBoundedContexts.Achievements.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.DeepWrappers.Localizations;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Achievements.Infrastructure
{
    public class AchievementEntityFactory : EntityFactory
    {
        private readonly ILocalizationService _localizationService;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;

        public AchievementEntityFactory(
            ILocalizationService localizationService,
            IAssetCollector assetCollector,
            IEntityRepository repository,
            ProtoWorld world,
            GameAspect aspect,
            DiContainer container) 
            : base(
                repository,
                world,
                aspect,
                container)
        {
            _localizationService = localizationService;
            _assetCollector = assetCollector;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            throw new InvalidOperationException();
        }
        
        public ProtoEntity Create(EntityLink link, string id)
        {
            AchievementModule module = link.GetModule<AchievementModule>();
            AchievementConfig config = _assetCollector
                .Get<AchievementConfigCollector>()
                .GetById(id);

            module.IconImage.sprite = config.Sprite;
            module.TitleText.text = _localizationService.GetText(config.TitleId);
            module.DescriptionText.text = _localizationService.GetText(config.DescriptionId);
            
            Aspect.Achievement.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            _repository.AddByName(entity, id);
            
            entity.AddStringId(id);
            entity.AddAchievementModule(module);

            //Save
            entity.AddSavableData();
            
            return entity;
        }

        public ProtoEntity Create(ProtoEntity entity)
        {
            EntityLink link = entity.GetEntityLink().EntityLink;
            string stringId = entity.GetStringId().Value;
            AchievementModule module = link.GetModule<AchievementModule>();
            AchievementConfig config = _assetCollector
                .Get<AchievementConfigCollector>()
                .GetById(stringId);

            module.IconImage.sprite = config.Sprite;
            module.TitleText.text = _localizationService.GetText(config.TitleId);
            module.UncompletedImage.gameObject.SetActive(false);
            module.DescriptionText.text = _localizationService.GetText(config.DescriptionId);
            
            return entity;
        }
        
        public ProtoEntity InitPopUpView(EntityLink link, ProtoEntity entity)
        {
            string stringId = entity.GetStringId().Value;
            AchievementModule module = link.GetModule<AchievementModule>();
            AchievementConfig config = _assetCollector
                .Get<AchievementConfigCollector>()
                .GetById(stringId);
            
            InitLink(link, entity, false);

            module.IconImage.sprite = config.Sprite;
            module.TitleText.text = _localizationService.GetText(config.TitleId);
            module.UncompletedImage?.gameObject.SetActive(false);
            module.DescriptionText.text = _localizationService.GetText(config.DescriptionId);
            module.Animation.Play();
            
            return entity;
        }
        
        public ProtoEntity InitAchievementInfoView(EntityLink link, ProtoEntity entity)
        {
            string stringId = entity.GetStringId().Value;
            AchievementInfoModule module = link.GetModule<AchievementInfoModule>();
            AchievementConfig config = _assetCollector
                .Get<AchievementConfigCollector>()
                .GetById(stringId);
            
            InitLink(link, entity, false);

            module.IconImage.sprite = config.Sprite;
            module.TitleText.text = _localizationService.GetText(config.TitleId);
            module.DescriptionText.text = _localizationService.GetText(config.DescriptionId);
            
            return entity;
        }
    }
}