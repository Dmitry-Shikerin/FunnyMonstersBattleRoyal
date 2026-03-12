using Dott;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views
{
    public class UiContainerAnimator : MonoBehaviour
    {
        //Const
        private const string UiContainerLabel = "<size=18><b><color=#0000FF><i>Ui Container Animator</i></color></b></size>";
        
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _label = UiContainerLabel;
        
        [Required] [SerializeField] private UiContainerBase _container;
        [Required] [SerializeField] private DOTweenTimeline _showTimeline;
        [Required] [SerializeField] private DOTweenTimeline _hideTimeline;

        [OnInspectorInit]
        private void OnInspectorInit()
        {
            _container = GetComponent<UiPopUpView>();
        }
        
        private void Awake()
        {
            _container.Showed += OnShowed;
            _container.Hided += OnHided;
        }

        private void OnDestroy()
        {
            _container.Showed -= OnShowed;
            _container.Hided -= OnHided;
        }

        private void OnShowed()
        {
            _showTimeline?.Play();
        }

        private void OnHided()
        {
            _hideTimeline?.Play();
        }
    }
}