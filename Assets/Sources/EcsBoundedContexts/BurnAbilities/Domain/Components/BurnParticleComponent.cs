using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.BurnAbilities.Domain.Components
{
    [Serializable]
    [Component(group: ComponentGroup.Enemy)]
    [Aspect(AspectName.Game)]
    public struct BurnParticleComponent
    {
        public ParticleSystem Value;
    }
}