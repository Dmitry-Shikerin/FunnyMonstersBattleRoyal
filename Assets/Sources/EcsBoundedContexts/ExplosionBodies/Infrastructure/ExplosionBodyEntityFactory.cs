using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.ExplosionBodies.Domain;
using Sources.EcsBoundedContexts.ExplosionBodies.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure
{
    public class ExplosionBodyEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityPool _pool;

        public ExplosionBodyEntityFactory(
            IAssetCollector assetCollector,
            IEntityPoolManager poolManager,
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
            _assetCollector = assetCollector;
            _pool = poolManager.GetPool<ExplosionBodyTag>();
            _pool.InitPool(Create);
        }

        public override ProtoEntity Create(EntityLink link) =>
            Create(Vector3.zero);

        public ProtoEntity Create(Vector3 position)
        {
            ProtoEntity entity = _pool.Get();
            entity.GetTransform().Value.position = position;
            
            return entity;
        }
        
        private ProtoEntity Create()
        {
            ExplosionBodyModule resourceObject = _assetCollector.Get<ExplosionBodyModule>();
            ExplosionBodyModule module = Object.Instantiate(resourceObject);
            EntityLink link = module.GetComponent<EntityLink>();
            
            Aspect.ExplosionBody.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            
            entity.AddTransform(link.transform);
            entity.AddGameObject(link.gameObject);
            entity.AddActive();
            
            return entity;
        }
    }
}