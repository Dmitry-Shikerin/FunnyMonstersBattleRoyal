using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.GameObjects.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("Active")]
    [Component(group: ComponentGroup.Common)]
    public struct ActiveComponent
    {
    }
}