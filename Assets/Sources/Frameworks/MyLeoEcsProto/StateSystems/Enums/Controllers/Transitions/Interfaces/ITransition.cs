using System;
using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Controllers.Transitions.Interfaces
{
    public interface ITransition<out TEnumState>
        where TEnumState : Enum
    {
        TEnumState GetNextState(ProtoEntity entity);
        bool CanTransit(ProtoEntity entity);
    }
}