using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Movements.TargetPoint.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("TargetPointIndex")]
    [Component(group: ComponentGroup.Common)]
    public struct TargetPointIndexComponent
    {
        public int Value;
    }
}