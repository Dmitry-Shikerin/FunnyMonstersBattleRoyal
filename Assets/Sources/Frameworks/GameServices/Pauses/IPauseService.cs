using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sources.Frameworks.GameServices.Pauses
{
    public interface IPauseService
    {
        event Action<bool> PauseChanged;
        event Action<bool> PauseSoundChanged;
        
        public int PauseListenersCount { get; }
        public int SoundPauseListenersCount { get; }
        public bool IsPaused { get; }
        public bool IsSoundPaused { get; }
        
        void ContinueGame();
        void ContinueSound();
        void PauseSound();
        void PauseGame();
    }
}