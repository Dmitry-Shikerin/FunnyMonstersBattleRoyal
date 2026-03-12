using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Editor.Infrastructure.Implementation.Services;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    [Serializable]
    public class AudioData
    {
        //Constants
        private const float DefaultWeight = 1f;
        private const float MaxWeight = 1f;
        private const float MinWeight = 0f;
        
        //Fields
        [HideInInspector]
        [SerializeField] public SoundGroupData Parent;
        [PropertyOrder(1)]
        [HideLabel]
#if UNITY_EDITOR 
        [OnValueChanged(nameof(OnAudioClipChanged))]
#endif
        [SerializeField] public AudioClip AudioClip;
        [Range(MinWeight, MaxWeight)]
        [PropertyOrder(4)]
        public float Weight = DefaultWeight;
        [HideInInspector]
        [SerializeField] public bool IsPlaying;
        
        public AudioData() =>
            Reset();
        
        public AudioData(AudioClip audioClip, SoundGroupData parent)
        {
            Reset();
            AudioClip = audioClip;
            Parent = parent;
        }
        
        public AudioData(AudioClip audioClip, float weight)
        {
            Reset();
            AudioClip = audioClip;
            Weight = weight;
        }
        
        public void Reset()
        {
            AudioClip = null;
            Weight = 1;
        }
        
        //Editor
#if UNITY_EDITOR  
        [PropertyOrder(3)]
        [HorizontalGroup(30)]
        [HideLabel]
        [SerializeField] private float _curentValue;        
        [PropertyOrder(3)]
        [HorizontalGroup]
        [ProgressBar(0, 1)]
        [HideLabel]
        [OnValueChanged(nameof(OnSliderValueChanged))]
        [SerializeField] private float _value;     
        [PropertyOrder(3)]
        [HorizontalGroup(30)]
        [HideLabel]
        [SerializeField] private float _maxValue;

        [PropertyOrder(2)]
        [HorizontalGroup(30)]
        [HideLabel]
        [Button(SdfIconType.Play)]
        public void Play()
        {
            IsPlaying = true;
            PreviewSoundPlayerService.Play(AudioClip, Parent, SetSliderValue, Stop);
        }
        
        [PropertyOrder(2)]
        [HorizontalGroup(30)]
        [HideLabel]
        [Button(SdfIconType.Stop)]
        public void Stop()
        {
            IsPlaying = false;
            SetSliderValue(0);
            PreviewSoundPlayerService.Stop();
        }

        private void OnSliderValueChanged(float value)
        {
            Debug.Log($"SliderValueChanged: {value}");
        }

        public void SetSliderValue(float value)
        {
            _curentValue = value;
            _value = value;
        }

        public void SetSliderValue(Vector2 minMaxValue, float value)
        {
            _curentValue = value;
            _value = value;
            _maxValue = minMaxValue.y;
        }

        private void OnAudioClipChanged(AudioClip audioClip)
        {
            float maxValue = 0;
            
            if (audioClip != null)
                maxValue = audioClip.length;
            
            SetSliderValue(new Vector2(0, maxValue), 0);
        }
#endif
    }
}