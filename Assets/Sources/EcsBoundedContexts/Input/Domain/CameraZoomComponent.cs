using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct CameraZoomComponent
    {
        public Vector2 Value;
    }
}