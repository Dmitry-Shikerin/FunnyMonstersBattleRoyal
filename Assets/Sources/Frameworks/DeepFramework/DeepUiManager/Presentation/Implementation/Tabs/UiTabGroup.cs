using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs
{
    public class UiTabGroup : MonoBehaviour
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Tab Group</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private UiTab _firstTab;
        [SerializeField] private List<UiTab> _tabs;

        public UiTab CurrentTab { get; private set; }
        
        private void Awake()
        {
            _firstTab.Show();
            CurrentTab = _firstTab;
        }

        public void ShowTab(UiTab tab)
        {
            if (tab == null)
                throw new ArgumentNullException(nameof(tab));
            
            if (CurrentTab != null && tab == CurrentTab)
                return;
            
            if (_tabs.Contains(tab) == false)
                throw new ArgumentException($"Tab {tab} is not in the group {_tabs}.");

            foreach (UiTab toggle in _tabs)
            {
                if (toggle == tab)
                    continue;
                
                toggle.Hide();
            }
            
            CurrentTab = tab;
        }
    }
}