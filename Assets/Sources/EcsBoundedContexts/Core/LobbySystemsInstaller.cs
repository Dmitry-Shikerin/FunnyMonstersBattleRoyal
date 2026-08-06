using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
using Sources.EcsBoundedContexts.Spawners.Controllers;
using Sources.EcsBoundedContexts.Settings.Controllers;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;

namespace Sources.EcsBoundedContexts.Core
{
	public static class LobbySystemsInstaller
	{
		public static void InstallBindings(ContainerBuilder containerBuilder)
		{
			//Default

			//Common

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
			containerBuilder.RegisterType(typeof(ChangedSettingsSystem), Lifetime.Singleton, Resolution.Lazy);
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
