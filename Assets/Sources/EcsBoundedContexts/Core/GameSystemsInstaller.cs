using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.EcsBoundedContexts.CharacterSpawner.Controllers.Data;
using Sources.EcsBoundedContexts.EnemySpawners.Controllers.SaveLoads;
using Sources.EcsBoundedContexts.EnemySpawners.Controllers;
using Sources.EcsBoundedContexts.AnimatorLod.Controllers;
using Sources.EcsBoundedContexts.ApplyAbility.Controllers;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Upgrades.Controllers.Data;
using Sources.EcsBoundedContexts.Tutorials.Controllers.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data;
using Sources.EcsBoundedContexts.NukeAbilities.Controllers.Data;
using Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data;
using Sources.EcsBoundedContexts.HealthBoosters.Controllers.Data;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.SwingingTrees.Controllers.Systems;
using Sources.EcsBoundedContexts.Bunker.Controllers.SaveDatas;
using Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Upgrades.Controllers;
using Sources.EcsBoundedContexts.BurnAbilities.Controllers;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers;
using Sources.EcsBoundedContexts.Cameras.Controllers;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
using Sources.EcsBoundedContexts.CharacterSpawner.Controllers;
using Sources.EcsBoundedContexts.Movements.Move.Systems;
using Sources.EcsBoundedContexts.NukeAbilities.Controllers;
using Sources.EcsBoundedContexts.Damage.Controllers;
using Sources.EcsBoundedContexts.Enemies.Controllers.Systems;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Controllers;
using Sources.EcsBoundedContexts.GameCompleted.Controllers;
using Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers;
using Sources.EcsBoundedContexts.HealthBoosters.Controllers;
using Sources.EcsBoundedContexts.Bunker.Controllers;
using Sources.EcsBoundedContexts.LookAt.Controllers;
using Sources.EcsBoundedContexts.Movements.Rotation.Systems;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.Tutorials.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers;
using Sources.EcsBoundedContexts.GameObjects.Controllers;
using Sources.EcsBoundedContexts.SaveAfterWaves.Controllers;
using Sources.EcsBoundedContexts.Achievements.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public static class GameSystemsInstaller
	{
		public static void InstallBindings(DiContainer container)
		{
			//Default

			//Common
			container.Bind<InterstitialAfterWaveSystem>();
			container.Bind<VolumeLoadSystem>();
			container.Bind<UpgradeLoadSystem>();
			container.Bind<KillEnemyCounterLoadSystem>();
			container.Bind<HealthBusterLoadSystem>();
			container.Bind<DailyRewardLoadSystem>();
			container.Bind<BunkerLoadSystem>();
			container.Bind<DailyRewardSystem>();
			container.Bind<DamageSystem>();
			container.Bind<GameCompletedSystem>();
			container.Bind<GameOverSystem>();
			container.Bind<HealthBusterSystem>();
			container.Bind<BunkerDamageSystem>();
			container.Bind<NavMeshMoveSystem>();
			container.Bind<ChangeVolumeSystem>();
			container.Bind<ActiveGameObjectSystem>();
			container.Bind<VolumeSaveSystem>();
			container.Bind<UpgradeSaveSystem>();
			container.Bind<KillEnemyCounterSaveSystem>();
			container.Bind<HealthBusterSaveSystem>();
			container.Bind<DailyRewardSaveSystem>();
			container.Bind<BunkerSaveSystem>();

			//EventBuffer

			//Player
			container.Bind<CleanHealthSystem>();
			container.Bind<MoveSpeedSystem>();
			container.Bind<RotationSpeedSystem>();
			container.Bind<RotationSystem>();

			//Tree

			//Camera
			container.Bind<MainCameraInitializeSystem>();

			//AnimatorLod
			container.Bind<AnimatorLodSystem>();

			//Light
			container.Bind<MoveAlongPathSystem>();
			container.Bind<LookAtCameraSystem>();

			//Chunks

			//Ability
			container.Bind<CharacterSpawnerLoadSystem>();
			container.Bind<EnemySpawnerLoadSystem>();
			container.Bind<EnemySpawnSystem>();
			container.Bind<AbilityApplierSystem>();
			container.Bind<TutorialLoadSystem>();
			container.Bind<PlayerWalletLoadSystem>();
			container.Bind<NukeAbilityLoadSystem>();
			container.Bind<FlamethrowerAbilityLoadSystem>();
			container.Bind<TreeSwingInitSystem>();
			container.Bind<UpgradeSystem>();
			container.Bind<BurnSystem>();
			container.Bind<PlayerWalletSystem>();
			container.Bind<UpdateAvailabilityUpgradeButtonsSystem>();
			container.Bind<CharacterSpawnSystem>();
			container.Bind<NukeAbilitiesSystem>();
			container.Bind<FlamethrowerAbilitySystem>();
			container.Bind<TreeSwingerSystem>();
			container.Bind<TimerSystem>();
			container.Bind<TutorialSystem>();
			container.Bind<SaveAfterWaveSystem>();
			container.Bind<PlayerWalletSaveSystem>();
			container.Bind<NukeAbilitySaveSystem>();
			container.Bind<FlamethrowerAbilitySaveSystem>();
			container.Bind<EnemySpawnerSaveSystem>();
			container.Bind<CharacterSpawnerSaveSystem>();

			//Characters
			container.Bind<ClearTargetEnemySystem>();
			container.Bind<FindEnemiesSystem>();
			container.Bind<MeleeMassAttackSystem>();
			container.Bind<RotationToEnemySystem>();
			container.Bind<CleanTargetCharacterSystem>();

			//Enemy
			container.Bind<ExplodeSystem>();
			container.Bind<FindCharactersSystem>();
			container.Bind<MassAttackSystem>();

			//Upgrade

			//Achievements
			container.Bind<AchievementsLoadSystem>();
			container.Bind<SelectAchievementSystem>();
			container.Bind<AllAbilitiesUsedAchievementSystem>();
			container.Bind<FiftyWaveCompletedAchievementSystem>();
			container.Bind<FirstEnemyKillAchievementSystem>();
			container.Bind<FirstHealthBoosterUsageAchievementSystem>();
			container.Bind<FirstUpgradeAchievementSystem>();
			container.Bind<FirstWaveCompletedAchievementSystem>();
			container.Bind<MaxUpgradeAchievementSystem>();
			container.Bind<ScullsDiggerAchievementSystem>();
			container.Bind<AchievementsSaveSystem>();

			//Tutorial
			container.Bind<TutorialSaveSystem>();

		}
	}
}
