using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;
using UnityEngine.Rendering;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable]
    [ProtoUnityAuthoring("Light")]
    [Component(group: ComponentGroup.Light)]
    public struct LightComponent
    {
        public Light m_Light;
        public float m_Range;
        public float m_Intensity;
        public Color m_Color;
        public bool m_RequestShadow;
        public float m_ShadowStrength;
        public LightShadows m_ShadowMode;
        public LightShadowResolution m_ShadowResolution;
    }
}