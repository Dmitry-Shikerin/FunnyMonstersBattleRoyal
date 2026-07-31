using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Players.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Players.Infrastructure
{
    public class PlayerEntityFactory  : EntityFactory
    {
        private readonly IEntityRepository _repository;

        public PlayerEntityFactory(
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
            PlayerNameUiModule module = link.GetModule<PlayerNameUiModule>();
            
            Aspect.Player.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.Player);
            Authoring(link, entity);
            
            entity.AddStringId(IdsConst.Player);
            entity.AddPlayerName("DefaultPlayerName");
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
    }
}