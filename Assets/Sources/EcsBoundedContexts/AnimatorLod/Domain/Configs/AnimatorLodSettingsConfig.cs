using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AnimatorLod.Domain.Configs
{
    public class AnimatorLodSettingsConfig : Config
    {
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public int FrameCount { get; private set; }
        [field: SerializeField] public SkinQuality MaxBoneWeight { get; private set; }
    }
}