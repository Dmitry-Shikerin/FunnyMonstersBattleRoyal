using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class UiButton : UiSelectable
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Button</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        [EnumToggleButtons]
        [SerializeField] private EnableState _sendOnClick = EnableState.On;
        [Space(Space)] 
        [ShowIf(nameof(_sendOnClick), EnableState.On)] 
        [Required] [SerializeField] private ButtonId _buttonId = ButtonId.Default;
        [Space(Space)]
        [Required] [SerializeField] private Button _button;
        //TODO зарефакторить делей
        [SerializeField] private bool _isDelayedOnClick;
        [EnableIf("_isDelayedOnClick")]
        [SerializeField] private float _seconds;

        private ButtonsManager _buttonsManager;
        private CancellationTokenSource _token;
        private TimeSpan _delay;

        private void Awake()
        {
            _token = new CancellationTokenSource();
            _delay = TimeSpan.FromSeconds(_seconds);
            _buttonsManager = DeepUiBrain.ButtonsManager;
            
            if (_sendOnClick == EnableState.On && _buttonId != ButtonId.Default)
                _buttonsManager.Register(_buttonId, this);
            
            _button.onClick.AddListener(SendSignal);
        }

        private void OnDestroy()
        {
            if (_sendOnClick == EnableState.On && _buttonId != ButtonId.Default)
                _buttonsManager.Unregister(_buttonId, this);
            
            _button.onClick.RemoveListener(SendSignal);
        }

        private async void SendSignal()
        {
            if (_isDelayedOnClick == false)
            {
                Click();
                return;
            }

            try
            {
                await UniTask.Delay(_delay, cancellationToken: _token.Token);
                Click();
            }
            catch (OperationCanceledException)
            {
            }
        }

        protected override void Click()
        {
            base.Click();
            
            if (_sendOnClick == EnableState.On && _buttonId != ButtonId.Default)
                _buttonsManager.HandleOnClick(_buttonId);
        }

        [OnInspectorInit]
        private void SetButton()
        {
            if(_button == null)
                _button = GetComponent<Button>();
        }
    }
}