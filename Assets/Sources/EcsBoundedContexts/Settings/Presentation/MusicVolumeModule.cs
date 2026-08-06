using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Settings.Domain.Data;
using Sources.EcsBoundedContexts.Settings.Presentation.Interfaces;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class MusicVolumeModule : EntityModule, ISettingsModule
    {
        [Required] [SerializeField] private TMP_Text _sliderValue;
        [Required] [SerializeField] private UiToggle _muteToggle;
        [Required] [SerializeField] private Slider _slider;
        
        private ISoundService _soundService;
        private SettingsConfig _config;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
            _muteToggle.StateChanged += OnMuteToggleStateChanged;
        }

        protected override void OnAfterDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _muteToggle.StateChanged -= OnMuteToggleStateChanged;
        }

        public void UpdateView()
        {
            //Slider
            float musicValue = Entity.GetMusicVolume().Value;
            float sliderValue = GetSliderValue(musicValue);
            _slider.value = sliderValue;
            //Text
            _sliderValue.text = sliderValue.ToString();
            
            //Mute
            if (Entity.HasMutedMusicVolume())
                _muteToggle.SetState(EnableState.Off);
            else
                _muteToggle.SetState(EnableState.On);
        }

        public void CancelSettings()
        {
            SettingsSaveData data = Entity.GetSavedSettings().Value;
            SetSettings(data.MusicVolume, data.IsMusicMuted);
        }

        public void ResetToDefaultSettings() =>
            SetSettings(_config.MusicVolume, _config.IsMutedMusic);

        private void SetSettings(float volume, bool isMuted)
        {
            float sliderValue = GetSliderValue(volume);
            float nextVolume = sliderValue.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeMusicVolume(nextVolume);
            Entity.ReplaceMusicVolume(nextVolume);

            if (isMuted && Entity.HasMutedMusicVolume() == false)
                Entity.AddMutedMusicVolume();
            
            if (isMuted == false && Entity.HasMutedMusicVolume())
                Entity.DelMutedMusicVolume();
            
            UpdateView();
        }

        private void ChangeVolume(float value)
        {
            float nextVolume = value.Normalize(_slider.minValue, _slider.maxValue);
            _sliderValue.text = value.ToString();
            _soundService.ChangeMusicVolume(nextVolume);
            Entity.ReplaceMusicVolume(nextVolume);
            AddChangeSettings();
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            if (IsInitialized == false)
                return;
            
            AddChangeSettings();
            
            if (state == EnableState.On)
            {
                _soundService.UnmuteMusic();
                Entity.DelMutedMusicVolume();
                return;
            }
            
            _soundService.MuteMusic();
            Entity.AddMutedMusicVolume();
        }

        private void AddChangeSettings()
        {
            if (Entity.HasInitialized() == false)
                return;
            
            if (Entity.HasChangedSettings())
                return;

            Entity.AddChangedSettings();
        }
        
        private float GetSliderValue(float value) =>
            Mathf.Clamp(Mathf.RoundToInt(value * 100f), _slider.minValue, _slider.maxValue);

        [Inject]
        private void Construct(
            ISoundService soundService,
            IAssetCollector assetCollector)
        {
            _soundService = soundService;
            _config = assetCollector.Get<SettingsConfig>();
        }
    }
}