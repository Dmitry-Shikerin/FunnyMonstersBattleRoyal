using Dott;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public class UiToggleAnimator : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Toggle Animator</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private UiToggle _toggle;
        [SerializeField] private DOTweenTimeline _enableTimeline;
        [SerializeField] private DOTweenTimeline _disableTimeline;
        [SerializeField] private EnableState _startState;

        private void Awake() => 
            _toggle.StateChanged += OnStateChanged;        
        
        private void OnDestroy() => 
            _toggle.StateChanged -= OnStateChanged;

        private void OnStateChanged(EnableState obj)
        {
            if (obj == EnableState.On)
                _enableTimeline.Restart();
            else
                _disableTimeline.Restart();
        }
    }
}