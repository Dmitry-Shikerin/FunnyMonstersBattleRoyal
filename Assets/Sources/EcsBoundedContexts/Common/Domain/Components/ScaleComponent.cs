using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Common.Domain.Components
{
    [Component(group: ComponentGroup.Common)]
    public struct ScaleComponent
    {
        public Vector3 TargetScale;
        public Vector3 CurrentScale;
    }
}