using Leopotam.EcsProto;
using Reflex.Core;
using Reflex.Enums;
using Reflex.Injectors;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DailyRewards.Infrastructure;
using Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure;
using Sources.EcsBoundedContexts.Input.Infrastructure;
using Sources.EcsBoundedContexts.KillEnemyCounters.Infrastructure;
using Sources.EcsBoundedContexts.Lights.Infrastructure;
using Sources.EcsBoundedContexts.Players.Infrastructure;
using Sources.EcsBoundedContexts.PlayerWallets.Infrastructure;
using Sources.EcsBoundedContexts.Spawners.Infrastructure;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Services;
using Sources.EcsBoundedContexts.Tutorials.Infrastructure;
using Sources.EcsBoundedContexts.Volumes.Infrastructure;
using Sources.Frameworks.GameServices.EntityPools.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Features;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.MyLeoEcsProto.Repositories.Impl;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers.Common
{
    public class EcsInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            GameAspect aspect = new GameAspect();
            ProtoWorld world = new ProtoWorld(aspect);
            ProtoSystems systems = new ProtoSystems(world);
            containerBuilder.RegisterValue(world);
            containerBuilder.RegisterValue(aspect);
            containerBuilder.RegisterValue(systems);
            containerBuilder.RegisterType(typeof(EventBuffer), new[] { typeof(IEventBuffer) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(EntityRepository), new[] { typeof(IEntityRepository) }, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(FeatureService), new[] { typeof(IFeatureService) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Pools
            containerBuilder.RegisterType(typeof(EntityPoolManager), new[] { typeof(IEntityPoolManager) }, Lifetime.Singleton, Resolution.Lazy);
            
            //Characters
            containerBuilder.RegisterType(typeof(CharacterEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Input
            containerBuilder.RegisterType(typeof(InputEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Cameras
            containerBuilder.RegisterType(typeof(MainCameraEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Lights
            containerBuilder.RegisterType(typeof(LightEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //ExplosionsBodies
            containerBuilder.RegisterType(typeof(ExplosionBodyEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(ExplosionBodyBloodyEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Wallet
            containerBuilder.RegisterType(typeof(PlayerWalletEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //KillEnemyCounter
            containerBuilder.RegisterType(typeof(KillEnemyCounterEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Tutorial
            containerBuilder.RegisterType(typeof(TutorialEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //HealthBuster
            
            //DailyRewards
            containerBuilder.RegisterType(typeof(DailyRewardEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(DailyRewardService), Lifetime.Singleton, Resolution.Lazy);
            
            //Volume
            containerBuilder.RegisterType(typeof(VolumeEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //Player
            containerBuilder.RegisterType(typeof(PlayerEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            
            //SpawnPoints
            containerBuilder.RegisterType(typeof(SpawnPointEntityFactory), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(SpawnPointEntitiesProvider), Lifetime.Singleton, Resolution.Lazy);
        }
    }
}