using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.HealthBoosters.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Cameras.Controllers;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
using Sources.EcsBoundedContexts.HealthBoosters.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public static class MainMenuSystemsInstaller
	{
		public static void InstallBindings(DiContainer container)
		{
			//Default

			//Common
			container.Bind<VolumeLoadSystem>();
			container.Bind<HealthBusterLoadSystem>();
			container.Bind<DailyRewardLoadSystem>();
			container.Bind<DailyRewardSystem>();
			container.Bind<HealthBusterSystem>();
			container.Bind<ChangeVolumeSystem>();
			container.Bind<VolumeSaveSystem>();
			container.Bind<HealthBusterSaveSystem>();
			container.Bind<DailyRewardSaveSystem>();

			//EventBuffer

			//Player

			//Tree

			//Camera
			container.Bind<MainCameraInitializeSystem>();

			//AnimatorLod

			//Light

			//Chunks

			//Ability

			//Characters

			//Enemy

			//Upgrade

			//Achievements
			container.Bind<AchievementsLoadSystem>();
			container.Bind<SelectAchievementSystem>();
			container.Bind<AchievementsSaveSystem>();

			//Tutorial

		}
	}
}
