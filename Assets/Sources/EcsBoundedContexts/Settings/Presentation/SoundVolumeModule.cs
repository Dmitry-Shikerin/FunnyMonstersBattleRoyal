using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Buttons;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Settings.Presentation
{
    public class SoundVolumeModule : EntityModule
    {
        [Required] [SerializeField] private TMP_Text _sliderValue;
        [field: Required] [field: SerializeField] public UiToggle MuteToggle {get; private set; }
        [field: Required] [field: SerializeField] public Slider Slider { get; set; }
        
        private ISoundService _soundService;

        private void OnEnable()
        {
            Slider.onValueChanged.AddListener(ChangeVolume);
            MuteToggle.StateChanged += OnMuteToggleStateChanged;
        }

        protected override void OnAfterDisable()
        {
            Slider.onValueChanged.RemoveListener(ChangeVolume);
            MuteToggle.StateChanged -= OnMuteToggleStateChanged;
        }

        private void ChangeVolume(float value)
        {
            float nextVolume = value.Normalize(Slider.minValue, Slider.maxValue);
            _sliderValue.text = value.ToString();
            _soundService.ChangeSoundsVolume(nextVolume);
            Entity.ReplaceSoundVolume(nextVolume);
        }

        private void OnMuteToggleStateChanged(EnableState state)
        {
            if (state == EnableState.On)
            {
                _soundService.UnmuteSounds();
                Entity.DelMutedSoundVolume();
                return;
            }
            
            _soundService.MuteSounds();
            Entity.AddMutedSoundVolume();
        }

        [Inject]
        private void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }
    }
}