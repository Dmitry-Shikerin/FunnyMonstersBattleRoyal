using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Players.Controllers.Data;

namespace Sources.EcsBoundedContexts.Core
{
	public static class MainMenuSystemsInstaller
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
			containerBuilder.RegisterType(typeof(PlayerLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(PlayerSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//Characters

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial

		}
	}
}
