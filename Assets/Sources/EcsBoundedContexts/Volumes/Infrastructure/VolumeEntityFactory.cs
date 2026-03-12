using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Presentation;
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
            DiContainer container) 
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
            VolumeModule module = link.GetModule<VolumeModule>();
            
            Aspect.GameVolume.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            _repository.AddByName(entity, id);
            entity.AddVolumeType(module.VolumeType);
            entity.AddStringId(id);
            entity.AddVolumeModule(module);
            
            //Save
            entity.AddSavableData();

            return entity;
        }
    }
}