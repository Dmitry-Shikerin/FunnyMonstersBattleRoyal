using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Movements.Move.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("MoveSpeed")]
    [Component(group: ComponentGroup.Common)]
    public struct MoveSpeedComponent
    {
        public float Value;
    }
}