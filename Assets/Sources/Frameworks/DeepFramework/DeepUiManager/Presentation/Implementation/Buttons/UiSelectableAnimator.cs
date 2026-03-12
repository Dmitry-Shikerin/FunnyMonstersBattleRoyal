using System;
using Dott;
using Sirenix.OdinInspector;
using UnityEngine;

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
        [Required] [SerializeField] private DOTweenTimeline _selectableTimeline;

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
        }

        private void OnDestroy()
        {
            if (_selectable == null)
                return;
            
            _selectable.OnClick -= OnClick;
        }

        private void OnClick()
        {
            _selectableTimeline.Restart();
        }
    }
}