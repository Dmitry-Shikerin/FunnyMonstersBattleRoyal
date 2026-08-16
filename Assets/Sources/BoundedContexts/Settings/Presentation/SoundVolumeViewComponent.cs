using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Data;
using Sources.BoundedContexts.Settings.Presentation.Base;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Settings.Presentation
{
    public class SoundVolumeViewComponent : SettingsViewComponentBase
    {
        [Required] [SerializeField] private TMP_Text _sliderValue;
        [Required] [SerializeField] private UiToggle _muteToggle;
        [Required] [SerializeField] private Slider _slider;

        private ISoundService _soundService;
        private SettingsConfig _config;
        public float Volume { get; private set; }
        public bool IsMute { get; private set; }

        [Inject]
        private void Construct(
            ISoundService soundService,
            IAssetCollector assetCollector)
        {
            _soundService = soundService;
            _config = assetCollector.Get<SettingsConfig>();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
            _muteToggle.SubscribeStateChange(OnMuteToggleStateChanged);
        }

        protected void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _muteToggle.UnsubscribeStateChange(OnMuteToggleStateChanged);
        }

        public override void UpdateView()
        {
            //Slider
            float soundVolume = SettingsView.Data.SoundVolume;
            Volume = soundVolume;
            float sliderValue = GetSliderValue(soundVolume);
            _slider.value = sliderValue;
            //Text
            _sliderValue.text = sliderValue.ToString();

            //Mute
            IsMute = SettingsView.Data.IsSoundMuted;
            EnableState state = IsMute ? EnableState.Off : EnableState.On;
            _muteToggle.SetState(state);
        }

        public override void CancelSettings()
        {
            SettingsSaveData data = SettingsView.Data;
            SetSettings(data.SoundVolume, data.IsSoundMuted);
        }

        public override void ResetToDefaultSettings()
        {
            SetSettings(_config.SoundVolume, _config.IsMutedSound);
            SettingsSaveData data = SettingsView.Data;
            data.SoundVolume = _config.SoundVolume;
            data.IsSoundMuted = _config.IsMutedSound;
        }

        public override void ApplySettings()
        {
            SettingsSaveData data = SettingsView.Data;
            data.SoundVolume = Volume;
            data.IsSoundMuted = IsMute;
        }

        private void SetSettings(float volume, bool isMuted)
        {
            Volume = volume;
            IsMute = isMuted;
            float sliderValue = GetSliderValue(volume);
            float nextVolume = sliderValue.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeSoundsVolume(nextVolume);
            UpdateView();
        }

        private void ChangeVolume(float value)
        {
            _sliderValue.text = value.ToString();
            float nextVolume = value.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeSoundsVolume(nextVolume);
            Volume = nextVolume;
            SettingsView.SetChange(true);
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            SettingsView.SetChange(true);

            if (state == EnableState.On)
            {
                _soundService.UnmuteSounds();
                IsMute = false;
                return;
            }

            _soundService.MuteSounds();
            IsMute = true;
        }

        private float GetSliderValue(float value) =>
            Mathf.Clamp(Mathf.RoundToInt(value * 100f), _slider.minValue, _slider.maxValue);
    }
}