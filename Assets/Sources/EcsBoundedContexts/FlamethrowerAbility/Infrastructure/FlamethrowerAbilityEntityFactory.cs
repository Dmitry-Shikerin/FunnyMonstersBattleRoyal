using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Infrastructure
{
    public class FlamethrowerAbilityEntityFactory : EntityFactory
    {
        private readonly FlamethrowerEntityFactory _flamethrowerEntityFactory;
        private readonly IEntityRepository _repository;

        public FlamethrowerAbilityEntityFactory(
            FlamethrowerEntityFactory flamethrowerEntityFactory,
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
            _flamethrowerEntityFactory = flamethrowerEntityFactory;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            FlamethrowerAbilityModule module = link.GetModule<FlamethrowerAbilityModule>();
            
            Aspect.FlamethrowerAbility.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.FlamethrowerAbility);
            Authoring(link, entity);

            entity.AddStringId(IdsConst.FlamethrowerAbility);
            ProtoEntity upgradeLink = _repository.GetByName(IdsConst.FlamethrowerUpgrade);
            entity.AddUpgradeLink(upgradeLink);
            entity.AddAbility();
            entity.AddTransform(link.transform);
            ProtoEntity flamethrower = _flamethrowerEntityFactory.Create(null);
            entity.AddFlamethrowerLink(flamethrower);
            Vector3[] path = module.Path.Select(transform => transform.position).ToArray();
            flamethrower.AddPointPath(path);
            flamethrower.GetTransform().Value.position = module.Path[0].position;
            
            return entity;
        }
    }
}