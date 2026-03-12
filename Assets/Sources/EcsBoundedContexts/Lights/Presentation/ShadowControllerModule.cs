using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Lights.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Presentation
{
    public class ShadowControllerModule : EntityModule
    {
        [Tooltip("Defines for what distance to the manager this shadow can be enabled. " +
                 "Default uses the manager default value." +
                 "You can set the distance to be proportional to the light range or custom.")]
        public ImportanceMode m_Importance = ImportanceMode.MEDIUM;
        [Tooltip("Defines the fine priority of the shadow. " +
                 "Within the same importance level, all shadows of a given priority value will have priority over shadows of a lower priority value. ")]
        public int m_Priority = 0;
        public DistanceMode m_DistanceMode = DistanceMode.DEFAULT;
        public float m_LightRangeShadowDistanceCoeff = 2f;
        public float m_CustomMaxShadowDistance = 100f;
        [Tooltip("Defines how the light intensity is reduced when its shadow is disabled. " +
                 "Default uses the manager default value. " +
                 "Ignore will not modify the intensity. " +
                 "You can set the intensity reduction to a custom coefficient.")]
        public IntensityReductionMode m_IntensityReductionMode = IntensityReductionMode.DEFAULT;
        public float m_CustomIntensityReductionCoeff = 1f;
        [Tooltip("Defines how the light range is reduced when its shadow is disabled. " +
                 "Default uses the manager default value. " +
                 "Ignore will not modify the range. " +
                 "You can set the range reduction to a custom coefficient.")]
        public RangeReductionMode m_RangeReductionMode = RangeReductionMode.DEFAULT;
        public float m_CustomRangeReductionCoeff = 1f;
        public bool m_IsShadowEnabled;
        public bool m_IsResolutionReduced;
        public Light m_Light;
    }
}