using Leopotam.EcsProto;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using Sources.EcsBoundedContexts.Achievements.Infrastructure;
using Sources.EcsBoundedContexts.ApplyAbility.Infrastructure;
using Sources.EcsBoundedContexts.Bunker.Infrastructure;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.CharacterSpawner.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DailyRewards.Infrastructure;
using Sources.EcsBoundedContexts.Enemies.Infrastructure.Factories;
using Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Factories;
using Sources.EcsBoundedContexts.EnemySpawners.Infrastructure.Services;
using Sources.EcsBoundedContexts.ExplosionBodies.Infrastructure;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Infrastructure;
using Sources.EcsBoundedContexts.HealthBoosters.Infrastructure;
using Sources.EcsBoundedContexts.KillEnemyCounters.Infrastructure;
using Sources.EcsBoundedContexts.Lights.Infrastructure;
using Sources.EcsBoundedContexts.NukeAbilities.Infrastructure;
using Sources.EcsBoundedContexts.PlayerWallets.Infrastructure;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories;
using Sources.EcsBoundedContexts.Tutorials.Infrastructure;
using Sources.EcsBoundedContexts.Upgrades.Infrastructure;
using Sources.EcsBoundedContexts.Volumes.Infrastructure;
using Sources.Frameworks.GameServices.EntityPools.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Features;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.MyLeoEcsProto.Repositories.Impl;

namespace Sources.App.DIContainers.Common
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<IEcsGameStartUp, EcsGameStartUp>();
            GameAspect aspect = new GameAspect();
            ProtoWorld world = new ProtoWorld(aspect);
            ProtoSystems systems = new ProtoSystems(world);
            container.Bind(world);
            container.Bind(aspect);
            container.Bind(systems);
            container.Bind<IEventBuffer, EventBuffer>();
            container.Bind<IEntityRepository, EntityRepository>();
            container.Bind<IFeatureService, FeatureService>();
            
            //Pools
            container.Bind<IEntityPoolManager, EntityPoolManager>();
            
            //Characters
            container.Bind<CharacterSpawnerEntityFactory>();
            container.Bind<CharacterMeleeEntityFactory>();
            container.Bind<CharacterRangeEntityFactory>();
            container.Bind<CharacterSpawnPointEntityFactory>();

            //Enemies
            container.Bind<ISpawnService, SpawnService>();
            container.Bind<EnemySpawnerEntityFactory>();
            container.Bind<EnemyBossEntityFactory>();
            container.Bind<EnemyEntityFactory>();
            container.Bind<EnemyKamikazeEntityFactory>();
            container.Bind<EnemySpawnPointEntityFactory>();
            
            //Trees
            container.Bind<TreeSwingEntityFactory>();
            
            //Cameras
            container.Bind<MainCameraEntityFactory>();
            
            //Lights
            container.Bind<LightEntityFactory>();
            
            //Bunkers
            container.Bind<BunkerEntityFactory>();
            
            //Abilities
            container.Bind<AbilityApplierEntityFactory>();
            container.Bind<FlamethrowerEntityFactory>();
            container.Bind<FlamethrowerAbilityEntityFactory>();
            container.Bind<NukeAbilityEntityFactory>();
            container.Bind<NukeBombEntityFactory>();
            
            //ExplosionsBodies
            container.Bind<ExplosionBodyEntityFactory>();
            container.Bind<ExplosionBodyBloodyEntityFactory>();
            
            //Wallet
            container.Bind<PlayerWalletEntityFactory>();
            
            //KillEnemyCounter
            container.Bind<KillEnemyCounterEntityFactory>();
            
            //Tutorial
            container.Bind<TutorialEntityFactory>();
            
            //HealthBuster
            container.Bind<HealthBusterEntityFactory>();
            
            //Upgrades
            container.Bind<UpgradeEntityFactory>();
            
            //DailyRewards
            container.Bind<DailyRewardEntityFactory>();
            container.Bind<DailyRewardService>();
            
            //Volume
            container.Bind<VolumeEntityFactory>();
            
            //Achievements
            container.Bind<AchievementEntityFactory>();
        }
    }
}