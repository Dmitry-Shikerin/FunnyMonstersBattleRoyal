using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Controllers.Data;
using Sources.EcsBoundedContexts.Spawners.Controllers;
using Sources.EcsBoundedContexts.Settings.Controllers;
using Sources.EcsBoundedContexts.Characters.Controllers.Systems;

namespace Sources.EcsBoundedContexts.Core
{
	public class LobbySystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public LobbySystemsCollector(
			ProtoSystems protoSystems,
			SettingsLoadSystem settingsLoadSystem, //Order: 10 //Ability
			PlayerLoadSystem playerLoadSystem, //Order: 10 //Ability
			SpawnPointsInitializeSystem spawnPointsInitializeSystem, //Order: 11 //Ability
			ChangedSettingsSystem changedSettingsSystem, //Order: 50 //Ability
			CharacterUpdateSystem characterUpdateSystem, //Order: 55 //Characters
			SettingsSaveSystem settingsSaveSystem, //Order: 504 //Ability
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				settingsLoadSystem, //Ability
				playerLoadSystem, //Ability
				spawnPointsInitializeSystem, //Ability
				changedSettingsSystem, //Ability
				characterUpdateSystem, //Characters
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
