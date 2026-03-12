using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Common)]
    [Serializable]
    [ProtoUnityAuthoring]
    public struct BloodParticleComponent
    {
        public ParticleSystem Value;
    }
}