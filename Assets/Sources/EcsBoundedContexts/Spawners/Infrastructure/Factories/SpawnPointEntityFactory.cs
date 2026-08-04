using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Spawners.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Spawners.Infrastructure.Factories
{
    public class SpawnPointEntityFactory : EntityFactory
    {
        public SpawnPointEntityFactory(
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
        }

        public override ProtoEntity Create(EntityLink link)
        {
            SpawnPointModule module = link.GetModule<SpawnPointModule>();
            
            Aspect.SpawnPoint.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            entity.AddTransform(link.transform);
            entity.AddSpawnPointTransform(module.SpawnPointTransform);
            
            return entity;
        }
    }
}