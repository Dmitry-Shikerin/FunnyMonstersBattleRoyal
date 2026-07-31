using System;
using DG.Tweening;
using Dott;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public class UiSelectableAnimator : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Selectable Animator</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [Required] [SerializeField] private UiSelectable _selectable;
        [Required] [SerializeField] private DOTweenTimeline _onClickTimeline;
        [Required] [SerializeField] private DOTweenTimeline _highlitedEnterTimeline;
        [Required] [SerializeField] private DOTweenTimeline _highlitedExitTimeline;

        private Sequence _onClickSequence;
        private Sequence _highlitedEnterSequence;
        private Sequence _highlitedExitSequence;

        [OnInspectorInit]
        private void Init()
        {
            _selectable = GetComponent<UiSelectable>();
        }
        
        private void Start()
        {
            if (_selectable == null)
                throw new NullReferenceException("UiSelectable is null");
            
            _selectable.OnClick += OnClick;
            _selectable.Highlited += Highlited;
        }

        private void OnDestroy()
        {
            if (_selectable == null)
                return;
            
            _selectable.OnClick -= OnClick;
            _selectable.Highlited -= Highlited;
        }

        private void OnClick()
        {
            _onClickSequence = _onClickTimeline.Restart();
        }

        private void Highlited(bool highlited)
        {
            if (highlited)
            {
                PlayEnterHighlitedTimeline();
                return;
            }
            
            PlayExitHighlitedTimeline();
        }

        private void PlayEnterHighlitedTimeline()
        {
            if (_highlitedEnterTimeline == null)
                return;

            _highlitedExitSequence?.Kill();
            _highlitedEnterSequence = _highlitedEnterTimeline.Restart();
        }  
        
        private void PlayExitHighlitedTimeline()
        {
            if (_highlitedExitTimeline == null)
                return;
            
            _highlitedEnterSequence?.Kill();
            _highlitedExitSequence = _highlitedExitTimeline?.Restart();
        }
    }
}