namespace Sources.EcsBoundedContexts.Settings.Presentation.Interfaces
{
    public interface ISettingsModule
    {
        void UpdateView();
        void CancelSettings();
        void ResetToDefaultSettings();
    }
}