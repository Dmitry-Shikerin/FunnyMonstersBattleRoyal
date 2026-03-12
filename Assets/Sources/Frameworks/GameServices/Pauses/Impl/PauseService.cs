using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Frameworks.GameServices.Pauses.Domain.Constants;
using UnityEngine;

namespace Sources.Frameworks.GameServices.Pauses.Impl
{
    public class PauseService : IPauseService
    {
        public event Action<bool> PauseChanged;
        public event Action<bool> PauseSoundChanged;

        public int PauseListenersCount { get; private set; }
        public int SoundPauseListenersCount { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsSoundPaused { get; private set; }
        
        public void ContinueSound()
        {
            SoundPauseListenersCount--;

            if (SoundPauseListenersCount > 0)
                return;

            if (SoundPauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(SoundPauseListenersCount));

            IsSoundPaused = false;
            PauseSoundChanged?.Invoke(IsSoundPaused);
        }

        public void ContinueGame()
        {
            PauseListenersCount--;

            if (PauseListenersCount > 0)
                return;

            if (PauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(PauseListenersCount));

            IsPaused = false;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = TimeScaleConst.Max;
        }

        public void PauseSound()
        {
            SoundPauseListenersCount++;
            
            if (SoundPauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(SoundPauseListenersCount));

            IsSoundPaused = true;
            PauseSoundChanged?.Invoke(IsSoundPaused);
        }

        public void PauseGame()
        {
            PauseListenersCount++;

            if (PauseListenersCount < 0)
                throw new IndexOutOfRangeException(nameof(PauseListenersCount));

            IsPaused = true;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = TimeScaleConst.Min;
        }
    }
}