using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimatorLod.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(AnimatorLodSettingsCollector), menuName = "Configs/" + nameof(AnimatorLodSettingsCollector), order = 51)]
    public class AnimatorLodSettingsCollector : ConfigCollector<AnimatorLodSettingsConfig>
    {
    }
}