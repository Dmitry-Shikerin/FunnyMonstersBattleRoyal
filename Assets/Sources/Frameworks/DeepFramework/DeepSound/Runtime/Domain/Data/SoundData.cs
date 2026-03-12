using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    [Serializable]
    public class SoundData
    {
        public SoundSource SoundSource;
        [ValueDropdown(nameof(GetDataBases))]
        public SoundDatabaseName DatabaseName;
        [ValueDropdown(nameof(GetSoundNames))]
        public SoundName SoundName;
        public AudioClip AudioClip;
        public AudioMixerGroup OutputAudioMixerGroup;
        [Range(0, 1)]
        public float Volume;
        public bool IsMute;
        
        public SoundData()
        {
            Reset();
        }
        
        public SoundGroupData GetAudioData() =>
            DeepSoundSettings.Database.GetAudioData(DatabaseName, SoundName);
        
        public void Reset()
        {
            SoundSource = SoundSource.Default;
            DatabaseName = SoundDatabaseName.Default;
            SoundName = SoundName.Default;
            AudioClip = null;
        }

        private IEnumerable<SoundDatabaseName> GetDataBases() =>
            DeepSoundSettings.Database.GetDatabaseNames();
        
        private IEnumerable<SoundName> GetSoundNames() =>
            DeepSoundSettings.Database.GetSoundDatabase(DatabaseName).GetSoundNames();
    }
}