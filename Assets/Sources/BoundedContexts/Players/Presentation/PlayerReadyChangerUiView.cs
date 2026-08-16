using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.Players.Presentation
{
    public class PlayerReadyChangerUiView : MonoBehaviour
    {
        [Required] [SerializeField] private UiToggle _readyToggle;
        
        private PlayerReadyView _playerReadyView;
        
        public void Construct(PlayerReadyView playerReadyView)
        {
            gameObject.SetActive(false);
            _playerReadyView = playerReadyView;
            _readyToggle.SetState(EnableState.Off);
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            if (_playerReadyView == null)
                return;
            
            ChangeReady(_readyToggle.State);
            _readyToggle.SubscribeStateChange(ChangeReady);
        }

        private void OnDisable()
        {
            if (_playerReadyView == null)
                return;
            
            _readyToggle.UnsubscribeStateChange(ChangeReady);
        }

        private void ChangeReady(EnableState state)
        {
            bool isReady = state == EnableState.On;
            _playerReadyView.ChangeReady(isReady);
        }
    }
}