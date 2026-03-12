using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Particles.Domain
{
    [Serializable]
    [Component(group: ComponentGroup.Characters)]
    public struct HealParticleComponent
    {
        public ParticleSystem Value;
    }
}