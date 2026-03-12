using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Infrastructure
{
    public class FlamethrowerEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public FlamethrowerEntityFactory(
            IAssetCollector assetCollector,
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
        }

        public override ProtoEntity Create(EntityLink link)
        {
            FlamethrowerModule asset = _assetCollector.Get<FlamethrowerModule>();
            FlamethrowerModule flamethrower = Object.Instantiate(asset);
            link = flamethrower.GetComponent<EntityLink>();
            FlamethrowerModule module = link.GetModule<FlamethrowerModule>();
            
            Aspect.Flamethrower.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            
            entity.AddTransform(link.transform);
            entity.AddFlameParticle(module.FlameParticle);
            entity.AddMoveSpeed(3);
            entity.AddTargetPointIndex(0);
            
            return entity;
        }
    }
}