using Fusion;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class PlayerViewComponent : NetworkBehaviour, IViewComponent
    {
        private IDataService _dataService;
        
        [Networked]
        public NetworkString<_32> Name { get; set; }

        public PlayerRef PlayerRef { get; private set; }

        [Inject]
        private void Construct(IDataService dataService) =>
            _dataService = dataService;

        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
            
            if (Runner.LocalPlayer != playerRef)
                return;
            
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            SetName_Rpc(playerSaveData.PlayerName);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void SetName_Rpc(NetworkString<_32> playerName)
        {
            Name = name;
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
    }
}