using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.NukeAbilities.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Infrastructure
{
    public class NukeAbilityEntityFactory : EntityFactory
    {
        private readonly NukeBombEntityFactory _nukeBombEntityFactory;
        private readonly IEntityRepository _repository;

        public NukeAbilityEntityFactory(
            NukeBombEntityFactory nukeBombEntityFactory,
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
            _nukeBombEntityFactory = nukeBombEntityFactory;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            NukeAbilityModule module = link.GetModule<NukeAbilityModule>();

            Aspect.NukeAbility.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.NukeAbility);
            Authoring(link, entity);

            entity.AddAbility();
            entity.AddStringId(IdsConst.NukeAbility);
            ProtoEntity upgradeLink = _repository.GetByName(IdsConst.NukeUpgrade);
            entity.AddUpgradeLink(upgradeLink);
            entity.AddNukeDamageCollider(module.DamageCollider);
            entity.AddNukeParticle(module.NukeParticle);

            ProtoEntity nukeBomb = _nukeBombEntityFactory.Create(null);
            entity.AddNukeBombLink(nukeBomb);
            Vector3[] path = module.Path.Select(transform => transform.position).ToArray();
            nukeBomb.AddPointPath(path);
            nukeBomb.GetTransform().Value.position = module.Path[0].position;

            return entity;
        }
    }
}