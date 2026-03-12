using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.PlayerWallets.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.PlayerWallets.Infrastructure
{
    public class PlayerWalletEntityFactory : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public PlayerWalletEntityFactory(
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
            PlayerWalletModule module = link.GetModule<PlayerWalletModule>();
            
            Aspect.PlayerWallet.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.PlayerWallet);
            Authoring(link, entity);
            
            entity.AddPlayerWalletModule(module);
            entity.AddStringId(IdsConst.PlayerWallet);
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
    }
}