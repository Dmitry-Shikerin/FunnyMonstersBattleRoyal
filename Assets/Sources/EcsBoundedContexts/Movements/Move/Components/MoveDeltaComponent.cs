using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Movements.Move.Components
{
    [Component(group: ComponentGroup.Player)]
    public struct MoveDeltaComponent
    {
        public float Value;
    }
}