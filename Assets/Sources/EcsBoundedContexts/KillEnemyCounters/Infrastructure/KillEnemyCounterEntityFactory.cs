using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.KillEnemyCounters.Infrastructure
{
    public class KillEnemyCounterEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public KillEnemyCounterEntityFactory(
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
            Aspect.KillEnemyCounter.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.KillEnemyCounter);
            
            entity.AddStringId(IdsConst.KillEnemyCounter);
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
    }
}