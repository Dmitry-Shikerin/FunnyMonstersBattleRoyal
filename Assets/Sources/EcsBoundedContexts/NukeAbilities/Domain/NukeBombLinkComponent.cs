using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.NukeAbilities.Domain
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct NukeBombLinkComponent
    {
        public ProtoEntity Value;
    }
}