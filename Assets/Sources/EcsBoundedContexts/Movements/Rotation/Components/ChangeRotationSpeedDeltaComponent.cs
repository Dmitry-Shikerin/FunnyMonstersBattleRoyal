using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct ChangeRotationSpeedDeltaComponent
    {
        public float Value;
    }
}