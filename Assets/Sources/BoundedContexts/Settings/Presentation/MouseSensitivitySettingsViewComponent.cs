using System;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class MouseSensitivitySettingsViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private TMP_Text _sliderValue;
        [Required] [SerializeField] private Slider _slider;

        private SettingsConfig _config;
        public float Sensitivity { get; private set; }

        public event Action<float> OnSensitivityChange;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<SettingsConfig>();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
        }

        protected void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
        }

        public override void UpdateView()
        {
            //Slider
            float sensitivity = SettingsView.Data.MouseSensitivity;
            Sensitivity = sensitivity;
            //float sliderValue = GetSliderValue(sensitivity);
            float sliderValue = sensitivity.MapClamped(
                _config.MinMaxMouseSensitivity.x, _config.MinMaxMouseSensitivity.y, _slider.minValue, _slider.maxValue);
            _slider.value = sliderValue;
            //Text
            _sliderValue.text = sliderValue.ToString();
        }

        public override void CancelSettings()
        {
            SetSettings(SettingsView.Data.MouseSensitivity);
        }

        public override void ResetToDefaultSettings()
        {
            SetSettings(_config.MouseSensitivity);
            SettingsView.Data.MouseSensitivity = _config.MouseSensitivity;
        }

        public override void ApplySettings()
        {
            SettingsView.Data.MouseSensitivity = Sensitivity;
        }

        private void SetSettings(float sensitivity)
        {
            Sensitivity = sensitivity;
            //float sliderValue = GetSliderValue(sensitivity);
            float sliderValue = sensitivity.MapClamped(
                _config.MinMaxMouseSensitivity.x, _config.MinMaxMouseSensitivity.y, _slider.minValue, _slider.maxValue);
            float nextSensitivity = sliderValue.Normalize(_slider.minValue, _slider.maxValue);
            OnSensitivityChange?.Invoke(nextSensitivity);
            UpdateView();
        }

        private void ChangeVolume(float value)
        {
            _sliderValue.text = value.ToString();
            //float nextSensitivity = value.Normalize(_slider.minValue, _slider.maxValue);
            float sliderValue = value.MapClamped(
                _slider.minValue, _slider.maxValue, _config.MinMaxMouseSensitivity.x, _config.MinMaxMouseSensitivity.y);
            OnSensitivityChange?.Invoke(sliderValue);
            Sensitivity = sliderValue;
            SettingsView.SetChange(true);
        }
    }
}