using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Presentation;
using Sources.Frameworks.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;

namespace Sources.Frameworks.GameServices.DeepWrappers.Sounds
{
    public interface ISoundService : IInitialize, IDestroy
    {
        void ChangeSoundsVolume(float volume);
        void ChangeMusicVolume(float volume);
        void PauseSounds();
        void UnpauseSounds();
        void MuteSounds();
        void UnmuteSounds();
        void PauseMusic();
        void UnpauseMusic();
        void MuteMusic();
        void UnmuteMusic();
        DeepSoundController Play(SoundDatabaseName databaseName, SoundName soundName);
        void Stop(SoundName soundName);
    }
}