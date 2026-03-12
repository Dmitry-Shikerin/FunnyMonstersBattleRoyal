using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UiContainerBase : MonoBehaviour
    {
        private const int Spase = 10;
        
        [Space(Spase)]
        [Required] [SerializeField] private bool _isHideGameObject = true;
        [Required] [SerializeField] private bool _isHideCanvas;
        [Required] [SerializeField] private bool _isCanvasGroupBlockRaycast;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private bool _isInit;
        
        public event Action Showed;
        public event Action Hided;
        
        
        [OnInspectorInit]
        private void OnInspectorInit()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            _isInit = true;
        }

        public virtual void Show()
        {
            if (_isHideGameObject)
                gameObject.SetActive(true);
            
            if (_isHideCanvas)
                _canvasGroup.alpha = 1;     
            
            if (_isCanvasGroupBlockRaycast)
                _canvasGroup.blocksRaycasts = true;
            
            Showed?.Invoke();
        }

        public virtual void Hide()
        {
            if (_isHideGameObject)
                gameObject.SetActive(false);
            
            if (_isHideCanvas)
                _canvasGroup.alpha = 0;
            
            if (_isCanvasGroupBlockRaycast)
                _canvasGroup.blocksRaycasts = false;
            
            Hided?.Invoke();
        }
    }
}