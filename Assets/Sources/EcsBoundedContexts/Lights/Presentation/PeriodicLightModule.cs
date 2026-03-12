using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Presentation
{
    public class PeriodicLightModule : EntityModule
    {
        public Light m_Light;

        public float m_MinIntensity = 0f;
        public float m_MaxIntensity = 3f;
        public float m_Period = 0.1f;

        public float m_MaxTimeOffset = 1f;

        public float m_Offset;
    }
}