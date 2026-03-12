using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.EnemySpawners.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Factories
{
    public class EnemySpawnPointEntityFactory : EntityFactory
    {
        public EnemySpawnPointEntityFactory(
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
        }

        public override ProtoEntity Create(EntityLink link)
        {
            EnemySpawnPointModule module = link.GetModule<EnemySpawnPointModule>();
            
            Aspect.EnemySpawnPoint.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            //Components
            entity.AddTransform(link.transform);
            entity.AddTargetCharactersPoints(module.MeleeSpawnPoint.Entity, module.RangeSpawnPoint.Entity);
            
            return entity;
        }
    }
}