using System;
using Fusion;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct PlayerRefComponent
    {
        public PlayerRef Value;
    }
}