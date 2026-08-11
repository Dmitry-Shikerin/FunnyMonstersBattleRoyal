using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Infrastructure.Services.Interfaces
{
    public interface IScreenService
    {
        int MaxFramerate { get; }
        
        void SetResolution(int width, int height, RefreshRate refreshRate);
        void SetFullScreen(FullScreenMode mode);
        void SetFramerate(int frameRate);
    }
}