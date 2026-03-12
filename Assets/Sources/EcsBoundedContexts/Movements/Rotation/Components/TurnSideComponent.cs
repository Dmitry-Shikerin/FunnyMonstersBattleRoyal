using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Movements.Rotation.Data;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Components
{
    [Component(group: ComponentGroup.Common)]
    public struct TurnSideComponent
    {
        public TurnSideType Value;
    }
}