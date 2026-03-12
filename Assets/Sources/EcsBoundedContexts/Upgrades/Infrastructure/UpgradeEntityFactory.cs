using System;
using System.Linq;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Upgrades.Domain.Configs;
using Sources.EcsBoundedContexts.Upgrades.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
namespace Sources.EcsBoundedContexts.Upgrades.Infrastructure
{
    public class UpgradeEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;

        public UpgradeEntityFactory(
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
            throw new NotImplementedException();
        }

        public ProtoEntity Create(EntityLink link, string id)
        {
            ApplyUpgradeModule module = link.GetModule<ApplyUpgradeModule>();
            //TODO доработать
            UpgradeConfig config = _assetCollector.Get<UpgradeConfigContainer>().UpgradeConfigs.First(x => x.Id == id);
            
            Aspect.Upgrade.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, id);
            Authoring(link, entity);

            entity.AddApplyUpgradeUiModule(module);
            entity.AddUpgradeConfig(config, 0);
            entity.AddStringId(id);
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();

            return entity;
        }
    }
}