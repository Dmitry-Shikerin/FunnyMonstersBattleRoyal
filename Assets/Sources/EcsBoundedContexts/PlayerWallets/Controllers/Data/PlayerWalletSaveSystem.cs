using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Data;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data
{
    [EcsSystem(504)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class PlayerWalletSaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _saveIt = new(
            It.Inc<
                PlayerWalletComponent,
                SaveDataEvent>());
        [DI] private readonly ProtoIt _clearIt = new(
            It.Inc<
                PlayerWalletComponent,
                ClearDataEvent>());

        public PlayerWalletSaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _saveIt)
            {
                int coins = entity.GetPlayerWallet().Value;

                PlayerWalletSaveData data = new PlayerWalletSaveData
                {
                    Id = IdsConst.PlayerWallet,
                    Coins = coins,
                };
                
                _dataService.SaveData(data, IdsConst.PlayerWallet);
            }

            foreach (ProtoEntity entity in _clearIt)
            {
                _dataService.Clear(IdsConst.PlayerWallet);
            }
        }
    }
}