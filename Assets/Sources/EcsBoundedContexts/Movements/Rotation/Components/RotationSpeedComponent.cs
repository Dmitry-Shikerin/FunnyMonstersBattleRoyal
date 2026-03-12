using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("RotationSpeed")]
    [Component(group: ComponentGroup.Common)]
    public struct RotationSpeedComponent
    {
        public float Value;
    }
}