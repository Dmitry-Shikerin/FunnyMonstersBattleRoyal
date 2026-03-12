using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Domain
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct FlamethrowerLinkComponent
    {
        public ProtoEntity Value;
    }
}