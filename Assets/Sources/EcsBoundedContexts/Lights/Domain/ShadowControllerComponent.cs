using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Lights.Domain.Enums;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("PeriodicLight")]
    [Component(group: ComponentGroup.Light)]
    public struct ShadowControllerComponent
    {
        public ImportanceMode m_Importance;
        [Tooltip("Defines for what distance to the manager this shadow can be enabled. " +
                 "Default uses the manager default value." +
                 "You can set the distance to be proportional to the light range or custom.")]
        public DistanceMode m_DistanceMode;
        public float m_LightRangeShadowDistanceCoeff;
        public float m_CustomMaxShadowDistance;
        [Tooltip("Defines how the light intensity is reduced when its shadow is disabled. " +
                 "Default uses the manager default value. " +
                 "Ignore will not modify the intensity. " +
                 "You can set the intensity reduction to a custom coefficient.")]
        public IntensityReductionMode m_IntensityReductionMode;
        public float m_CustomIntensityReductionCoeff;
        [Tooltip("Defines how the light range is reduced when its shadow is disabled. " +
                 "Default uses the manager default value. " +
                 "Ignore will not modify the range. " +
                 "You can set the range reduction to a custom coefficient.")]
        public RangeReductionMode m_RangeReductionMode;
        public float m_CustomRangeReductionCoeff;
        public bool m_IsShadowEnabled;
        public bool m_IsResolutionReduced;
    }
}