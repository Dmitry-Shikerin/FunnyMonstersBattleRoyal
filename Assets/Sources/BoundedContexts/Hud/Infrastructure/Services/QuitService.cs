using Sources.BoundedContexts.Hud.Infrastructure.Services.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Infrastructure.Services
{
    public class QuitService : IQuitService
    {
        public void QuitApplication()
        {
            Application.Quit();
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}