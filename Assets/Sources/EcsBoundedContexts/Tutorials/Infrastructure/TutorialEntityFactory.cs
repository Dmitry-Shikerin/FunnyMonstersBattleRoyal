using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Tutorials.Infrastructure
{
    public class TutorialEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public TutorialEntityFactory(
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
            Aspect.Tutorial.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.Tutorial);
            
            entity.AddStringId(IdsConst.Tutorial);
            
            //Save
            entity.AddSavableData();
            
            return entity;
        }
    }
}