using System.Collections.Generic;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.NetworkCore.Services;
using Sources.BoundedContexts.Players.Presentation;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.ReadyCheckers
{
    public class PlayersReadyInitializerUiView : MonoBehaviour
    {
        [Required] [SerializeField] private List<PlayerReadyUiView> _views;
        
        private JoinManager _joinManager;

        [Inject]
        private void Construct(JoinManager joinManager)
        {
            gameObject.SetActive(false);
            _joinManager = joinManager;
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            if (_joinManager == null)
                return;
            
            _joinManager.OnPlayersChanged += OnPlayersChanged;
        }

        private void OnDisable()
        {
            if (_joinManager == null)
                return;
            
            _joinManager.OnPlayersChanged -= OnPlayersChanged;
        }

        public void Initialize()
        {
            OnPlayersChanged();
        }

        private void OnPlayersChanged()
        {
            Debug.Log($"OnPlayerChanged");
            
            foreach (PlayerReadyUiView view in _views)
                view.HideInfo();

            for (int i = 0; i < _joinManager.PlayersObjects.Count; i++)
            {
                if (_joinManager == null)
                    Debug.Log($"null");

                if (_joinManager.PlayersObjects[i] == null)
                    Debug.Log($"null");

                if (_joinManager.PlayersObjects[i].GetComponent<ViewComponentsLink>() == null)
                    Debug.Log($"Null");
                
                ViewComponentsLink link = _joinManager.PlayersObjects[i].GetComponent<ViewComponentsLink>();
                PlayerReadyView playerReadyView = link.Get<PlayerReadyView>();
                string playerName = link.Get<PlayerViewComponent>().Name.Value;
                PlayerReadyUiView uiView = _views[i];
                uiView.SetName(playerName);
                playerReadyView.Construct(uiView);
            }
        }
    }
}