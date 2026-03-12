using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Steppers
{
    [DisallowMultipleComponent]
    public class UiStepper : MonoBehaviour
    {
        private const string Label = "<size=18><b><color=#C71585><i>Ui Stepper</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [EnumToggleButtons] [HideLabel]
        [SerializeField] private UiStepperSettingsToggle _settingsToggle = UiStepperSettingsToggle.Settings;
        
        #region Settings
        [ShowIf(nameof(IsSettings))]
        [SerializeField] private Slider _slider;
        [ShowIf(nameof(IsSettings))]
        [SerializeField] private Button _plusButton;
        [ShowIf(nameof(IsSettings))]
        [SerializeField] private Button _minusButton;
        [ShowIf(nameof(IsSettings))]
        [SerializeField] private Button _resetButton;
        [ShowIf(nameof(IsSettings))]
        [SerializeField] private TMP_Text _valueText;
        #endregion
        #region Advanced
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("MinMax")]
        [SerializeField] private float _minValue;
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("MinMax")]
        [SerializeField] private float _maxValue;
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("Current")]
        [SerializeField] private float _currentValue;
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("Current")]
        [SerializeField] private float _changeValue;
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("Default")]
        [SerializeField] private float _defaultValue;
        [ShowIf(nameof(IsAdvanced))]
        [HorizontalGroup("Default")]
        [SerializeField] private bool _isResetToDefautToAwake = true;
        #endregion
        #region CallBacks
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onValueChanged;
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onIncrease;
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onDecrease;
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onReset;
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onMaxValueReached;
        [ShowIf(nameof(IsCallBacks))]
        [SerializeField] private UnityEvent<float> _onMinValueReached;
        
        public event Action<float> ValueChanged;
        public event Action<float> IncreaseValueChanged;
        public event Action<float> DecreaseValueChanged;
        public event Action<float> ResetValueChanged;
        public event Action<float> MaxValueReached;
        public event Action<float> MinValueReached;
        #endregion
        
        private bool IsSettings => _settingsToggle == UiStepperSettingsToggle.Settings;
        private bool IsAdvanced => _settingsToggle == UiStepperSettingsToggle.Advanced;
        private bool IsCallBacks => _settingsToggle == UiStepperSettingsToggle.CallBacks;

        private void Awake()
        {
            if (_isResetToDefautToAwake == false)
                return;
            
            OnReset();
        }

        private void OnEnable()
        {
            _minusButton?.onClick?.AddListener(OnDecrease);
            _plusButton?.onClick?.AddListener(OnIncrease);
            _resetButton?.onClick?.AddListener(OnReset);
        }

        private void OnDisable()
        {
            _minusButton?.onClick?.RemoveListener(OnDecrease);
            _plusButton?.onClick?.RemoveListener(OnIncrease);
            _resetButton?.onClick?.RemoveListener(OnReset);
        }

        public void SetValue(float targetValue)
        {
            float value = Math.Clamp(targetValue, _minValue, _maxValue);

            if (Mathf.Approximately(_slider.value, _maxValue))
                return;
            
            _slider.value = value;
            _currentValue = value;
            _onValueChanged?.Invoke(value);
            ValueChanged?.Invoke(value);

            if (Mathf.Approximately(_slider.value, _maxValue))
            {
                _onMaxValueReached?.Invoke(value);
                MaxValueReached?.Invoke(value);
            }

            if (Mathf.Approximately(_slider.value, _minValue))
            {
                _onMinValueReached?.Invoke(value);
                MinValueReached?.Invoke(value);
            }
        }

        private void OnIncrease()
        {
            float nextValue = _slider.value + _changeValue;
            float value = Math.Clamp(nextValue, _minValue, _maxValue);

            if (Mathf.Approximately(_slider.value, _maxValue))
                return;
            
            _slider.value = value;
            _currentValue = value;
            _onValueChanged?.Invoke(value);
            _onIncrease?.Invoke(value);
            
            ValueChanged?.Invoke(value);
            IncreaseValueChanged?.Invoke(value);
            
            if (Mathf.Approximately(_slider.value, _maxValue) == false)
                return;
            
            _onMaxValueReached?.Invoke(value);
            MaxValueReached?.Invoke(value);
        }

        private void OnDecrease()
        {
            float nextValue = _slider.value - _changeValue;
            float value = Math.Clamp(nextValue, _minValue, _maxValue);
            
            if (Mathf.Approximately(_slider.value, _minValue))
                return;
            
            _slider.value = value;
            _currentValue = value;
            _onValueChanged?.Invoke(value);
            _onDecrease?.Invoke(value);
            
            ValueChanged?.Invoke(value);
            DecreaseValueChanged?.Invoke(value);
            
            if (Mathf.Approximately(_slider.value, _minValue) == false)
                return;
            
            _onMinValueReached?.Invoke(value);
            MinValueReached?.Invoke(value);
        }

        private void OnReset()
        {
            float value = Math.Clamp(_defaultValue, _minValue, _maxValue);
            _slider.value = value;
            _currentValue = value;
            
            _onValueChanged?.Invoke(value);
            _onReset?.Invoke(value);
            
            ValueChanged?.Invoke(value); 
            ResetValueChanged?.Invoke(value);
        }
    }
}