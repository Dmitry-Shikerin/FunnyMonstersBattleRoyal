using Sources.EcsBoundedContexts.Settings.Domain.Enums;
using Sources.Frameworks.GameServices.Loads.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Domain.Data
{
    public struct SettingsSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public FullScreenMode FullScreenMode { get; set; }
        public int Framerate { get; set; }
        public ResolutionSaveData Resolution { get; set; }
        public bool IsVSync { get; set; }
        public GraphicsQualities GraphicsQuality { get; set; }
        public float MusicVolume { get; set; }
        public bool IsMusicMuted { get; set; }
        public float SoundVolume { get; set; }
        public bool IsSoundMuted { get; set; }
    }
}