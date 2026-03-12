using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("PeriodicLight")]
    [Component(group: ComponentGroup.Light)]
    public struct PeriodicLightComponent
    {
        public Light m_Light;
        public float m_MinIntensity;
        public float m_MaxIntensity;
        public float m_Period;
        public float m_MaxTimeOffset;
        public float m_Offset;
    }
}