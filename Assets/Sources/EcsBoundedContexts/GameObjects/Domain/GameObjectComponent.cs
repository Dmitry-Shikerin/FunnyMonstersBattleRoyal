using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.GameObjects.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("GameObject")]
    [Component(group: ComponentGroup.Common)]
    public struct GameObjectComponent
    {
        public GameObject Value;
    }
}