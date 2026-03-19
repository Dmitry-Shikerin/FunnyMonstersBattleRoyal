using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
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
    public class CharacterEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;

        private readonly DiContainer _container;
        private readonly IEntityPool _pool;

        public CharacterEntityFactory(
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
            // _pool = entityPoolManager.GetPool<CharacterTag>();
            // _pool.InitPool(Create);
        }

        // public override ProtoEntity Create(EntityLink link) =>
        //     Create(Vector3.zero);
        //
        // public ProtoEntity Create(Vector3 position)
        // {
        //     ProtoEntity entity = _pool.Get();
        //     entity.GetTransform().Value.position = position;
        //     
        //     return entity;
        // }
        
        public override ProtoEntity Create(EntityLink link)
        {
            // CharacterModule resourceObject = _assetCollector.Get<CharacterModule>();
            // CharacterModule gameObject = Object.Instantiate(resourceObject);
            // EntityLink link = gameObject.GetComponent<EntityLink>();
            
            CharacterConfig config = _assetCollector.Get<CharacterConfig>();
            CharacterModule module = link.GetModule<CharacterModule>();
            
            Aspect.Character.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);

            //Components
            entity.AddTransform(link.transform);
            entity.AddGameObject(link.gameObject);
            entity.AddAnimancerEcs(module.Animancer);
            entity.AddAnimancerState(null);
            entity.AddCharacterController(module.CharacterController);
            entity.AddActive();
            
            //Stats
            entity.AddAttackPower(4);
            // entity.AddCharacterMeleeConfig(config);

            //Rotation
            // entity.AddRotationSpeed(config.RotationSpeed);
            // entity.AddChangeRotationSpeedDelta(config.ChangeRotationSpeedDelta);
            
            //Health
            // entity.AddLookAt(module.HealthBarTransform);
            // entity.AddHealthBar(module.HealthBarImage);

            module.FsmOwner.InitGraphOwner(_container, entity);
            
            return entity;
        }
    }
}