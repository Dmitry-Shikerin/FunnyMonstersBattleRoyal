using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Common.Extancions.Colliders;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.Enemies.Presentation;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Infrastructure.Factories
{
    public class EnemyKamikazeEntityFactory : EntityFactory
    {
        private readonly IAssetCollector _assetCollector;
        private readonly DiContainer _container;
        private readonly IEntityPool _pool;

        public EnemyKamikazeEntityFactory(
            IEntityPoolManager poolManager,
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
            _pool = poolManager.GetPool<EnemyKamikazeTag>();
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
            EnemyKamikazeModule resourceObject = _assetCollector.Get<EnemyKamikazeModule>();
            EnemyKamikazeModule module = Object.Instantiate(resourceObject);
            EntityLink link = module.GetComponent<EntityLink>();
            EnemyKamikazeConfig config = _assetCollector.Get<EnemyKamikazeConfig>();
            
            Aspect.EnemyKamikaze.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            
            entity.AddTransform(link.transform);
            entity.AddGameObject(link.gameObject);
            entity.AddAnimancerEcs(module.Animancer);
            entity.AddAnimancerState(null);
            entity.AddNavMesh(module.NavMeshAgent);
            entity.AddActive();
            
            //Stats
            entity.AddEnemyType(EnemyType.Kamikaze);
            entity.AddMassAttackPower(5);
            
            //Health
            entity.AddHealth(50);
            entity.AddMaxHealth(50);
            entity.AddHealthBar(module.HealthBarImage);
            entity.AddLookAt(module.HealthBarTransform);
            
            //Particles
            entity.AddBurnParticle(module.BurnParticleSystem);
            entity.AddBloodParticle(module.BloodParticleSystem);
            
            module.BehaviourTreeOwner.InitGraphOwner(_container, entity);
            
            return entity;
        }
    }
}