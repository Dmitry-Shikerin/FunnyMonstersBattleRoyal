using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure
{
    public class SettingsEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;

        public SettingsEntityFactory(
            IAssetCollector assetCollector,
            IEntityRepository repository,
            ProtoWorld world,
            GameAspect aspect,
            Container container)
            : base(
                repository,
                world,
                aspect,
                container)
        {
            _assetCollector = assetCollector;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            SettingsConfig config = _assetCollector.Get<SettingsConfig>();
            Aspect.Settings.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.Settings);
            Authoring(link, entity);

            SetterSettingsModule module = link.GetModule<SetterSettingsModule>();
            entity.AddSetterSettingsModule(module);
            entity.AddStringId(IdsConst.Settings);
            entity.AddSoundVolume(config.SoundVolume);
            entity.AddMusicVolume(config.MusicVolume);
            entity.AddFramerate(config.Framerate);
            entity.AddFullScreenMode(config.FullScreenMode);
            entity.AddGraphicsQuality("");
            Resolution resolution = Screen.currentResolution;
            entity.AddResolution(resolution.width, resolution.height, resolution.refreshRate);
            entity.AddSavedSettings(new SettingsSaveData());
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
    }
}