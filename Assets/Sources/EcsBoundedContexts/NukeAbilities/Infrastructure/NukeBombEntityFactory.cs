using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.NukeAbilities.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Infrastructure
{
    public class NukeBombEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;

        public NukeBombEntityFactory(
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
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            NukeBombModule asset = _assetCollector.Get<NukeBombModule>();
            NukeBombModule nukeBombModule = Object.Instantiate(asset);
            EntityLink newLink = nukeBombModule.GetComponent<EntityLink>();
            
            Aspect.NukeBomb.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.NukeBomb);
            Authoring(newLink, entity);

            entity.AddGameObject(newLink.gameObject);
            entity.AddTransform(newLink.transform);
            entity.AddActive();
            entity.AddMoveSpeed(3);
            entity.AddTargetPointIndex(0);
            
            return entity;
        }
    }
}