using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Data
{
    [Serializable]
    public class SoundGroupData
    {
        //Const
        private const bool DefaultIgnoreListenerPause = true;
        private const bool DefaultLoop = false;
        private const bool DefaultResetSequenceAfterInactiveTime = false;
        private const float DefaultPitch = 0;
        private const float DefaultSequenceResetTime = 5f;
        private const float DefaultSpatialBlend = 0;
        private const float DefaultVolume = 0;
        private const float MaxPitch = 24;
        private const float MaxSpatialBlend = 1;
        private const float MaxVolume = 0;
        private const float MinPitch = -24;
        private const float MinSpatialBlend = 0;
        private const float MinVolume = -80;
        private const SoundPlayMode DefaultPlayMode = SoundPlayMode.Random;
        
        //Fields
        [SerializeField] public SoundDatabaseName ParentDatabaseName;
        [SerializeField] public SoundName SoundName;
        [SerializeField] public bool IgnoreListenerPause;
        [EnumToggleButtons] 
        [SerializeField] private SoundGroupDataEnum _soundGroupDataEnum;
        [ShowIf("IsVolume")] 
        [MinMaxSlider(MinVolume, MaxVolume)] 
        [SerializeField] public Vector2 Volume;
        [ShowIf("IsPitch")] 
        [MinMaxSlider(MinPitch, MaxPitch)] 
        [SerializeField] public Vector2 Pitch;
        [ShowIf("IsSpatialBlend")]
        [Range(MinSpatialBlend, MaxSpatialBlend)]
        [SerializeField] public float SpatialBlend;
        [SerializeField] public bool Loop;
        [EnumToggleButtons]
        [SerializeField] public SoundPlayMode Mode;
        [SerializeField] public bool ResetSequenceAfterInactiveTime;
        [ShowIf("ResetSequenceAfterInactiveTime")]
        [SerializeField] public float SequenceResetTime;
        [HideInInspector]
        [SerializeField] public bool IsPlaying;
        private readonly List<AudioData> _playedSounds = new List<AudioData>();
        private int _lastPlayedSoundsIndex = -1;
        private float _lastPlayedSoundTime;
        [HideInInspector]
        [SerializeField] public AudioData LastPlayedAudioData;
        [SerializeField] public List<AudioData> Sounds = new ();
        
        private bool IsVolume => _soundGroupDataEnum == SoundGroupDataEnum.Volume;
        private bool IsPitch => _soundGroupDataEnum == SoundGroupDataEnum.Pitch;
        private bool IsSpatialBlend => _soundGroupDataEnum == SoundGroupDataEnum.SpatialBlend;

        public float RandomPitch => DeepSoundUtils.SemitonesToPitch(Random.Range(Pitch.x, Pitch.y));
        public float RandomVolume => DeepSoundUtils.DecibelToLinear(Random.Range(Volume.x, Volume.y));

        public bool HasMissingAudioClips
        {
            get
            {
                if (SoundName == SoundName.Default)
                    return false;
                
                if (Sounds == null || Sounds.Count == 0)
                    return true;

                foreach (AudioData audioData in Sounds)
                {
                    if (audioData == null || audioData.AudioClip == null)
                        return true;
                }

                return false;
            }
        }

        public bool HasSound
        {
            get
            {
                if (SoundName == SoundName.Default)
                    return false;
                
                if (Sounds == null || Sounds.Count == 0)
                    return false;
                
                return HasMissingAudioClips == false;
            }
        }
        
        private void Reset()
        {
            SoundName = SoundName.Default;
            IgnoreListenerPause = DefaultIgnoreListenerPause;
            Loop = DefaultLoop;
            Volume = new Vector2()
            {
                x = DefaultVolume, 
                y = DefaultVolume
            };
            Pitch = new Vector2()
            {
                x = DefaultPitch, 
                y = DefaultPitch
            };
            SpatialBlend = DefaultSpatialBlend;
            Mode = DefaultPlayMode;
            ResetSequenceAfterInactiveTime = DefaultResetSequenceAfterInactiveTime;
            SequenceResetTime = DefaultSequenceResetTime;
        }
        
        public bool Contains(AudioClip audioClip)
        {
            if (audioClip == null)
                return false;
            
            return Sounds.Any(data => data.AudioClip == audioClip);
        }
        
        public AudioData AddAudioData(AudioClip audioClip = null)
        {
            AudioData audioData = new AudioData(audioClip, this);
            Sounds.Add(audioData);

            return audioData;
        }

        public void Remove(AudioData audioData) =>
            Sounds.Remove(audioData);

        public void ChangeLastPlayedAudioData()
        {
            LastPlayedAudioData = GetAudioData(Mode);
        }
        
        private AudioData GetAudioData(SoundPlayMode playMode)
        {
            return playMode switch
            {
                SoundPlayMode.Random => GetRandom(),
                SoundPlayMode.Sequence => GetSequence(),
                _ => throw new InvalidOperationException("Invalid play mode"),
            };
        }

        private AudioData GetRandom()
        {
            if (_playedSounds.Count == Sounds.Count)
                _playedSounds.Clear();

            AudioData foundClip = null; //look for a sound that has not been played
                    
            while (foundClip == null)   //until such a sound is found continue the search
            {
                int randomIndex = Random.Range(0, Sounds.Count);
                foundClip = Sounds[randomIndex];        //get a random sound
                        
                if (_playedSounds.Contains(foundClip)) //check that it has not been played
                {
                    foundClip = null; //it has been played -> discard it
                }
                else
                {
                    _playedSounds.Add(foundClip); //it has not been played -> add it to the _playedSounds list and continue
                    _lastPlayedSoundsIndex = randomIndex;
                }
            }

            return foundClip; //return the sound that will get played
        }

        private AudioData GetSequence()
        {
            if (_playedSounds.Count == Sounds.Count) //if all the sounds in the sounds list were played
                _lastPlayedSoundsIndex = -1;         //-> reset the sequence index

            if (ResetSequenceAfterInactiveTime &&                                      //if resetSequenceAfterInactiveTime 
                Time.realtimeSinceStartup - _lastPlayedSoundTime > SequenceResetTime) //and enough time has passed since the last sound in the sequence has been played //Time.unscaledTime
                _lastPlayedSoundsIndex = -1;                                          //-> reset the sequence index

            if (_lastPlayedSoundsIndex == -1) //if the last played index is in the reset state (-1)
                _playedSounds.Clear();        //-> reset the played sounds list

            _lastPlayedSoundsIndex = _lastPlayedSoundsIndex == -1 || _lastPlayedSoundsIndex >= Sounds.Count - 1
                ? 0 //if the index has been reset (-1)
                : _lastPlayedSoundsIndex + 1; //-> set the last played index as the first entry in the sounds list
                                              //-> otherwise set the last played index as the next entry in the sequence

            _playedSounds.Add(Sounds[_lastPlayedSoundsIndex]); //add the played sound to the playedSounds list
            _lastPlayedSoundTime = Time.realtimeSinceStartup;   //save the last played sound time
                    
            return Sounds[_lastPlayedSoundsIndex];              //return the sound that will get played
        }
    }
}