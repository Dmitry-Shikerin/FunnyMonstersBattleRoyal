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
    public class SoundVolumeModule : EntityModule, ISettingsModule
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
            float musicValue = Entity.GetSoundVolume().Value;
            float sliderValue = GetSliderValue(musicValue);
            _slider.value = sliderValue;
            //Text
            _sliderValue.text = sliderValue.ToString();
            
            //Mute
            if (Entity.HasMutedSoundVolume())
                _muteToggle.SetState(EnableState.Off);
            else
                _muteToggle.SetState(EnableState.On);
        }

        public void CancelSettings()
        {
            SettingsSaveData data = Entity.GetSavedSettings().Value;
            SetSettings(data.SoundVolume, data.IsSoundMuted);
        }

        public void ResetToDefaultSettings() =>
            SetSettings(_config.SoundVolume, _config.IsMutedSound);

        private void SetSettings(float volume, bool isMuted)
        {
            float sliderValue = GetSliderValue(volume);
            float nextVolume = sliderValue.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeSoundsVolume(nextVolume);
            Entity.ReplaceSoundVolume(nextVolume);

            if (isMuted && Entity.HasMutedSoundVolume() == false)
                Entity.AddMutedSoundVolume();
            
            if (isMuted == false && Entity.HasMutedSoundVolume())
                Entity.DelMutedSoundVolume();
            
            UpdateView();
        }

        private void ChangeVolume(float value)
        {
            _sliderValue.text = value.ToString();
            float nextVolume = value.Normalize(_slider.minValue, _slider.maxValue);
            _soundService.ChangeSoundsVolume(nextVolume);
            Entity.ReplaceSoundVolume(nextVolume);
            AddChangeSettings();
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            AddChangeSettings();
            
            if (state == EnableState.On)
            {
                _soundService.UnmuteSounds();
                Entity.DelMutedSoundVolume();
                return;
            }
            
            _soundService.MuteSounds();
            Entity.AddMutedSoundVolume();
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