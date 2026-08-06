using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Domain.Data
{
    [CreateAssetMenu(fileName = nameof(SettingsConfig), menuName = "Configs/" + nameof(SettingsConfig), order = 51)]
    public class SettingsConfig : Config
    {
        [field: Header("Volume")]
        [field: SerializeField] public float MusicVolume { get; private set; } = 0.75f;
        [field: SerializeField] public bool IsMutedMusic { get; private set; }
        [field: SerializeField] public float SoundVolume { get; private set; } = 0.75f;
        [field: SerializeField] public bool IsMutedSound { get; private set; }
    }
}