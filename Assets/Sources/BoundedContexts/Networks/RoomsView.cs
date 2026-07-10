using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class RoomsView : MonoBehaviour
    {
        [Required] [SerializeField] private UiButton _button;
        
        private ISceneService _sceneService;

        [Inject]
        private void Construct(ISceneService sceneService) =>
            _sceneService = sceneService;

        // public override void OnJoinedRoom()
        // {
        //     _sceneService.ChangeSceneAsync(IdsConst.Gameplay);
        // }
        //
        // public override void OnEnable()
        // {
        //     base.OnEnable();
        //     _button.OnClick += QuickGame;
        // }
        //
        // public override void OnDisable()
        // {
        //     base.OnDisable();
        //     _button.OnClick -= QuickGame;
        // }
        //
        // private void QuickGame()
        // {
        //     PhotonNetwork.JoinRandomOrCreateRoom();
        // }
    }
}