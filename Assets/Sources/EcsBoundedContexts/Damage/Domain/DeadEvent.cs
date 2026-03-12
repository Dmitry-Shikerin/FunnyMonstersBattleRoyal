using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Player)]
    [Serializable]
    [ProtoUnityAuthoring]
    public struct DeadEvent
    {
    }
}