using Fusion;
using Sources.Frameworks.ViewComponents.Presentation;

namespace Sources.BoundedContexts.Players.Presentation
{
    public class PlayerReadyView : NetworkBehaviour, IViewComponent
    {
        private PlayerReadyUiView _view;

        [Networked]
        [OnChangedRender(nameof(ChangeUiReady))]
        private NetworkBool IsReady { get; set; }
        
        public PlayerRef PlayerRef { get; private set; }
        
        public void Init(PlayerRef playerRef) =>
            PlayerRef = playerRef;

        public void Construct(PlayerReadyUiView view)
        {
            _view = view;
            _view.SetReady(IsReady);
        }

        public void ChangeReady(bool isReady)
        {
            if (Runner.IsClient)
            {
                ChangeReady_Rpc(new NetworkBool(isReady));
                return;
            }
            
            IsReady = isReady;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal = false)]
        private void ChangeReady_Rpc(NetworkBool isReady) =>
            IsReady = isReady;

        private void ChangeUiReady() =>
            _view.SetReady(IsReady);
    }
}