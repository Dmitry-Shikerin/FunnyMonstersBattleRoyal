using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.ApplyAbility.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.ApplyAbility.Infrastructure
{
    public class AbilityApplierEntityFactory : EntityFactory
    {
        public AbilityApplierEntityFactory(
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
            return default;
        }

        public ProtoEntity Create(EntityLink link, ProtoEntity entity)
        {
            AbilityApplierModule module = link.GetModule<AbilityApplierModule>();
            
            InitLink(link, entity);
            entity.AddAbilityApplierModule(module);
            
            return entity;
        }
    }
}