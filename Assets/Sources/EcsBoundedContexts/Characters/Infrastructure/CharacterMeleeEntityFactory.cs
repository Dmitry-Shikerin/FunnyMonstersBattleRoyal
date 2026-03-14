using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Extansions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.EcsBoundedContexts.Characters.Infrastructure
{
    public class CharacterMeleeEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        private readonly DiContainer _container;
        private readonly IEntityPool _pool;

        public CharacterMeleeEntityFactory(
            IEntityPoolManager entityPoolManager,
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
            _container = container;
            _pool = entityPoolManager.GetPool<CharacterMeleeTag>();
            _pool.InitPool(Create);
        }

        public override ProtoEntity Create(EntityLink link) =>
            Create(Vector3.zero);

        public ProtoEntity Create(Vector3 position)
        {
            ProtoEntity entity = _pool.Get();
            entity.GetTransform().Value.position = position;
            
            return entity;
        }
        
        private ProtoEntity Create()
        {
            CharacterMeleeModule resourceObject = _assetCollector.Get<CharacterMeleeModule>();
            CharacterMeleeModule gameObject = Object.Instantiate(resourceObject);
            EntityLink link = gameObject.GetComponent<EntityLink>();
            
            CharacterMeleeConfig config = _assetCollector.Get<CharacterMeleeConfig>();
            CharacterMeleeModule module = link.GetModule<CharacterMeleeModule>();
            
            Aspect.CharacterMelee.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            //Components
            entity.AddTransform(link.transform);
            entity.AddGameObject(link.gameObject);
            entity.AddAnimancerEcs(module.Animancer);
            entity.AddAnimancerState(null);
            entity.AddActive();
            
            //Stats
            entity.AddCharacter();
            entity.AddCharacterType(CharacterType.Melee);
            entity.AddEnemiesFindRange(config.FindRange);
            entity.AddAttackPower(4);
            entity.AddCharacterMeleeConfig(config);

            //Rotation
            entity.AddRotationSpeed(config.RotationSpeed);
            entity.AddChangeRotationSpeedDelta(config.ChangeRotationSpeedDelta);
            
            //Health
            entity.AddHealth(config.Health);
            entity.AddMaxHealth(config.Health);
            entity.AddLookAt(module.HealthBarTransform);
            entity.AddHealthBar(module.HealthBarImage);
            
            module.BehaviourTreeOwner.InitGraphOwner(_container, entity);
            
            return entity;
        }
    }
}