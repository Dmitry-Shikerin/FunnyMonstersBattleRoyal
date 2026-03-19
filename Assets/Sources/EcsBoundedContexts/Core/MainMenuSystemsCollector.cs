using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.DailyRewards.Controllers.Data;
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
			MainCameraInitializeSystem mainCameraInitializeSystem, //Order: 51 //Camera
			DailyRewardSystem dailyRewardSystem, //Order: 56 //Common
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			DailyRewardSaveSystem dailyRewardSaveSystem //Order: 510 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				volumeLoadSystem, //Common
				dailyRewardLoadSystem, //Common
				mainCameraInitializeSystem, //Camera
				dailyRewardSystem, //Common
				changeVolumeSystem, //Common
				volumeSaveSystem, //Common
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
