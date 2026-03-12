using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.HealthBoosters.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.HealthBoosters.Infrastructure
{
    public class HealthBusterEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public HealthBusterEntityFactory(
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
            HealthBusterModule module = link.GetModule<HealthBusterModule>();
            
            Aspect.HealthBuster.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.HealthBooster);
            Authoring(link, entity);
            
            entity.AddStringId(IdsConst.HealthBooster);

            entity.ReplaceHealthBuster(5);
            entity.AddHealthBusterModule(module);
            
            //Save
            entity.AddSavableData();
            
            return entity;
        }
    }
}