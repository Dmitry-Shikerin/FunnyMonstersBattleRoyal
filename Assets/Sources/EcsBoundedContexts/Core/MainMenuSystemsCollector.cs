using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Volumes.Controllers.Data;
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
			ChangeVolumeSystem changeVolumeSystem, //Order: 83 //Common
			VolumeSaveSystem volumeSaveSystem //Order: 501 //Common
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				volumeLoadSystem, //Common
				changeVolumeSystem, //Common
				volumeSaveSystem, //Common
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
