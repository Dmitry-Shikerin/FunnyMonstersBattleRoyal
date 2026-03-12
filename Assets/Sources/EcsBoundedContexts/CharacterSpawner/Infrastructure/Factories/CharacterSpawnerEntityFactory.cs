using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Infrastructure.Factories
{
    public class CharacterSpawnerEntityFactory : EntityFactory
    {
        private readonly CharacterSpawnPointEntityFactory _characterSpawnPointEntityFactory;
        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _repository;

        public CharacterSpawnerEntityFactory(
            CharacterSpawnPointEntityFactory characterSpawnPointEntityFactory,
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
            _characterSpawnPointEntityFactory = characterSpawnPointEntityFactory;
            _assetCollector = assetCollector;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            CharacterSpawnerModule module = link.GetModule<CharacterSpawnerModule>();
            
            Aspect.CharacterSpawner.NewEntity(out ProtoEntity entity);
            _repository.AddByName(entity, IdsConst.CharacterSpawnerAbility);
            Authoring(link, entity);

            entity.AddStringId(IdsConst.CharacterSpawnerAbility);
            entity.AddAbility();
            entity.AddAbilityCooldownDuration(25);
            
            //Components
            entity.AddTransform(link.transform);
            List<ProtoEntity> meleeSpawnPoints = CreateSpawnPoints(module.MeleeSpawnPoints);
            List<ProtoEntity> rangeSpawnPoints = CreateSpawnPoints(module.RangeSpawnPoints);
            entity.AddCharactersSpawnPoints(meleeSpawnPoints, rangeSpawnPoints);
            
            return entity;
        }

        private List<ProtoEntity> CreateSpawnPoints(List<EntityLink> spawnPointsLinks)
        {
            List<ProtoEntity> points = new List<ProtoEntity>();
            
            foreach (EntityLink link in spawnPointsLinks)
            {
                ProtoEntity point = _characterSpawnPointEntityFactory.Create(link);
                points.Add(point);
            }
            
            return points;
        }
    }
}