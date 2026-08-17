using System.Collections.Generic;
using Leopotam.EcsProto;

namespace Sources.EcsBoundedContexts.Core
{
	public class LobbySystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public LobbySystemsCollector(
			ProtoSystems protoSystems
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
			};
		}

		public void AddSystems()
		{
			foreach (IProtoSystem system in _systems)
				_protoSystems.AddSystem(system);
		}
	}
}
