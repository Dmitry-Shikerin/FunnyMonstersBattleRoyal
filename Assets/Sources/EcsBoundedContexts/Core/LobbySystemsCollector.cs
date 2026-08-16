using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Settings.Controllers;
using Sources.EcsBoundedContexts.Players.Controllers.Data;

namespace Sources.EcsBoundedContexts.Core
{
	public class LobbySystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public LobbySystemsCollector(
			ProtoSystems protoSystems,
			SettingsLoadSystem settingsLoadSystem, //Order: 10 //Ability
			ChangedSettingsSystem changedSettingsSystem, //Order: 50 //Ability
			SettingsSaveSystem settingsSaveSystem, //Order: 504 //Ability
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				settingsLoadSystem, //Ability
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
