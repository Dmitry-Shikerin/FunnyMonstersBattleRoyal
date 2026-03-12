using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Domain
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct NukeDamageColliderComponent
    {
        public BoxCollider Value;
    }
}