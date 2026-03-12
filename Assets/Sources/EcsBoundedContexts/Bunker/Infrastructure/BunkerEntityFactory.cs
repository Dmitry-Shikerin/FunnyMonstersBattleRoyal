using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Bunker.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Bunker.Infrastructure
{
    public class BunkerEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public BunkerEntityFactory(
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
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            BunkerModule module = link.GetModule<BunkerModule>();
            
            Aspect.Bunker.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.Bunker);
            Authoring(link, entity);
            entity.AddStringId(IdsConst.Bunker);
            
            entity.AddTransform(link.transform);
            entity.AddHealth(20);
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }

        public ProtoEntity InitUiLink(EntityLink link, ProtoEntity entity)
        {
            BunkerUiModule module = link.GetModule<BunkerUiModule>();
            InitLink(link, entity, false);

            entity.AddBunkerUiModule(module);
            
            return entity;
        }
    }
}