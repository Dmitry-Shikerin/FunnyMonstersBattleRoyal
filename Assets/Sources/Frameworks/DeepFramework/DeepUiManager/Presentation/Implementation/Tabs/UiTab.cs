using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs
{
    [RequireComponent(typeof(Button))]
    [DisallowMultipleComponent]
    public class UiTab : UiSelectable
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Tab</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private Button _button;
        [SerializeField] private UiTabGroup _tabGroup;
        [SerializeField] private UiContainer _container;

        public event Action<EnableState> StateChanged;
        
        public EnableState State { get; private set; } = EnableState.On;

        private void Awake()
        {
            _button.onClick.AddListener(Show);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Show);
        }

        public void Show()
        {
            _tabGroup.ShowTab(this);
            _container.Show();
            ChangeState(EnableState.On);
            Click();
        }
        
        public void Hide()
        {
            _container.Hide();
            ChangeState(EnableState.Off);
        }

        public void ChangeState(EnableState state)
        {
            State = state;
            StateChanged?.Invoke(state);
        }

        [OnInspectorInit]
        private void Init()
        {
            if (_tabGroup != null)
                return;
            
            _tabGroup = GetComponentInParent<UiTabGroup>();
        }
    }
}