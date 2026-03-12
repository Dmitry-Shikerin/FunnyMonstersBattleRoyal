using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.EcsBoundedContexts.EnemySpawners.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Factories
{
    public class EnemySpawnerEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly EnemySpawnPointEntityFactory _enemySpawnPointEntityFactory;
        private readonly IEntityRepository _repository;

        public EnemySpawnerEntityFactory(
            IAssetCollector assetCollector,
            EnemySpawnPointEntityFactory enemySpawnPointEntityFactory,
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
            _enemySpawnPointEntityFactory = enemySpawnPointEntityFactory;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            EnemySpawnerModule module = link.GetModule<EnemySpawnerModule>();
            EnemySpawnerConfig config = _assetCollector.Get<EnemySpawnerConfig>();
            
            Aspect.EnemySpawner.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.EnemySpawner);
            Authoring(link, entity);
            
            //Components
            entity.AddStringId(IdsConst.EnemySpawner);
            entity.AddTransform(link.transform);
            ProtoEntity[,] spawnPoints = CreateSpawnPoints(module.SpawnPoints);
            entity.AddEnemySpawnPoints(spawnPoints);
            entity.AddEnemySpawnerData(
                0,
                0,
                0,
                0,
                EnemyType.Default);
            entity.AddEnemySpawnerConfig(config);
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }
        
        public ProtoEntity InitUiLink(EntityLink link, ProtoEntity entity)
        {
            EnemySpawnerUiModule module = link.GetModule<EnemySpawnerUiModule>();
            
            InitLink(link, entity);

            entity.AddEnemySpawnerUiModule(module);
            
            return entity;
        }
        
        private ProtoEntity[,] CreateSpawnPoints(List<EntityLink> spawnPointsLinks)
        {
             List<ProtoEntity> entities = new List<ProtoEntity>();
            
             foreach (EntityLink link in spawnPointsLinks)
             {
                 ProtoEntity point = _enemySpawnPointEntityFactory.Create(link);
                 entities.Add(point);
             }
             
             ProtoEntity[,] points = new ProtoEntity[2, 4];
             
             for (int i = 0; i < points.GetLength(0); i++)
             {
                 for (int j = 0; j < points.GetLength(1); j++)
                 {
                     points[i, j] = entities[i * 4 + j];
                 }
             }
            
             return points;
        }
    }
}