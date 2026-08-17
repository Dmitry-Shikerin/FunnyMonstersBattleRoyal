using Sources.BoundedContexts.Settings.Domain.Enums;
using Sources.Frameworks.GameServices.Loads.Domain;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Domain.Data
{
    public class SettingsSaveData : IEntitySaveData
    {
        //Graphics
        public string Id { get; set; }
        public FullScreenMode FullScreenMode { get; set; }
        public int Framerate { get; set; }
        public ResolutionSaveData Resolution { get; set; }
        public bool IsVSync { get; set; }
        public GraphicsQualities GraphicsQuality { get; set; }
        //Volume
        public float MusicVolume { get; set; }
        public bool IsMusicMuted { get; set; }
        public float SoundVolume { get; set; }
        public bool IsSoundMuted { get; set; }
        //Mouse
        public float MouseSensitivity { get; set; }
    }
}