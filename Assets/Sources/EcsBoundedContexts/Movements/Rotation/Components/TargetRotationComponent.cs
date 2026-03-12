using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct TargetRotationComponent
    {
        public Quaternion Value;
    }
}