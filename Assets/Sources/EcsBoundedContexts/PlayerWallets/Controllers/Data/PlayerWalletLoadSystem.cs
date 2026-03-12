using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Data;
using Sources.EcsBoundedContexts.PlayerWallets.Infrastructure;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.PlayerWallets.Controllers.Data
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class PlayerWalletLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IDataService _dataService;
        private readonly PlayerWalletEntityFactory _playerWalletEntityFactory;

        public PlayerWalletLoadSystem(
            IUiViewService uiViewService,
            PlayerWalletEntityFactory playerWalletEntityFactory,
            IDataService dataService)
        {
            _uiViewService = uiViewService;
            _playerWalletEntityFactory = playerWalletEntityFactory;
            _dataService = dataService;
        }

        public void Init(IProtoSystems systems)
        {
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();

            //PlayerWallet
            ProtoEntity wallet = _playerWalletEntityFactory.Create(gameplayUiView.Wallet);

            if (_dataService.HasKey(IdsConst.PlayerWallet) == false)
                return;
            
            //Load
            PlayerWalletSaveData playerWalletSaveData =
                _dataService.LoadData<PlayerWalletSaveData>(IdsConst.PlayerWallet);
            wallet.ReplacePlayerWallet(playerWalletSaveData.Coins);
        }
    }
}