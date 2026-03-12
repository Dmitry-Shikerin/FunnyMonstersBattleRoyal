using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("FireLight")]
    [Component(group: ComponentGroup.Light)]
    public struct FireLightComponent
    {
        public float m_MinFlickeringTime;
        public float m_MaxFlickeringTime;
        public float m_MinIntensity;
        public float m_MaxIntensity;
        public float m_IntensitySmoothing;
        public float m_MaxPositionOffset;
        public float m_PositionSmoothing;
        public Light m_Light;
        public float m_TargetIntensity;
        public Vector3 m_DefaultPos;
        public Vector3 m_TargetPos;
    }
}