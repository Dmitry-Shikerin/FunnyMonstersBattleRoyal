using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Infrastructure
{
    public class InputEntityFactory : EntityFactory
    {
        public InputEntityFactory(
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
            Aspect.Input.NewEntity(out ProtoEntity entity);

            entity.AddDirection(Vector3.zero);
            
            return default;
        }
    }
}