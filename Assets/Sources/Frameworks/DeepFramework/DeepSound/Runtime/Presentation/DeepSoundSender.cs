using System;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation
{
    public class DeepSoundSender : MonoBehaviour
    {
        //Const
        private const string Label = "<size=18><b><color=#C71585><i>Deep Sound Sender</i></color></b></size>";
        private const int Space = 10;
        
        //Fields
        [DisplayAsString(false)] 
        [HideLabel]
        [SerializeField] private string _label = Label;
        
        [field: SerializeField] public SoundData SoundData { get; private set; }

        private void Awake() =>
            DeepSoundManager.Register(this);

        private void OnDestroy() =>
            DeepSoundManager.Unregister(this);

        public void Play()
        {
            Action action = SoundData.SoundSource switch
            {
                SoundSource.Default => () =>
                {
                    if (SoundData.SoundName == SoundName.Default)
                        return;
                     
                    DeepSoundManager
                        .Play(SoundData.DatabaseName, SoundData.SoundName)
                        .SetVolume(SoundData.Volume)
                        .SetMute(SoundData.IsMute);
                },
                SoundSource.AudioClip => () =>
                {
                    if (SoundData.AudioClip == null)
                        return;
                     
                    DeepSoundManager
                        .Play(SoundData.AudioClip)
                        .SetOutputAudioMixerGroup(SoundData.OutputAudioMixerGroup)
                        .SetVolume(SoundData.Volume);
                },
                SoundSource.MasterAudio =>  null, // no action, or throw an exception, or return a default value
                _ => null
            };
            
            action?.Invoke();
        }
    }
}