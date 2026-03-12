using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Presentation
{
    public class FlickeringLightModule : EntityModule
    {
        public float m_MinFlickeringTime = 0.01f;
        public float m_MaxFlickeringTime = 0.1f;
        public float m_MinIntensity = 0f;
        public float m_MaxIntensity = 1f;
        public float m_IntensitySmoothing = 0.1f;
        public Light m_Light;
        public float m_TargetIntensity;
    }
}