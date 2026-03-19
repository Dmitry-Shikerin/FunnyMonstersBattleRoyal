using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct DirectionComponent
    {
        public Vector3 Value;
    }
}