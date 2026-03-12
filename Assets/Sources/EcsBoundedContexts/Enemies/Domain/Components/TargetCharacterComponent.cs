using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Enemies.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Enemy)]
    public struct TargetCharacterComponent
    {
        public ProtoEntity Value;
    }
}