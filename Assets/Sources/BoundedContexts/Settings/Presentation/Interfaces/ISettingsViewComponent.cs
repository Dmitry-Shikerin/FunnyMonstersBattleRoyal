namespace Sources.BoundedContexts.Settings.Presentation.Interfaces
{
    public interface ISettingsViewComponent
    {
        void Initialize();
        void UpdateView();
        void CancelSettings();
        void ResetToDefaultSettings();
        void ApplySettings();
    }
}