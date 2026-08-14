using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct PlayerEntityComponent
    {
        public ProtoEntity Value;
    }
}