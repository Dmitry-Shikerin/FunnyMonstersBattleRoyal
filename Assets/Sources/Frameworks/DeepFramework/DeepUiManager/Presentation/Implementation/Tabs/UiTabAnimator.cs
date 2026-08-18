using DG.Tweening;
using Dott;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs
{
    //TODO обобщить с Ui Toggle
    public class UiTabAnimator : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Tab Animator</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        [SerializeField] private UiTab _tab;
        [SerializeField] private DOTweenTimeline _enableTimeline;
        [SerializeField] private DOTweenTimeline _disableTimeline;

        private Sequence _enableSequence;
        private Sequence _disableSequence;

        private void Awake() => 
            _tab.StateChanged += OnStateChanged;        
        
        private void OnDestroy() => 
            _tab.StateChanged -= OnStateChanged;

        private void OnStateChanged(EnableState obj)
        {
            if (obj == EnableState.On)
            {
                _disableSequence?.Kill();
                _enableSequence = _enableTimeline.Restart();
            }
            else
            {
                _enableSequence?.Kill();
                _disableSequence = _disableTimeline.Restart();
            }
        }
    }
}