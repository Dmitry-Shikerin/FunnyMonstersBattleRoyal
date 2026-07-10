using Reflex.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class LobbyView : MonoBehaviour
    {
        private IUiViewService _uiViewService;

        [Inject]
        public void Construct(IUiViewService uiViewService)
        {
            _uiViewService = uiViewService;
        }
        
        // public void Connect()
        // {
        //     PhotonNetwork.ConnectUsingSettings();
        //     _uiViewService.Show(UiViewId.Wait);
        // }
        //
        // public override void OnConnectedToMaster()
        // {
        //     PhotonNetwork.JoinLobby();
        // }
        //
        // public override void OnJoinedLobby()
        // {
        //     _uiViewService.Show(UiViewId.CreateRoom);
        // }
        //
        // public override void OnDisconnected(DisconnectCause cause)
        // {
        //     Debug.Log($"Disconnected {cause}");
        // }
    }
}