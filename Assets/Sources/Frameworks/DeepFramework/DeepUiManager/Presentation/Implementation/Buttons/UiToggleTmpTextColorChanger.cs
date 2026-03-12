using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Tabs;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using TMPro;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    public class UiToggleTmpTextColorChanger : MonoBehaviour
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Toggle Tmp Text Color Changer</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [SerializeField] private UiTab _controller;
        [SerializeField] private TMP_Text _targetText;
        [SerializeField] private Color _enabledColor;
        [SerializeField] private Color _disabledColor;
        
        private void Awake()
        {
            OnStateChanged(_controller.State);
            _controller.StateChanged += OnStateChanged;
        }

        private void OnDestroy() => 
            _controller.StateChanged -= OnStateChanged;
        
        private void OnStateChanged(EnableState state)
        {
            _targetText.color = state == EnableState.On ? _enabledColor : _disabledColor;
        }
    }
}