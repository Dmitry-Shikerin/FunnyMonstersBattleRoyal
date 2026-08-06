using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
using Sources.EcsBoundedContexts.Settings.Controllers;

namespace Sources.EcsBoundedContexts.Core
{
	public class MainMenuSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public MainMenuSystemsCollector(
			ProtoSystems protoSystems,
			SettingsLoadSystem settingsLoadSystem, //Order: 10 //Ability
			PlayerLoadSystem playerLoadSystem, //Order: 10 //Ability
			ChangedSettingsSystem changedSettingsSystem, //Order: 50 //Ability
			SettingsSaveSystem settingsSaveSystem, //Order: 504 //Ability
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				settingsLoadSystem, //Ability
				playerLoadSystem, //Ability
				changedSettingsSystem, //Ability
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
