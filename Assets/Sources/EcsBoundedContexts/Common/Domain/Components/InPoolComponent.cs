using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Common.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct InPoolComponent
    {
    }
}