using System.Collections.Generic;
using Leopotam.EcsProto;
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
	public class GameSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public GameSystemsCollector(
			ProtoSystems protoSystems,
			AnimatorLodSystem animatorLodSystem, //Order: 3 //AnimatorLod
			AbilityApplierSystem abilityApplierSystem, //Order: 5 //Ability
			InterstitialAfterWaveSystem interstitialAfterWaveSystem, //Order: 6 //Common
			VolumeLoadSystem volumeLoadSystem, //Order: 7 //Common
			UpgradeLoadSystem upgradeLoadSystem, //Order: 8 //Common
			TutorialLoadSystem tutorialLoadSystem, //Order: 9 //Ability
			PlayerWalletLoadSystem playerWalletLoadSystem, //Order: 10 //Ability
			KillEnemyCounterLoadSystem killEnemyCounterLoadSystem, //Order: 12 //Common
			DailyRewardLoadSystem dailyRewardLoadSystem, //Order: 15 //Common
			AchievementsLoadSystem achievementsLoadSystem, //Order: 17 //Achievements
			SelectAchievementSystem selectAchievementSystem, //Order: 18 //Achievements
			UpgradeSystem upgradeSystem, //Order: 50 //Ability
			BurnSystem burnSystem, //Order: 50 //Ability
			PlayerWalletSystem playerWalletSystem, //Order: 51 //Ability
			MainCameraInitializeSystem mainCameraInitializeSystem, //Order: 51 //Camera
			UpdateAvailabilityUpgradeButtonsSystem updateAvailabilityUpgradeButtonsSystem, //Order: 53 //Ability
			DailyRewardSystem dailyRewardSystem, //Order: 56 //Common
			MoveAlongPathSystem moveAlongPathSystem, //Order: 58 //Light
			CleanHealthSystem cleanHealthSystem, //Order: 60 //Player
			CleanTargetCharacterSystem cleanTargetCharacterSystem, //Order: 61 //Characters
			FindCharactersSystem findCharactersSystem, //Order: 63 //Enemy
			DamageSystem damageSystem, //Order: 64 //Common
			MassAttackSystem massAttackSystem, //Order: 65 //Enemy
			GameCompletedSystem gameCompletedSystem, //Order: 67 //Common
			GameOverSystem gameOverSystem, //Order: 68 //Common
			LookAtCameraSystem lookAtCameraSystem, //Order: 71 //Light
			MoveSpeedSystem moveSpeedSystem, //Order: 72 //Player
			RotationSpeedSystem rotationSpeedSystem, //Order: 73 //Player
			RotationSystem rotationSystem, //Order: 74 //Player
			NavMeshMoveSystem navMeshMoveSystem, //Order: 75 //Common
			TimerSystem timerSystem, //Order: 79 //Ability
			TutorialSystem tutorialSystem, //Order: 80 //Ability
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			ActiveGameObjectSystem activeGameObjectSystem, //Order: 84 //Common
			AllAbilitiesUsedAchievementSystem allAbilitiesUsedAchievementSystem, //Order: 450 //Achievements
			FirstEnemyKillAchievementSystem firstEnemyKillAchievementSystem, //Order: 452 //Achievements
			FirstUpgradeAchievementSystem firstUpgradeAchievementSystem, //Order: 454 //Achievements
			MaxUpgradeAchievementSystem maxUpgradeAchievementSystem, //Order: 456 //Achievements
			ScullsDiggerAchievementSystem scullsDiggerAchievementSystem, //Order: 457 //Achievements
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			UpgradeSaveSystem upgradeSaveSystem, //Order: 502 //Common
			TutorialSaveSystem tutorialSaveSystem, //Order: 503 //Tutorial
			PlayerWalletSaveSystem playerWalletSaveSystem, //Order: 504 //Ability
			KillEnemyCounterSaveSystem killEnemyCounterSaveSystem, //Order: 506 //Common
			DailyRewardSaveSystem dailyRewardSaveSystem, //Order: 510 //Common
			AchievementsSaveSystem achievementsSaveSystem //Order: 513 //Achievements
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				animatorLodSystem, //AnimatorLod
				abilityApplierSystem, //Ability
				interstitialAfterWaveSystem, //Common
				volumeLoadSystem, //Common
				upgradeLoadSystem, //Common
				tutorialLoadSystem, //Ability
				playerWalletLoadSystem, //Ability
				killEnemyCounterLoadSystem, //Common
				dailyRewardLoadSystem, //Common
				achievementsLoadSystem, //Achievements
				selectAchievementSystem, //Achievements
				upgradeSystem, //Ability
				burnSystem, //Ability
				playerWalletSystem, //Ability
				mainCameraInitializeSystem, //Camera
				updateAvailabilityUpgradeButtonsSystem, //Ability
				dailyRewardSystem, //Common
				moveAlongPathSystem, //Light
				cleanHealthSystem, //Player
				cleanTargetCharacterSystem, //Characters
				findCharactersSystem, //Enemy
				damageSystem, //Common
				massAttackSystem, //Enemy
				gameCompletedSystem, //Common
				gameOverSystem, //Common
				lookAtCameraSystem, //Light
				moveSpeedSystem, //Player
				rotationSpeedSystem, //Player
				rotationSystem, //Player
				navMeshMoveSystem, //Common
				timerSystem, //Ability
				tutorialSystem, //Ability
				changeVolumeSystem, //Common
				activeGameObjectSystem, //Common
				allAbilitiesUsedAchievementSystem, //Achievements
				firstEnemyKillAchievementSystem, //Achievements
				firstUpgradeAchievementSystem, //Achievements
				maxUpgradeAchievementSystem, //Achievements
				scullsDiggerAchievementSystem, //Achievements
				volumeSaveSystem, //Common
				upgradeSaveSystem, //Common
				tutorialSaveSystem, //Tutorial
				playerWalletSaveSystem, //Ability
				killEnemyCounterSaveSystem, //Common
				dailyRewardSaveSystem, //Common
				achievementsSaveSystem, //Achievements
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
