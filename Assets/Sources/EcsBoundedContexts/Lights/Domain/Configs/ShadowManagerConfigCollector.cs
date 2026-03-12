using Sources.EcsBoundedContexts.Lights.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(ShadowManagerConfigCollector), menuName = "Configs/" + nameof(ShadowManagerConfigCollector), order = 51)]
    public class ShadowManagerConfigCollector : ConfigCollector<ShadowManagerConfig>
    {
        [Tooltip("The fading time in seconds of the shadow strenght when the shadow is enabled.")]
        [field: SerializeField] public float m_FadeInTime { get; private set; } = 1f;
        [Tooltip("The fading time in seconds of the shadow strenght when the shadow is disabled.")]
        [field: SerializeField] public float m_FadeOutTime { get; private set; } = 1f;

        [Tooltip("The fading time in seconds of the light ranges when the shadow is enabled.")]
        [field: SerializeField] public float m_RangeFadeInTime {get; private set; } = 2f;
        [Tooltip("The fading time in seconds of the light ranges when the shadow is disabled.")]
        [field: SerializeField] public float m_RangeFadeOutTime {get; private set; } = 1f;

        [Tooltip("The fading time in seconds of the light intensity when the shadow is enabled.")]
        [field: SerializeField] public float m_IntensityFadeInTime { get; private set; } = 2f;
        [Tooltip("The fading time in seconds of the light intensity when the shadow is disabled.")]
        [field: SerializeField] public float m_IntensityFadeOutTime { get; private set; } = 1f;
        
        [field: SerializeField] public DefaultImportanceMode m_DefaultImportanceMode { get; private set; }
        [field: SerializeField] public ImportanceMode m_DefaultImportance { get; private set; } = ImportanceMode.MEDIUM;
        [field: SerializeField] public float m_MediumMinLightRange { get; private set; } = 12f;
        [field: SerializeField] public float m_MediumMinLightIntensity { get; private set; } = 2f;
        [field: SerializeField] public float m_HighMinLightRange { get; private set; } = 20f;
        [field: SerializeField] public float m_HighMinLightIntensity { get; private set; } = 3f;
        
        [field: SerializeField] public ShadowManagerConfig CurrentConfig { get; private set; }

        public void SetCurrentById(string id) =>
            CurrentConfig = GetById(id);

        public void SetCurrentByIndex(int index)
        {
            int count = Configs.Count - 1;
            
            if (index > count)
                index = count;
            
            CurrentConfig = Configs[index];
        }
    }
}