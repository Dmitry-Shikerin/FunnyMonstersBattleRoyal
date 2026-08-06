using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
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
			SettingsLoadSystem settingsLoadSystem, //Order: 10 //Ability
			PlayerLoadSystem playerLoadSystem, //Order: 10 //Ability
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			SettingsSaveSystem settingsSaveSystem, //Order: 504 //Ability
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				volumeLoadSystem, //Common
				settingsLoadSystem, //Ability
				playerLoadSystem, //Ability
				changeVolumeSystem, //Common
				volumeSaveSystem, //Common
				settingsSaveSystem, //Ability
				playerSaveSystem, //Ability
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
