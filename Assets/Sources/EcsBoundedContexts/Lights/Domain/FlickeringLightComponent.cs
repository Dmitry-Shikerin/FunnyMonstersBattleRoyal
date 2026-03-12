using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("FlickeringLight")]
    [Component(group: ComponentGroup.Light)]
    public struct FlickeringLightComponent
    {
        public float m_MinFlickeringTime;
        public float m_MaxFlickeringTime;
        public float m_MinIntensity;
        public float m_MaxIntensity;
        public float m_IntensitySmoothing;
        public Light m_Light;
        public float m_TargetIntensity;
    }
}