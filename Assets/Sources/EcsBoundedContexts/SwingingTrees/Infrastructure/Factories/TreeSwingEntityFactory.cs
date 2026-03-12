using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Configs;
using Sources.EcsBoundedContexts.SwingingTrees.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Random = UnityEngine.Random;

namespace Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories
{
    public class TreeSwingEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        public TreeSwingEntityFactory(
            IEntityRepository repository,
            DiContainer container,
            IAssetCollector assetCollector, 
            ProtoWorld world,
            GameAspect aspect) 
            : base(repository, world, aspect, container)
        {
            _assetCollector = assetCollector ?? throw new ArgumentNullException(nameof(assetCollector));
        }
        
        public override ProtoEntity Create(EntityLink link)
        {
            TreeModule module = link.GetModule<TreeModule>();
            TreeSwingerCollector collector = _assetCollector.Get<TreeSwingerCollector>();
            TreeSwingerConfig config = collector.GetById(module.TreeType.ToString());
            ref SwingingTreeComponent treeSwinger = ref Aspect.SwingingTree.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            Initialize(ref treeSwinger, config);
            entity.AddTransform(link.transform);
            
            return entity;
        }
        
        private void Initialize(ref SwingingTreeComponent treeSwinger, TreeSwingerConfig config)
        {
            treeSwinger.MaxAngleX = config.SwingMaxAngleX + Random.Range(-config.SwingMaxAngleRandomnessX, config.SwingMaxAngleRandomnessX);
            treeSwinger.MaxAngleY = config.SwingMaxAngleY + Random.Range(-config.SwingMaxAngleRandomnessY, config.SwingMaxAngleRandomnessY);
            treeSwinger.SpeedX = config.SwingSpeedX + Random.Range(-config.SwingSpeedRandomnessX, config.SwingSpeedRandomnessX);
            treeSwinger.SpeedY = config.SwingSpeedY + Random.Range(-config.SwingSpeedRandomnessY, config.SwingSpeedRandomnessY);
            treeSwinger.Direction = config.Direction + Random.Range(-config.DirectionRandomness, config.DirectionRandomness);
            treeSwinger.EnableYAxisSwingingTree = config.EnableYAxisSwinging;
        }
    }
}