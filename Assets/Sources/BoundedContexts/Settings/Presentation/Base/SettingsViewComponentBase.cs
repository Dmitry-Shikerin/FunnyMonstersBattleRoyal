using UnityEngine;

namespace Sources.BoundedContexts.Settings.Presentation.Base
{
    public abstract class SettingsViewComponentBase : MonoBehaviour
    {
        protected SettingsView SettingsView { get; private set; }
        protected bool IsInitialized { get; private set; }
        
        public void Initialize(SettingsView settingsView)
        {
            SettingsView = settingsView;
            IsInitialized = true;
        }

        public virtual void UpdateView()
        {
        }

        public virtual void CancelSettings()
        {
        }

        public virtual void ResetToDefaultSettings()
        {
        }

        public virtual void ApplySettings()
        {
        }
    }
}