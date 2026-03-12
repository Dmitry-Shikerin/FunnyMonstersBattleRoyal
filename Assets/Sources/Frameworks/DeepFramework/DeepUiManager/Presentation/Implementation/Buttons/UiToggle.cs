using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons
{
    [DisallowMultipleComponent]
    public class UiToggle : UiSelectable
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Ui Toggle</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        [SerializeField] private Button _button;

        public event Action<EnableState> StateChanged;
        private readonly List<Action<EnableState>> _stateCallbacks = new ();
        
        public EnableState State {get; private set; } = EnableState.On;

        private void Awake()
        {
            SetState(EnableState.Off);
            _button.onClick.AddListener(ChangeState);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeState);
        }
        
        public void SubscribeStateChange(Action<EnableState> callback)
        {
            if (_stateCallbacks.Contains(callback))
                throw new ArgumentException("Callback already subscribed");
            
            _stateCallbacks.Add(callback);
        }

        public void UnsubscribeStateChange(Action<EnableState> callback)
        {
            if (_stateCallbacks.Contains(callback) == false)
                throw new ArgumentException("Callback already not subscribed");
            
            _stateCallbacks.Remove(callback);
        }

        public void SetState(EnableState state)
        {
            if (state == State)
                return;
            
            State = state;
            StateChanged?.Invoke(state);

            for (int i = _stateCallbacks.Count - 1; i >= 0; i--)
                _stateCallbacks[i].Invoke(state);
        }

        public void Enable()
        {
            SetState(EnableState.On);
        }
        
        public void Disable()
        {
            SetState(EnableState.Off);
        }

        public void ChangeState()
        {
            if (State == EnableState.On)
            {
                State = EnableState.Off;
            }
            else
            {
                State = EnableState.On;
            }
            
            StateChanged?.Invoke(State);
            Click();
        }
    }
}