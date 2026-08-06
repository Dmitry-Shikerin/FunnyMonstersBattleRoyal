using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
using Sources.EcsBoundedContexts.Volumes.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public static class MainMenuSystemsInstaller
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
			containerBuilder.RegisterType(typeof(SettingsSaveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//Characters

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial

		}
	}
}
