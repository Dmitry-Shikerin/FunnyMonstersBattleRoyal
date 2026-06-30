using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct CameraLookComponent
    {
        public Vector2 Value;
    }
}