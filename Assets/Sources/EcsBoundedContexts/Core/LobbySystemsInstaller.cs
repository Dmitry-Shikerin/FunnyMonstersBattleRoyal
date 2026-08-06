using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
using Sources.EcsBoundedContexts.Spawners.Controllers;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;
using Sources.EcsBoundedContexts.Volumes.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public static class LobbySystemsInstaller
	{
		public static void InstallBindings(ContainerBuilder containerBuilder)
		{
			//Default

			//Common
			containerBuilder.RegisterType(typeof(VolumeLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(ChangeVolumeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(VolumeSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//EventBuffer

			//Player

			//Tree

			//Camera

			//AnimatorLod

			//Light

			//Chunks

			//Ability
			containerBuilder.RegisterType(typeof(SettingsLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(SpawnPointsInitializeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(SettingsSaveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//Characters
			containerBuilder.RegisterType(typeof(CharacterUpdateSystem), Lifetime.Singleton, Resolution.Lazy);

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial

		}
	}
}
