using Fusion;
using Reflex.Attributes;
using Sources.BoundedContexts.Players.Presentation.Ui;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Players.Presentation
{
    public class PlayerViewComponent : NetworkBehaviour, IViewComponent
    {
        private IDataService _dataService;
        private PlayerNameUiView _playerNameUiView;

        [Networked]
        public NetworkString<_32> Name { get; set; }

        public PlayerRef PlayerRef { get; private set; }

        [Inject]
        private void Construct(IDataService dataService) =>
            _dataService = dataService;

        public void Construct(PlayerNameUiView playerNameUiView)
        {
            _playerNameUiView = playerNameUiView;
            _playerNameUiView.SetPlayerName(Name.Value);
        }

        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
            
            if (Runner.LocalPlayer != playerRef)
                return;

            if (_dataService.HasKey(IdsConst.Player) == false)
            {
                string playerName = GeneratePlayerName();
                SetName(playerName);
                return;
            }
            
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            SetName(playerSaveData.PlayerName);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            PlayerSaveData data = new PlayerSaveData
            {
                Id = IdsConst.Player,
                PlayerName = Name.Value,
            };
            _dataService.SaveData(data, IdsConst.Player);
        }

        private void SetName(string playerName)
        {
            if (Runner.IsClient)
            {
                SetName_Rpc(playerName);
                return;
            }
            
            Name = playerName;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetName_Rpc(NetworkString<_32> playerName)
        {
            Name = playerName;
        }
        
        private string GeneratePlayerName() =>
            $"PlayerName.{Random.Range(0, 9999)}";
    }
}