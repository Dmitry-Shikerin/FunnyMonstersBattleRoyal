using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Data
{
    public struct SettingsSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public float MusicVolume { get; set; }
        public bool IsMusicMuted { get; set; }
        public float SoundVolume { get; set; }
        public bool IsSoundMuted { get; set; }
        public int Framerate { get; set; }
    }
}