using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Players.Domain.Components;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Players.Systems.Data
{
    [EcsSystem(504)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class PlayerSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                PlayerTag,
                SaveDataEvent>());
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                PlayerTag,
                ClearDataEvent>());

        public PlayerSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                string name = entity.GetPlayerName().Value;

                PlayerSaveData data = new PlayerSaveData
                {
                    Id = IdsConst.Player,
                    Name = name,
                };
                Debug.Log($"Save name {name}");
                _dataService.SaveData(data, IdsConst.Player);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.Player);
            }
        }
    }
}