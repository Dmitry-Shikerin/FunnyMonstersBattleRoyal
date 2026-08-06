using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure
{
    public class SettingsEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public SettingsEntityFactory(
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
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            Aspect.Settings.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.Settings);
            Authoring(link, entity);
            
            entity.AddStringId(IdsConst.Settings);
            entity.AddSoundVolume(0);
            entity.AddMusicVolume(0);
            entity.AddFramerate(0);
            entity.AddFullScreen();
            entity.AddGraphicsQuality("");
            entity.AddResolutionIndex(0);
            entity.AddVSync();
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
    }
}