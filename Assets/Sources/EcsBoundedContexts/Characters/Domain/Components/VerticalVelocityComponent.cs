using System;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct VerticalVelocityComponent
    {
        public float Value;
    }
}