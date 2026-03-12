using System;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Enums.Domain
{
    public interface IEnumStateComponent<TEnumState>
        where TEnumState : Enum
    {
        TEnumState State { get; set; }
        bool IsEntered { get; set; }
    }
}