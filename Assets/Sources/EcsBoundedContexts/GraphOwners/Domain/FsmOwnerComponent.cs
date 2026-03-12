using NodeCanvas.StateMachines;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.GraphOwners.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct FsmOwnerComponent
    {
        public FSMOwner Value;
    }
}