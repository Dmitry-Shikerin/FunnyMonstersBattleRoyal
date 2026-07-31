using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
using Sources.EcsBoundedContexts.Players.Systems.Data;
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
			PlayerLoadSystem playerLoadSystem, //Order: 10 //Ability
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			VolumeSaveSystem volumeSaveSystem, //Order: 501 //Common
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				volumeLoadSystem, //Common
				playerLoadSystem, //Ability
				changeVolumeSystem, //Common
				volumeSaveSystem, //Common
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
