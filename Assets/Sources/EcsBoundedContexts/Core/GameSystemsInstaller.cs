using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.AnimatorLod.Controllers;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Tutorials.Controllers.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;
using Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data;
using Sources.EcsBoundedContexts.Input.Controllers;
using Sources.EcsBoundedContexts.PlayerWallets.Controllers;
using Sources.EcsBoundedContexts.Cameras.Controllers;
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
		public static void InstallBindings(ContainerBuilder containerBuilder)
		{
			//Default

			//Common
			containerBuilder.RegisterType(typeof(InterstitialAfterWaveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(VolumeLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(KillEnemyCounterLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(DamageSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(GameCompletedSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(GameOverSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(NavMeshMoveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(ChangeVolumeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(ActiveGameObjectSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(VolumeSaveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(KillEnemyCounterSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//EventBuffer

			//Player
			containerBuilder.RegisterType(typeof(CleanHealthSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(MoveSpeedSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(RotationSpeedSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(RotationSystem), Lifetime.Singleton, Resolution.Lazy);

			//Tree

			//Camera
			containerBuilder.RegisterType(typeof(CameraRotationSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(MainCameraInitializeSystem), Lifetime.Singleton, Resolution.Lazy);

			//AnimatorLod
			containerBuilder.RegisterType(typeof(AnimatorLodSystem), Lifetime.Singleton, Resolution.Lazy);

			//Light
			containerBuilder.RegisterType(typeof(MoveAlongPathSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(LookAtCameraSystem), Lifetime.Singleton, Resolution.Lazy);

			//Chunks

			//Ability
			containerBuilder.RegisterType(typeof(TutorialLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerWalletLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerWalletSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(TimerSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(TutorialSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerWalletSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//Characters
			containerBuilder.RegisterType(typeof(CharacterInitializeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(InputInitializeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(InputSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(CharacterUpdateSystem), Lifetime.Singleton, Resolution.Lazy);

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial
			containerBuilder.RegisterType(typeof(TutorialSaveSystem), Lifetime.Singleton, Resolution.Lazy);

		}
	}
}
