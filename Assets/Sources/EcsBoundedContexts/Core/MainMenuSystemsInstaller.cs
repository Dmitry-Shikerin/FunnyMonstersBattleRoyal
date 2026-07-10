using Reflex.Core;
using Reflex.Enums;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
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
			containerBuilder.RegisterType(typeof(DailyRewardLoadSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(DailyRewardSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(ChangeVolumeSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(VolumeSaveSystem), Lifetime.Singleton, Resolution.Lazy);
			containerBuilder.RegisterType(typeof(DailyRewardSaveSystem), Lifetime.Singleton, Resolution.Lazy);

			//EventBuffer

			//Player

			//Tree

			//Camera

			//AnimatorLod

			//Light

			//Chunks

			//Ability

			//Characters

			//Enemy

			//Upgrade

			//Achievements

			//Tutorial

		}
	}
}
