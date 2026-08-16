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
    public class MusicVolumeViewComponent : SettingsViewComponentBase
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
            _muteToggle.StateChanged += OnMuteToggleStateChanged;
        }

        protected void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _muteToggle.StateChanged -= OnMuteToggleStateChanged;
        }

        public override void UpdateView()
        {
            //Slider
            float musicValue = SettingsView.Data.MusicVolume;
            Volume = musicValue;
            float sliderValue = GetSliderValue(musicValue);
            _slider.value = sliderValue;
            //Text
            _sliderValue.text = sliderValue.ToString();
            //Mute
            IsMute = SettingsView.Data.IsMusicMuted;
            EnableState mute = IsMute ? EnableState.Off : EnableState.On;
            _muteToggle.SetState(mute);
        }

        public override void CancelSettings()
        {
            SettingsSaveData data = SettingsView.Data;
            SetSettings(data.MusicVolume, data.IsMusicMuted);
        }

        public override void ResetToDefaultSettings()
        {
            SetSettings(_config.MusicVolume, _config.IsMutedMusic);
            ApplySettings();
        }

        public override void ApplySettings()
        {
            SettingsView.Data.MusicVolume = Volume;
            SettingsView.Data.IsMusicMuted = IsMute;
        }

        private void SetSettings(float volume, bool isMuted)
        {
            float sliderValue = GetSliderValue(volume);
            float nextVolume = sliderValue.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeMusicVolume(nextVolume);
            Volume = volume;
            IsMute = isMuted;
            UpdateView();
        }

        private void ChangeVolume(float value)
        {
            float nextVolume = value.Normalize(_slider.minValue, _slider.maxValue);
            _sliderValue.text = value.ToString();
            _soundService.ChangeMusicVolume(nextVolume);
            Volume = nextVolume;
            SettingsView.SetChange(true);
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            if (IsInitialized == false)
                return;
            
            SettingsView.SetChange(true);
            
            if (state == EnableState.On)
            {
                _soundService.UnmuteMusic();
                IsMute = false;
                return;
            }
            
            _soundService.MuteMusic();
            IsMute = true;
        }
        
        private float GetSliderValue(float value) =>
            Mathf.Clamp(Mathf.RoundToInt(value * 100f), _slider.minValue, _slider.maxValue);
    }
}