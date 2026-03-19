using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.AnimatorLod.Controllers;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Tutorials.Controllers.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;
using Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data;
using Sources.EcsBoundedContexts.Input.Controllers;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers;
using Sources.EcsBoundedContexts.Cameras.Controllers;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
using Sources.EcsBoundedContexts.Movements.Move.Systems;
using Sources.EcsBoundedContexts.Damage.Controllers;
using Sources.EcsBoundedContexts.GameCompleted.Controllers;
using Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers;
using Sources.EcsBoundedContexts.LookAt.Controllers;
using Sources.EcsBoundedContexts.Movements.Rotation.Systems;
using Sources.EcsBoundedContexts.Timers.Infrastructure;
using Sources.EcsBoundedContexts.Tutorials.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers;
using Sources.EcsBoundedContexts.GameObjects.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public class GameSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public GameSystemsCollector(
			ProtoSystems protoSystems,
			AnimatorLodSystem animatorLodSystem, //Order: 3 //AnimatorLod
			InterstitialAfterWaveSystem interstitialAfterWaveSystem, //Order: 6 //Common
			VolumeLoadSystem volumeLoadSystem, //Order: 7 //Common
			TutorialLoadSystem tutorialLoadSystem, //Order: 9 //Ability
			PlayerWalletLoadSystem playerWalletLoadSystem, //Order: 10 //Ability
			CharacterInitializeSystem characterInitializeSystem, //Order: 10 //Characters
			KillEnemyCounterLoadSystem killEnemyCounterLoadSystem, //Order: 12 //Common
			InputInitializeSystem inputInitializeSystem, //Order: 15 //Characters
			DailyRewardLoadSystem dailyRewardLoadSystem, //Order: 15 //Common
			InputSystem inputSystem, //Order: 50 //Characters
			PlayerWalletSystem playerWalletSystem, //Order: 51 //Ability
			MainCameraInitializeSystem mainCameraInitializeSystem, //Order: 51 //Camera
			DailyRewardSystem dailyRewardSystem, //Order: 56 //Common
			MoveAlongPathSystem moveAlongPathSystem, //Order: 58 //Light
			CleanHealthSystem cleanHealthSystem, //Order: 60 //Player
			DamageSystem damageSystem, //Order: 64 //Common
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
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			TutorialSaveSystem tutorialSaveSystem, //Order: 503 //Tutorial
			PlayerWalletSaveSystem playerWalletSaveSystem, //Order: 504 //Ability
			KillEnemyCounterSaveSystem killEnemyCounterSaveSystem, //Order: 506 //Common
			DailyRewardSaveSystem dailyRewardSaveSystem //Order: 510 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				animatorLodSystem, //AnimatorLod
				interstitialAfterWaveSystem, //Common
				volumeLoadSystem, //Common
				tutorialLoadSystem, //Ability
				playerWalletLoadSystem, //Ability
				characterInitializeSystem, //Characters
				killEnemyCounterLoadSystem, //Common
				inputInitializeSystem, //Characters
				dailyRewardLoadSystem, //Common
				inputSystem, //Characters
				playerWalletSystem, //Ability
				mainCameraInitializeSystem, //Camera
				dailyRewardSystem, //Common
				moveAlongPathSystem, //Light
				cleanHealthSystem, //Player
				damageSystem, //Common
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
				volumeSaveSystem, //Common
				tutorialSaveSystem, //Tutorial
				playerWalletSaveSystem, //Ability
				killEnemyCounterSaveSystem, //Common
				dailyRewardSaveSystem, //Common
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
