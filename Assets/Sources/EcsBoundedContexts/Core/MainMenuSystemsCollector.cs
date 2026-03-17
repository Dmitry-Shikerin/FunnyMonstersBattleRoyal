using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
using Sources.EcsBoundedContexts.Achievements.Controllers.SaveLoads;
using Sources.EcsBoundedContexts.Achievements.Controllers.Base;
using Sources.EcsBoundedContexts.Cameras.Controllers;
using Sources.EcsBoundedContexts.DailyRewards.Controllers;
using Sources.EcsBoundedContexts.Volumes.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public class MainMenuSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public MainMenuSystemsCollector(
			ProtoSystems protoSystems,
			VolumeLoadSystem volumeLoadSystem, //Order: 7 //Common
			DailyRewardLoadSystem dailyRewardLoadSystem, //Order: 15 //Common
			AchievementsLoadSystem achievementsLoadSystem, //Order: 17 //Achievements
			SelectAchievementSystem selectAchievementSystem, //Order: 18 //Achievements
			MainCameraInitializeSystem mainCameraInitializeSystem, //Order: 51 //Camera
			DailyRewardSystem dailyRewardSystem, //Order: 56 //Common
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			DailyRewardSaveSystem dailyRewardSaveSystem, //Order: 510 //Common
			AchievementsSaveSystem achievementsSaveSystem //Order: 513 //Achievements
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				volumeLoadSystem, //Common
				dailyRewardLoadSystem, //Common
				achievementsLoadSystem, //Achievements
				selectAchievementSystem, //Achievements
				mainCameraInitializeSystem, //Camera
				dailyRewardSystem, //Common
				changeVolumeSystem, //Common
				volumeSaveSystem, //Common
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
