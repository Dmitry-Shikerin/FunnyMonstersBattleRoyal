using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.EcsBoundedContexts.AnimatorLod.Controllers;
using Sources.EcsBoundedContexts.ApplyAbility.Controllers;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Upgrades.Controllers.Data;
using Sources.EcsBoundedContexts.Tutorials.Controllers.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data;
using Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Upgrades.Controllers;
using Sources.EcsBoundedContexts.BurnAbilities.Controllers;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers;
using Sources.EcsBoundedContexts.Cameras.Controllers;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
using Sources.EcsBoundedContexts.Movements.Move.Systems;
using Sources.EcsBoundedContexts.Damage.Controllers;
using Sources.EcsBoundedContexts.Enemies.Controllers.Systems;
using Sources.EcsBoundedContexts.GameCompleted.Controllers;
using Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers;
using Sources.EcsBoundedContexts.LookAt.Controllers;
using Sources.EcsBoundedContexts.Movements.Rotation.Systems;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.Tutorials.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers;
using Sources.EcsBoundedContexts.GameObjects.Controllers;
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
			container.Bind<DailyRewardLoadSystem>();
			container.Bind<DailyRewardSystem>();
			container.Bind<DamageSystem>();
			container.Bind<GameCompletedSystem>();
			container.Bind<GameOverSystem>();
			container.Bind<NavMeshMoveSystem>();
			container.Bind<ChangeVolumeSystem>();
			container.Bind<ActiveGameObjectSystem>();
			container.Bind<VolumeSaveSystem>();
			container.Bind<UpgradeSaveSystem>();
			container.Bind<KillEnemyCounterSaveSystem>();
			container.Bind<DailyRewardSaveSystem>();

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
			container.Bind<AbilityApplierSystem>();
			container.Bind<TutorialLoadSystem>();
			container.Bind<PlayerWalletLoadSystem>();
			container.Bind<UpgradeSystem>();
			container.Bind<BurnSystem>();
			container.Bind<PlayerWalletSystem>();
			container.Bind<UpdateAvailabilityUpgradeButtonsSystem>();
			container.Bind<TimerSystem>();
			container.Bind<TutorialSystem>();
			container.Bind<PlayerWalletSaveSystem>();

			//Characters
			container.Bind<CleanTargetCharacterSystem>();

			//Enemy
			container.Bind<FindCharactersSystem>();
			container.Bind<MassAttackSystem>();

			//Upgrade

			//Achievements
			container.Bind<AchievementsLoadSystem>();
			container.Bind<SelectAchievementSystem>();
			container.Bind<AllAbilitiesUsedAchievementSystem>();
			container.Bind<FirstEnemyKillAchievementSystem>();
			container.Bind<FirstUpgradeAchievementSystem>();
			container.Bind<MaxUpgradeAchievementSystem>();
			container.Bind<ScullsDiggerAchievementSystem>();
			container.Bind<AchievementsSaveSystem>();

			//Tutorial
			container.Bind<TutorialSaveSystem>();

		}
	}
}
