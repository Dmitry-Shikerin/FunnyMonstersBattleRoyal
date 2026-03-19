using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
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
	public static class GameSystemsInstaller
	{
		public static void InstallBindings(DiContainer container)
		{
			//Default

			//Common
			container.Bind<InterstitialAfterWaveSystem>();
			container.Bind<VolumeLoadSystem>();
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
			container.Bind<TutorialLoadSystem>();
			container.Bind<PlayerWalletLoadSystem>();
			container.Bind<PlayerWalletSystem>();
			container.Bind<TimerSystem>();
			container.Bind<TutorialSystem>();
			container.Bind<PlayerWalletSaveSystem>();

			//Characters
			container.Bind<CharacterInitializeSystem>();
			container.Bind<InputInitializeSystem>();
			container.Bind<InputSystem>();

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial
			container.Bind<TutorialSaveSystem>();

		}
	}
}
