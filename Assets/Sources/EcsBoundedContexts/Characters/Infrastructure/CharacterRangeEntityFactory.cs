using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Extancions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.EcsBoundedContexts.Characters.Infrastructure
{
    public class CharacterRangeEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        private readonly DiContainer _container;
        private readonly IEntityPool _pool;

        public CharacterRangeEntityFactory(
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
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _pool = entityPoolManager.GetPool<CharacterRangeTag>();
            _pool.InitPool(Create);
        }
        
        public override ProtoEntity Create(EntityLink link) =>
            Create(Vector3.zero);

        public ProtoEntity Create(Vector3 position)
        {
            var entity = _pool.Get();
            entity.GetTransform().Value.position = position;
            
            return entity;
        }
        
        private ProtoEntity Create()
        {
            CharacterRangeModule resourceObject = _assetCollector.Get<CharacterRangeModule>();
            CharacterRangeModule gameObject = Object.Instantiate(resourceObject);
            EntityLink link = gameObject.GetComponent<EntityLink>();
            
            CharacterRangeConfig config = _assetCollector.Get<CharacterRangeConfig>();
            CharacterRangeModule module = link.GetModule<CharacterRangeModule>();
            
            Aspect.CharacterRange.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            //Components
            entity.AddCharacter();
            entity.AddTransform(link.transform);
            entity.AddGameObject(link.gameObject);
            entity.AddAnimancerEcs(module.Animancer);
            entity.AddAnimancerState(null);
            entity.AddActive();

            //Stats
            entity.AddCharacterType(CharacterType.Range);
            entity.AddEnemiesFindRange(config.FindRange);
            entity.AddAttackPower(4);
            
            //Rotation
            entity.AddRotationSpeed(config.RotationSpeed);
            entity.AddChangeRotationSpeedDelta(config.ChangeRotationSpeedDelta);
            
            //Health
            entity.AddHealth(config.Health);
            entity.AddMaxHealth(config.Health);
            entity.AddLookAt(module.HealthBarTransform);
            entity.AddHealthBar(module.HealthBarImage);
            
            //Particles
            entity.AddShootParticle(module.ShootParticle);
            entity.AddHealParticle(module.HealthParticle);
            
            //Fsm
            module.BehaviourTreeOwner.InitGraphOwner(_container, entity);
            
            return entity;
        }
    }
}