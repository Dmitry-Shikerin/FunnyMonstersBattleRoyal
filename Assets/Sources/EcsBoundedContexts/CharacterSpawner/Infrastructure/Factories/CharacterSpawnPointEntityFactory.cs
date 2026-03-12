using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Infrastructure.Factories
{
    public class CharacterSpawnPointEntityFactory : EntityFactory
    {
        public CharacterSpawnPointEntityFactory(
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
            CharacterSpawnPointModule module = link.GetModule<CharacterSpawnPointModule>();
            
            Aspect.CharacterSpawnPoint.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            //Components
            entity.AddCharacterSpawnPointType(module.SpawnPointType);
            entity.AddTransform(link.transform);
            
            return entity;
        }
    }
}