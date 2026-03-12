using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Domain
{
    [Serializable]
    [Component(group: ComponentGroup.Ability)]
    public struct NukeParticleComponent
    {
        public ParticleSystem Value;
    }
}