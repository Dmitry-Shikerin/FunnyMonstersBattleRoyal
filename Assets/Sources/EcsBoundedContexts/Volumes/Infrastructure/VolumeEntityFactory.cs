using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Volumes.Infrastructure
{
    public class VolumeEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public VolumeEntityFactory(
            IEntityRepository repository,
            ProtoWorld world,
            GameAspect aspect,
            Container container) 
            : base(repository,
                world,
                aspect,
                container)
        {
            _repository = repository;
        }
        
        public override ProtoEntity Create(EntityLink link)
        {
            throw new NotImplementedException();
        }

        public ProtoEntity Create(EntityLink link, string id)
        {
            MusicVolumeModule module = link.GetModule<MusicVolumeModule>();
            
            Aspect.GameVolume.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            _repository.AddByName(entity, id);
            //entity.AddVolumeType(module.VolumeType);
            entity.AddStringId(id);
            entity.AddVolumeModule(module);
            
            //Save
            entity.AddSavableData();

            return entity;
        }
    }
}