using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Infrastructure;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.Frameworks.GameServices.DeepWrappers.Sounds
{
    public class SoundService : ISoundService
    {
        private readonly ISdkService _sdkService;
        private readonly IPauseService _pauseService;
        private readonly IEntityRepository _entityRepository;
        private readonly List<SoundDatabaseName> _soundDatabaseNames;
        private readonly List<SoundDatabaseName> _musicDatabaseNames;

        private string _musicSoundName;

        public SoundService(
            IPauseService pauseService,
            IEntityRepository entityRepository)
        {
            _pauseService = pauseService;
            _entityRepository = entityRepository;
            _soundDatabaseNames = new List<SoundDatabaseName>()
            {
                SoundDatabaseName.Sounds,
                SoundDatabaseName.UiSounds,
            };            
            _musicDatabaseNames = new List<SoundDatabaseName>()
            {
                SoundDatabaseName.Music,
            };
        }

        public void Initialize()
        {
            _pauseService.PauseChanged += OnPauseGame;
        }

        public void Destroy()
        {
            _pauseService.PauseChanged -= OnPauseGame;
        }

        public void MuteSounds()
        {
            foreach (SoundDatabaseName databaseName in _soundDatabaseNames)
                DeepSoundManager.Mute(databaseName);
        }

        public void UnmuteSounds()
        {
            foreach (SoundDatabaseName databaseName in _soundDatabaseNames)
                DeepSoundManager.Unmute(databaseName);
        }

        public void MuteMusic()
        {
            foreach (SoundDatabaseName databaseName in _musicDatabaseNames)
                DeepSoundManager.Mute(databaseName);
        }

        public void UnmuteMusic()
        {
            foreach (SoundDatabaseName databaseName in _musicDatabaseNames)
                DeepSoundManager.Unmute(databaseName);
        }

        public DeepSoundController Play(SoundDatabaseName databaseName, SoundName soundName)
        {
            float volume = GetVolume(databaseName);
            return DeepSoundManager
                .Play(databaseName, soundName)
                .SetVolume(volume)
                .SetMute(GetMute(databaseName));
        }

        public void Stop(SoundName soundName) =>
            DeepSoundManager.Stop(soundName);

        private void OnPauseGame(bool isPaused)
        {
            if (isPaused)
            {
                DeepSoundManager.Pause(SoundDatabaseName.Sounds);
                return;
            }
            
            DeepSoundManager.UnPause(SoundDatabaseName.Sounds);
        }

        public void PauseSounds() =>
            DeepSoundManager.Pause(SoundDatabaseName.Sounds);

        public void UnpauseSounds() =>
            DeepSoundManager.UnPause(SoundDatabaseName.Sounds);

        public void PauseMusic() =>
            DeepSoundManager.Pause(SoundDatabaseName.Music);

        public void UnpauseMusic() =>
            DeepSoundManager.UnPause(SoundDatabaseName.Music);

        public void ChangeSoundsVolume(float volume)
        {
            foreach (SoundDatabaseName databaseName in _soundDatabaseNames)
                DeepSoundManager.ChangeVolume(databaseName, volume);
        }

        public void ChangeMusicVolume(float volume)
        {
            foreach (SoundDatabaseName databaseName in _musicDatabaseNames)
                DeepSoundManager.ChangeVolume(databaseName, volume);
        }

        private bool GetMute(SoundDatabaseName databaseName)
        {
            ProtoEntity entity = _entityRepository.GetByName(IdsConst.Settings);
            
            return databaseName switch
            {
                SoundDatabaseName.Music => entity.HasMutedMusicVolume(),
                SoundDatabaseName.Sounds => entity.HasMutedSoundVolume(),
                SoundDatabaseName.UiSounds => entity.HasMutedSoundVolume(),
                _ => throw new ArgumentOutOfRangeException(nameof(databaseName), databaseName, null)
            };
        }

        private float GetVolume(SoundDatabaseName databaseName)
        {
            ProtoEntity entity = _entityRepository.GetByName(IdsConst.Settings);
            
            return databaseName switch
            {
                SoundDatabaseName.Music => entity.GetMusicVolume().Value,
                SoundDatabaseName.Sounds => entity.GetSoundVolume().Value,
                SoundDatabaseName.UiSounds => entity.GetSoundVolume().Value,
                _ => throw new ArgumentOutOfRangeException(nameof(databaseName), databaseName, null)
            };
        }
    }
}