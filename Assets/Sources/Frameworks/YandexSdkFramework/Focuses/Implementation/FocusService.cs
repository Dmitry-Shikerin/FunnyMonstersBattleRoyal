using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.YandexSdkFramework.Focuses.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;
using YG;

namespace Sources.Frameworks.YandexSdkFramework.Focuses.Implementation
{
    public class FocusService : IFocusService
    {
        private readonly IPauseService _pauseService;

        public FocusService(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void Initialize()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            OnInBackgroundChange(YG2.isFocusWindowGame);
            OnInBackgroundChange(Application.isFocused);
            
            Application.focusChanged += OnInBackgroundChange;
            YG2.onFocusWindowGame += OnInBackgroundChange;
        }

        public void Destroy()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;
            
            Application.focusChanged -= OnInBackgroundChange;
            YG2.onFocusWindowGame -= OnInBackgroundChange;
        }

        private void OnInBackgroundChange(bool isFocus)
        {
            if (isFocus == false)
            {
                _pauseService.PauseGame();
                _pauseService.PauseSound();

                return;
            }

            if (_pauseService.IsPaused)
                _pauseService.ContinueGame();

            if (_pauseService.IsSoundPaused)
                _pauseService.ContinueSound();
        }
    }
}