using Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure.Services
{
    public class ScreenService : IScreenService
    {
        public int MaxFramerate => Screen.currentResolution.refreshRate;

        public void SetResolution(int width, int height, RefreshRate refreshRate)
        {
            Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen, refreshRate);
        }

        public void SetFullScreen(FullScreenMode mode)
        {
            Screen.fullScreenMode = mode;
        }

        public void SetFramerate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
    }
}