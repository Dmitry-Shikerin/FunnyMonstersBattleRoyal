using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Spawners.Domain
{
    [Component(group: ComponentGroup.Common)]
    public struct SpawnPointTransformComponent
    {
        public Transform Value;
    }
}