using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DailyRewards.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.DailyRewards.Infrastructure
{
    public class DailyRewardEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public DailyRewardEntityFactory(
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
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            DailyRewardModule module = link.GetModule<DailyRewardModule>();
            
            Aspect.DailyReward.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            _repository.AddByName(entity, IdsConst.DailyReward);

            entity.AddStringId(IdsConst.DailyReward);
            DateTime lustRewardTime = DateTime.Now - TimeSpan.FromDays(1);
            entity.AddDailyRewardData(lustRewardTime, TimeSpan.Zero, DateTime.Now, DateTime.Now);
            entity.AddDailyRewardModule(module);
            
            return entity;
        }
    }
}