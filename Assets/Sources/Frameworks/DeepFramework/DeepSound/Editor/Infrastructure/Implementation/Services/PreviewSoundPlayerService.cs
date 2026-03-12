using System;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor;
using Sources.Frameworks.DeepFramework.DeepUtils.Editor.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;
using UnityEditor;

namespace Sources.Frameworks.DeepFramework.DeepSound.Editor.Infrastructure.Implementation.Services
{
    public static class PreviewSoundPlayerService
    {
        private static DeepSoundController _soundController;
        private static AudioSource _audioSource;

        private static Action<float> SliderValueChange;
        private static Action Stopped;

        static PreviewSoundPlayerService()
        {
            _soundController = Object.FindFirstObjectByType<DeepSoundController>() ??
                               new GameObject(nameof(DeepSoundController)).AddComponent<DeepSoundController>();
            _audioSource = _soundController.gameObject.GetComponent<AudioSource>() 
                           ?? _soundController.gameObject.AddComponent<AudioSource>();
        }

        public static void Initialize()
        {
            EditorUpdateService.Register(UpdateSlider);
        }

        public static void Destroy()
        {
            EditorUpdateService.UnRegister(UpdateSlider);
        }

        private static void UpdateSlider(float deltaTime)
        {
            SliderValueChange?.Invoke(_audioSource.time);

            if (_audioSource.clip == null)
                return;
            
            if (_audioSource.time + 0.1f >= _audioSource.clip.length)
                Stop();
        }

        public static AudioSource Play(SoundGroupData soundGroupData, Action<float> sliderValueChange, Action stopAction = null)
        {
            soundGroupData.ChangeLastPlayedAudioData();
            _audioSource.clip = soundGroupData.LastPlayedAudioData.AudioClip;

            _audioSource.ignoreListenerPause = soundGroupData.IgnoreListenerPause;
            _audioSource.volume = soundGroupData.RandomVolume;
            _audioSource.pitch = soundGroupData.RandomPitch;
            _audioSource.loop = soundGroupData.Loop;
            _audioSource.spatialBlend = soundGroupData.SpatialBlend;
            Camera main = Camera.main;
            _audioSource.transform.position = main == null ? Vector3.zero : main.transform.position;
            
            Stopped?.Invoke();
            Stopped = stopAction;
            SliderValueChange?.Invoke(0);
            SliderValueChange = (value) => SliderValueChanged(sliderValueChange);
            
            _audioSource.Play();

            return _audioSource;
        }

        private static void SliderValueChanged(Action<float> sliderValueChange)
        {
            var min = 0f;
            var max = 10f;
            var time = _audioSource.time;
            var length = _audioSource.clip.length;

            var value = time.FloatToPercent(min, length).FloatPercentToUnitPercent();
            
            sliderValueChange?.Invoke(value);
        }

        public static AudioSource Play(AudioClip audioClip, 
            SoundGroupData soundGroupData,
            Action<float> sliderValueChange, 
            Action stopAction = null,
            AudioMixerGroup outputAudioMixerGroup = null)
        {
            if (audioClip != null)
            {
                _audioSource.clip = audioClip;
            }
            else
            {
                soundGroupData.ChangeLastPlayedAudioData();

                if (soundGroupData.LastPlayedAudioData == null)
                    return _audioSource;

                _audioSource.clip = soundGroupData.LastPlayedAudioData.AudioClip;
            }

            _audioSource.ignoreListenerPause = soundGroupData.IgnoreListenerPause;
            _audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            _audioSource.volume = soundGroupData.RandomVolume;
            _audioSource.pitch = soundGroupData.RandomPitch;
            _audioSource.loop = soundGroupData.Loop;
            _audioSource.spatialBlend = soundGroupData.SpatialBlend;
            Camera main = Camera.main;
            _audioSource.transform.position = main == null ? Vector3.zero : main.transform.position;
            
            Stopped?.Invoke();
            Stopped = stopAction;
            SliderValueChange?.Invoke(0);
            SliderValueChange = (value) => SliderValueChanged(sliderValueChange);
            
            _audioSource.Play();

            return _audioSource;
        }

        public static void Stop()
        {
            _audioSource.Stop();
            SliderValueChange?.Invoke(0);
            SliderValueChange = null;
        }
    }
}