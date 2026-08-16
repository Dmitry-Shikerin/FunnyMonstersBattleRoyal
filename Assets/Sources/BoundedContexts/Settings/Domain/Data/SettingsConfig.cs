using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.BoundedContexts.Settings.Domain.Data
{
    [CreateAssetMenu(fileName = nameof(SettingsConfig), menuName = "Configs/" + nameof(SettingsConfig), order = 51)]
    public class SettingsConfig : Config
    {
        [field: Header("GraphicsQuality")]
        [field: SerializeField] public GraphicsQualities GraphicsQuality { get; private set; }
        
        [field: Header("VSync")]
        [field: SerializeField] public bool IsVSync { get; private set; } = false;

        [field: Header("FrameRate")]
        [field: ValueDropdown(nameof(GetFrameRates))]
        [field: SerializeField] public int Framerate { get; private set; } = -1;

        [field: Header("FullScreen")]
        [field: SerializeField] public FullScreenMode FullScreenMode { get; private set; } = FullScreenMode.ExclusiveFullScreen;

        [field: Header("Volume")]
        [field: SerializeField] public float MusicVolume { get; private set; } = 0.75f;
        [field: SerializeField] public bool IsMutedMusic { get; private set; }
        [field: SerializeField] public float SoundVolume { get; private set; } = 0.75f;
        [field: SerializeField] public bool IsMutedSound { get; private set; }

        private List<int> GetFrameRates()
        {
            return new List<int>()
            {
                30,
                60,
                120,
                144,
                240,
                -1,
            };
        }
    }
}