using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Enemy)]
    public struct MassAttackParticleComponent
    {
        public ParticleSystem Value;
    }
}