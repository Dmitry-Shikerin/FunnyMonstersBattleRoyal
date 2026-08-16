using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Players.Controllers.Data;

namespace Sources.EcsBoundedContexts.Core
{
	public class MainMenuSystemsCollector : ISystemsCollector
	{
		private readonly ProtoSystems _protoSystems;
		private readonly IEnumerable<IProtoSystem> _systems;

		public MainMenuSystemsCollector(
			ProtoSystems protoSystems,
			PlayerLoadSystem playerLoadSystem, //Order: 10 //Ability
			PlayerSaveSystem playerSaveSystem //Order: 504 //Ability
		)
		{
			_protoSystems = protoSystems;
			_systems = new IProtoSystem[]
			{
				playerLoadSystem, //Ability
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
