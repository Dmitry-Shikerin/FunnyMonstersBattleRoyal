using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Domain
{
    [Serializable]
    [Component(group: ComponentGroup.Enemy)]
    public struct FlameParticleComponent
    {
        public ParticleSystem Value;
    }
}