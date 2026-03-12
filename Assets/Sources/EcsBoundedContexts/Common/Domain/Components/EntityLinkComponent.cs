using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Common.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring("EntityLink")]
    [Component(group: ComponentGroup.Common)]
    public struct EntityLinkComponent
    {
        public EntityLink EntityLink;
        public int EntityId;
        public ProtoEntity ProtoEntity;
    }
}