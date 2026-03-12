using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.Frameworks.GameServices.EntityPools.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct ReturnToPoolActionComponent
    {
        public Action ReturnToPool;
    }
}