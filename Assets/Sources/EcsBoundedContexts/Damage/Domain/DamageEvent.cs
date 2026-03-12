using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Common)]
    [Serializable]
    [ProtoUnityAuthoring]
    public struct DamageEvent
    {
        public int Value;
    }
}