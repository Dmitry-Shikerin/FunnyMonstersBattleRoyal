using MyDependencies.Sources.Attributes;
using Photon.Pun;
using Photon.Realtime;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class LobbyView : MonoBehaviourPunCallbacks
    {
        private IUiViewService _uiViewService;

        [Inject]
        public void Construct(IUiViewService uiViewService)
        {
            _uiViewService = uiViewService;
        }
        
        public void Connect()
        {
            Debug.Log($"Connect");
            PhotonNetwork.ConnectUsingSettings();
            _uiViewService.Show(UiViewId.Wait);
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log($"Connected to master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log($"on joined lobby");
            _uiViewService.Show(UiViewId.CreateRoom);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"Disconnected {cause}");
        }
    }
}