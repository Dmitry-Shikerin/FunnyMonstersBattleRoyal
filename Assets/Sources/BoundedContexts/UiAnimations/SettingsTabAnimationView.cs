using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.BoundedContexts.UiAnimations
{
    public class SettingsTabAnimationView : MonoBehaviour
    {
        [Required] [SerializeField] private UiTab _tab;

        private void OnEnable()
        {
            _tab.StateChanged += ChangeAnimation;
        }

        private void OnDisable()
        {
            _tab.StateChanged -= ChangeAnimation;
        }

        private void ChangeAnimation(EnableState state)
        {
            
        }
    }
}