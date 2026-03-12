using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.LookAt.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct LookAtComponent
    {
        public Transform Value;
    }
}