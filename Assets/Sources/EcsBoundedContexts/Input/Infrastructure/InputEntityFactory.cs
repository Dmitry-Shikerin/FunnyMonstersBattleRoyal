using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Infrastructure
{
    public class InputEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public InputEntityFactory(
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
            Aspect.Input.NewEntity(out ProtoEntity entity);

            //_repository.AddByName(entity, IdsConst.Input);
            entity.AddDirection(Vector3.zero);
            entity.AddNetworkInputDirection(Vector3.zero);
            entity.AddNetworkCameraForward(Vector3.forward);
            entity.AddCameraZoom(3);
            entity.AddCameraLook(Vector2.zero);
            
            return entity;
        }
    }
}