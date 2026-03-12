using System;
using Sources.Frameworks.DeepFramework.DeepCores.Core;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation
{
    [DefaultExecutionOrder(-7)]
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class DeepSoundController : MonoBehaviour
    {
        [field: SerializeField] public SoundName Name { get; set; }
        [field: SerializeField] public SoundDatabaseName DataBaseName { get; set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public bool InUse { get; private set; }
        [field: SerializeField] public float PlayProgress { get; private set; }
        [field: SerializeField] public float LastPlayedTime { get; private set; }
        [field: SerializeField] public bool IsPaused { get; private set; }
        public bool IsPlaying => AudioSource.isPlaying;
        public bool IsMuted => AudioSource.mute;
        public float IdleDuration => Time.realtimeSinceStartup - LastPlayedTime;
        
        private Transform _transform;
        private Transform _followTarget;
        private bool _autoPaused;
        private float _savedClipTime;
        private DeepSoundControllersPool _pool;
        private DeepSoundManager _manager;
        private DeepCore _core;

        public void Construct(DeepSoundManager manager, DeepSoundControllersPool pool)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
        }
        
        private void Reset() =>
            ResetController();

        private void Awake()
        {
            _core = DeepCore.Instance;
            _transform = transform;
            AudioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            ResetController();
        }

        private void OnEnable()
        {
            _core.RegisterUpdate(Run);
        }

        private void OnDisable()
        {
            _core.UnregisterUpdate(Run);
        }

        private void Run(float deltaTime)
        {
            if (AudioSource.clip == null)
            {
                Stop();
                return;
            }
            
            UpdateLastPlayedTime();
            UpdatePlayProgress();
            FollowTarget();
        }

        public void Destroy()
        {
            Stop();
            Destroy(gameObject);
        }

        public DeepSoundController Play()
        {
            InUse = true;
            IsPaused = false;
            AudioSource.Play();
            return this;
        }

        public DeepSoundController SetFollowTarget(Transform followTarget)
        {
            _followTarget = followTarget;
            return this;
        }

        public DeepSoundController SetOutputAudioMixerGroup(AudioMixerGroup outputAudioMixerGroup)
        {
            if (outputAudioMixerGroup == null)
                return this;
            
            AudioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            return this;
        }

        public void Stop()
        {
            Unpause();
            Unmute();
            AudioSource.Stop();
            ResetController();
            UpdateLastPlayedTime();
            _pool?.ReturnToPool(this);
        }
        
        public DeepSoundController SetName(SoundDatabaseName databaseName, SoundName soundName, string audioClipName)
        {
            DataBaseName = databaseName;
            Name = soundName;
            gameObject.name = $"[{soundName}]-({audioClipName})";
            return this;
        }
        
        public DeepSoundController SetSpatialBlend(float spatialBlend)
        {
            AudioSource.spatialBlend = spatialBlend;
            return this;
        }
        
        public DeepSoundController SetPitch(float pitch)
        {
            AudioSource.pitch = pitch;
            return this;
        }
        
        public DeepSoundController SetLoop(bool loop)
        {
            AudioSource.loop = loop;
            return this;
        }

        public DeepSoundController SetVolume(float volume)
        {
            AudioSource.volume = Math.Clamp(volume, 0, 1);
            return this;
        }

        public DeepSoundController SetAudioClip(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
            return this;
        }

        public DeepSoundController SetPosition(Vector3 position)
        {
            _transform.position = position;
            return this;
        }

        public DeepSoundController Mute()
        {
            AudioSource.mute = true;
            return this;
        }

        public DeepSoundController Unmute()
        {
            AudioSource.mute = false;
            return this;
        }

        public DeepSoundController SetMute(bool isMute)
        {
            if (isMute)
            {
                Mute();
                return this;
            }
            
            Unmute();
            return this;
        }

        public DeepSoundController Pause()
        {
            _savedClipTime = AudioSource.time;
            AudioSource.Pause();
            IsPaused = true;
            return this;
        }

        public DeepSoundController Unpause()
        {
            if (_savedClipTime < 0 || (AudioSource.clip != null && _savedClipTime >= AudioSource.clip.length))
                _savedClipTime = 0;
    
            AudioSource.time = _savedClipTime;
            IsPaused = false;
            AudioSource.UnPause();
            return this; 
        }

        private void FollowTarget()
        {
            if (_followTarget == null) 
                return;
            
            _transform.position = _followTarget.position;
        }

        private void ResetController()
        {
            InUse = false;
            IsPaused = false;
            _followTarget = null;
        }

        private void UpdateLastPlayedTime()
        {
            if ((IsMuted || IsPaused || AudioSource.isPlaying) == false)
                return;
            
            LastPlayedTime = Time.realtimeSinceStartup;
        }

        private void UpdatePlayProgress()
        {
            if (AudioSource == null)
                return;
            
            if (AudioSource.clip == null)
                return;
            
            if (IsPaused)
                return;
            
            PlayProgress = Mathf.Clamp01(AudioSource.time / AudioSource.clip.length);

            if (PlayProgress >= 1f && AudioSource.loop)
            {
                PlayProgress = 0;
                AudioSource.time = 0;
                return;
            }

            if (PlayProgress >= 1f) //check if the sound finished playing
            {
                Stop();
                PlayProgress = 0;
            }
        }
    }
}